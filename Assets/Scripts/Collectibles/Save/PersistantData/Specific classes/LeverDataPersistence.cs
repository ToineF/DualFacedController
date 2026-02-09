using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class LeverDataPersistence : DataPersistence
    {
        [SerializeField] private Lever _lever;
        [SerializeField] private HingeJoint _joint;
        
        protected override void StartInternal()
        {
            _lever.OnLeverRight.AddListener(Save);
        }

        private void OnDestroy()
        {
            _lever.OnLeverRight.RemoveListener(Save);
        }

        
        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _lever.OnLeverRight?.Invoke();
                _joint.transform.eulerAngles = Vector3.Scale(_joint.transform.eulerAngles, new Vector3(1, 1, -1));
            }
        }
    }
}