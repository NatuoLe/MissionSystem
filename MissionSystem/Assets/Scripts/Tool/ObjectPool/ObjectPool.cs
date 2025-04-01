using UnityEngine;
using System.Collections.Generic;

// ObjectPool Class
namespace ThGold.Pool
{
    public class ObjectPool<T> where T : new()
    {
        private Stack<T> _objects;

        public ObjectPool(int initialSize)
        {
            _objects = new Stack<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                _objects.Push(new T());
            }
        }

        public T GetObject()
        {
            if (_objects.Count == 0)
            {
                _objects.Push(new T());
            }
            return _objects.Pop();
        }

        public void ReturnObject(T obj)
        {
            _objects.Push(obj);
        }
    }
}
// Usage
// Initialize the object pool
//var objectPool = new ObjectPool<GameObject>(10);

// Get an object from the pool
//var obj = objectPool.GetObject();

// Return the object to the pool
//objectPool.ReturnObject(obj);
