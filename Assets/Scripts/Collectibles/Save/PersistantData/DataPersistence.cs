using UnityEngine;

namespace Cattac.Collectibles.Save
{
    /// <summary>
    /// Generic interface to implement data persistance
    /// </summary>
    public abstract class DataPersistence : MonoBehaviour
    {
        public int Index
        {
            get => _index; 
            set => _index = value;
        }
        [SerializeField] private int _index;

        private void Start()
        {
            Load();
            StartInternal();
        }

        protected virtual void StartInternal() { }

        protected virtual void Save()
        {
            SetID(true);
        }
        
        protected abstract void Load();

        protected int GetID() => DataPersistenceManager.GetID(_index);
        
        protected void SetID(bool value) => DataPersistenceManager.SetID(_index, value);
        
    }
}