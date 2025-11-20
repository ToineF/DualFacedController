using System;
using System.Threading.Tasks;
using Cattac.Interactables;
using Cattac.Interactables.NPC;
using FeedbacksEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Cattac.Character
{
    public class CharacterHead : MonoBehaviour, IDetectable
    {
        public Action OnSeparate { get; set; }
        public Action OnConnect { get; set; }

        public bool IsMovementBlocked => NormalizedDirection.sqrMagnitude > .5f && CurrentRigidbody.linearVelocity.sqrMagnitude < 30;
        public Vector3 Direction { get; private set; }
        public Vector2 NormalizedDirection { get; private set; }
        public Vector2 InputDirection => _inputDirection;
        public Vector2 LastNormalizedDirection => _lastNormalizedDirection;

        public IGrabbable CurrentGrabbable
        {
            get => _currentGrabbable;
            set => _currentGrabbable = value;
        }

        public bool IsGrabbing { get; private set; }
        public bool IsSeparated { get; private set; }
        
        public Rigidbody CurrentRigidbody
        {
            get
            {
                var newRigidbody = IsSeparated ? SeparatedRigidbody : TogetherRigidbody;
                transform.SetParent(newRigidbody.transform);
                if (IsSeparated == false) SeparatedRigidbody.position = TogetherRigidbody.position;
                transform.localPosition = Vector3.zero;
                return newRigidbody;
            }
        }

        [field: Header("Head Properties")]
        [field: SerializeField] public Rigidbody TogetherRigidbody { get; set; }
        [field: SerializeField] public Rigidbody SeparatedRigidbody { get; set; }
        [field: SerializeField] public CapsuleCollider Collider { get; set; }
        [SerializeField] private CharacterHeadData _data;

        [Header("Input Properties")]
        [field:SerializeField, FormerlySerializedAs("_isLeftHead")] public bool IsLeftHead { get; private set; }
        
        [field:Header("Grab")]
        [field:SerializeField] public Rigidbody GrabParent { get; private set; }

        [Header("Feedbacks")]
        [SerializeField] private GameEvent _squeakFeedback;

        private float _snakeTimer;

        private Vector2 _inputDirection;
        private Vector2 _lastNormalizedDirection;
        private RaycastHit _lastGroundHit;

        private IGrabbable _currentGrabbable;

        private float _jumpTimer;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (UserInput.Instance.GetHead(IsLeftHead) == null) return;
            CheckMovements();
            CheckGrab();
        }

        private void CheckMovements()
        {
            _inputDirection = UserInput.Instance.GetHead(IsLeftHead).MoveInput;
            float lerpSpeed;
            if (_inputDirection.magnitude > 0.1f) // Acceleration
            {
                lerpSpeed = Mathf.Lerp(_data.DecelerationLerp, _data.AccelerationLerp, _inputDirection.magnitude);
            }
            else // Deceleration phase
            {
                lerpSpeed = Mathf.Lerp(_data.AccelerationLerp, _data.DecelerationLerp, (1 - _inputDirection.magnitude));
            }
            NormalizedDirection = Vector3.Lerp(NormalizedDirection, _inputDirection, lerpSpeed * Time.deltaTime);
            if (NormalizedDirection.magnitude > 0.1f) _lastNormalizedDirection = NormalizedDirection;
        }

        private void CheckGrab()
        {
            IsGrabbing = UserInput.Instance.GetHead(IsLeftHead).GrabInput;
            //Rigidbody.isKinematic = IsGrabbing;

            var isJumping = IsGrabbing;
            var isGrabbingThisFrame = UserInput.Instance.GetHead(IsLeftHead).GrabInputPressed;

            //if (isJumping) Rigidbody.AddForce(Vector3.up * _heightForce, ForceMode);
            // Update timer
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer <= 0 && isJumping) ApplyForceWithDecay(CurrentRigidbody);

            if (isGrabbingThisFrame)
            {
                if (GrabParent && _currentGrabbable == null)
                    Grab();
                else
                {
                    _currentGrabbable.OnUngrab(this);
                    _currentGrabbable = null;
                }
            }
        }

        private void Grab()
        {
            var colliders = Physics.OverlapSphere(GrabParent.transform.position, _data.GrabRadius, _data.GrabLayerMask);
            if (colliders.Length < 1) return;
            var minDistance = (GrabParent.transform.position - colliders[0].transform.position).sqrMagnitude;
            var closestCollider = colliders[0];
            if (colliders.Length > 1)
            {
                for (int i = 1; i < colliders.Length; i++)
                {
                    var currentDistance = (GrabParent.transform.position - colliders[i].transform.position).sqrMagnitude;
                    if (currentDistance < minDistance)
                    {
                        closestCollider = colliders[i];
                        minDistance = currentDistance;
                    }
                }
            }

            if (closestCollider.TryGetComponent(out IGrabbable grabbable))
            {
                _currentGrabbable = grabbable;
                _currentGrabbable.OnGrab(this);
            }
        }

        private async void ApplyForceWithDecay(Rigidbody rb)
        {
            GameEventsManager.PlayEvent(_squeakFeedback, gameObject);

            float currentForce = _data.JumpForce;
            _jumpTimer = _data.MinJumpsInterval;

            // Apply force over several frames with decay
            for (int i = 0; i < _data.JumpTime; i++)
            {
                if (currentForce <= 0) break;

                rb.AddForce(Vector3.up * currentForce, ForceMode.Impulse);
                currentForce -= _data.JumpDecrease;

                // Wait until the next frame
                await Task.Delay(1);
            }
        }

        private void ToggleSeparation()
        {
            IsSeparated = !IsSeparated;
            OnSeparation();
        }

        public void SetSeparation(bool isSeparated)
        {
            IsSeparated = isSeparated;
            OnSeparation();
        }

        private void OnSeparation()
        {
            if (IsSeparated) OnSeparate?.Invoke();
            else OnConnect?.Invoke();
        }

        private void FixedUpdate()
        {
            MoveSelf();
            MoveSnake();
        }

        private void MoveSelf()
        {
            Direction = new Vector3(NormalizedDirection.x, 0, NormalizedDirection.y) * _data.Speed;
            Direction = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * Direction;
            //Direction = _camera.transform.forward * Direction.z + _camera.transform.right * Direction.x;
            Direction = new Vector3(Direction.x, 0, Direction.z);
            Direction = Vector3.ProjectOnPlane(Direction, _lastGroundHit.normal);
            
            CurrentRigidbody.AddForce(Direction, ForceMode.Impulse);
            ApplyGrabbableForce(Direction);
            ApplyGravity();
        }

        private void ApplyGrabbableForce(Vector3 force)
        {
            _currentGrabbable?.AddForce(force);
        }

        private void ApplyGravity()
        {
            Physics.SphereCast(transform.position, Collider.radius, Vector3.down, out _lastGroundHit, _data.GroundDetectionDistance, _data.GroundLayer, QueryTriggerInteraction.Ignore);
        }

        private void MoveSnake()
        {
            _snakeTimer += Time.deltaTime;
            var s = Mathf.Sin((_snakeTimer + _data.SnakeOffset) / _data.SnakeFrequency) * _data.SnakeAmplitude;
            var directionVector = Vector3.Cross(new Vector3(NormalizedDirection.x, 0, NormalizedDirection.y), Vector3.down);
            CurrentRigidbody.AddForce(directionVector * s, ForceMode.Impulse);
        }
        
        public Cat_State OnDetect(Cat_StateManager catStateManager)
        {
            if (IsSeparated)
                return catStateManager.Cat_Chase_Mouse;
            else
                return catStateManager.Cat_State_LookAtDivinity;
        }
    }
}