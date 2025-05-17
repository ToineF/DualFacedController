using UnityEngine;

namespace FeedbacksEditor
{
    /// <summary>
    /// An example of how to call the GameEventsManager in code.
    /// </summary>
    public class FeedbackMethodExample : MonoBehaviour
    {
        [SerializeField] private GameEvent _feedback;
        [SerializeField] private GameObject _targetGameObject;

        public void PlayEvent()
        {
            GameEventsManager.PlayEvent(_feedback, _targetGameObject);
        }
    }
}