using UnityEngine;

namespace BulletEditor
{
    public class ObjectPooling<T> where T : Component
    {
        public int Count => _pool.Length;
        public T[] Pool => _pool;
        
        private T[] _pool;
        private int _index;
        
        public void Initialize(int poolSize, T prefab, Transform parent = null)
        {
            _pool = new T[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                var newObject = GameObject.Instantiate(prefab, parent);
                //newObject.gameObject.SetActive(false);
                _pool[i] = newObject;
            }
        }
        
        public T GetFreeObject() {
            _index = (_index + 1) % _pool.Length; 
            return _pool[_index];
        }

        public void ClearPool()
        {
            foreach (var item in _pool)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}