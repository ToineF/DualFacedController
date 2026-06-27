
    using UnityEngine;

    /// <summary>
    /// Set only one item in a collection to be active and hides all of the others
    /// </summary>
    public class ActivateSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;

        public void Switch(int index)
        {
            if (index < -1 ||  index >= _gameObjects.Length) return;

            foreach (var go in _gameObjects)
            {
                go.SetActive(false);
            }
            _gameObjects[index].SetActive(true);
        }
    }