using UnityEngine;
using AntoineFoucault.Utilities;

namespace Cattac.Cutscenes
{
    /// <summary>
    /// A collider that triggers a cutscene on enter
    /// </summary>
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
                CutsceneManager.Instance.StartNewSequence(_cutscene);
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