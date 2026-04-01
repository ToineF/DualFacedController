using System.Collections;
using Cattac.Character;
using Cattac.Interactables.Death;
using FeedbacksEditor;
using MaskTransitions;
using UnityEngine;

public class RespawnZone : BoxTriggerUnityEventPlayer
{
    [Header("Respawn Timings")]
    [SerializeField] private float _transitionTime = 1f;
    [SerializeField] private float _teleportatingDelay = .4f;
    [SerializeField] private float _startDelay = .4f;
    [SerializeField] private GameEvent _dieFeedback;
    
    private const float _respawnDelay = 1f;
    
    private float _respawnTimer;
    
    private void Start()
    {
        OnTriggerEnter.AddListener(Respawn);
    }
    
    private void OnDestroy()
    {
        OnTriggerEnter.RemoveListener(Respawn);
    }

    private void Respawn(CharacterHead characterHead)
    {
        if (_respawnTimer > 0f) return; 
        
        Vector3? position = CheckpointsManager.CurrentCheckpoint?.transform.position;
        if (position.HasValue == false)
        {
            Debug.LogError("Respawn position not found");
            return;
        }
        
        _respawnTimer = _respawnDelay;
        StartCoroutine(RespawnCoroutine(characterHead, position.Value));
    }

    private IEnumerator RespawnCoroutine(CharacterHead characterHead, Vector3 position)
    {
        GameEventsManager.PlayEvent(_dieFeedback, characterHead.gameObject);
        
        yield return new WaitForSeconds(_startDelay);
        
        TransitionManager.Instance.PlayTransition(_transitionTime);
        
        yield return new WaitForSeconds(_teleportatingDelay);
        
        characterHead.CharacterManager.TeleportPlayer(position);
        
    }

    private void Update()
    {
        _respawnTimer -= Time.deltaTime;
    }


    // private void EnterState(CharacterManager manager)
    // {
    //     Debug.Log("DEATH");
    //     manager.MovementManager.ResetAllExternalForces();
    //     manager.MovementManager.OnDeath?.Invoke(manager);
    //
    //     manager.Transition.SetTransition(() =>
    //     {
    //         manager.Transition.PlayFadeIn();
    //         Debug.Log(manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //
    //         manager.CharacterCollider.Rigidbody.gameObject.SetActive(false);
    //         manager.CheckpointManager.SetToCheckpointPosition(manager.CharacterCollider.Rigidbody.gameObject);
    //         manager.CharacterCollider.Rigidbody.gameObject.SetActive(true);
    //         Debug.Log("AAA " + manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //         manager.States.SwitchState(manager.States.FallingState);
    //         //manager.Transition.PlayFadeIn();
    //
    //
    //         Debug.Log("AAA " + manager.CharacterCollider.Rigidbody.gameObject.name + ": " + manager.CharacterCollider.Rigidbody.gameObject.transform.position);
    //     });
    //     
    //     // Feedbacks
    //     manager.Feedbacks.PlayFeedback(manager.Data.FeedbacksData.DeathFeedback);
    // }
}