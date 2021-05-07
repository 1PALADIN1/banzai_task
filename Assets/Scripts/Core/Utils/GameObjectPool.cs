using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Utils
{
	public class GameObjectPool<T> where T : MonoBehaviour
	{
		private readonly Queue<T> _gameObjects;
		
		public bool HasObjects => _gameObjects.Any();

		public GameObjectPool()
		{
			_gameObjects = new Queue<T>();
		}
		
		public T GetObject()
		{
			if (!HasObjects)
			{
				Debug.LogError("Game object pool is empty!");
				return null;
			}
			
			return _gameObjects.Dequeue();
		}

		public bool TryGetObject(out T gameObject)
		{
			if (!HasObjects)
			{
				gameObject = null;
				return false;
			}

			gameObject = _gameObjects.Dequeue();
			return true;
		}
		
		public void Release(T gameObject)
		{
			_gameObjects.Enqueue(gameObject);
		}
	}
}