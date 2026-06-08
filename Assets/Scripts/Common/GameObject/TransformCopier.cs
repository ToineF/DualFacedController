using UnityEngine;


    public class TransformCopier : MonoBehaviour
    {
        [SerializeField] private Transform _self;
        [SerializeField] private Transform _target;
        
        public void SetParent()
        {
            _self.SetParent(_target);
        }

        public void SetPosition()
        {
            _self.position = _target.position;
        }

        public void SetRotation()
        {
            _self.rotation = _target.rotation;
        }

        public void SetScale()
        {
            _self.localScale = _target.localScale;
        }

        public void SetTransform(bool setParent)
        {
            SetPosition();
            SetRotation();
            SetScale();
            if (setParent) SetParent();
        }
    }