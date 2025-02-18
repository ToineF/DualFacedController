using UnityEngine;
using AntoineFoucault.Utilities;

namespace Cattac.Cutscenes
{
    public class CutsceneCollider : BoxTrigger
    {
        [SerializeField] private Cutscene _cutscene;

        private new void Awake()
        {
            base.Awake();
        }

        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (!other.TryGetComponent(out Cattac.Character.CharacterHead head)) return;

            // Start Cutscene
            if (_cutscene != null)
            {
                CutsceneManager.Instance.StartNewSequence(_cutscene, head);
            }

            // Clear
            foreach (var child in transform.GetChildren())
            {
                child.transform.SetParent(transform.parent?.parent);
            }
            Destroy(gameObject);
        }
    }
}