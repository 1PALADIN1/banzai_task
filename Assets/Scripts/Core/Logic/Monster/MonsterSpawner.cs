using Core.Data.Monster;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Logic.Monster
{
	public class MonsterSpawner : MonoBehaviour
	{
		[SerializeField] private int _maxMonsterSize = 10;
		
		private IMonsterFactory _monsterFactory;
		private int _activeMonstersNumber;
		
		[Inject]
		public void Init(IMonsterFactory monsterFactory, ISceneController sceneController)
		{
			_monsterFactory = monsterFactory;
		}

		public bool TrySpawnMonster(MonsterType monsterType, Vector2 spawnPosition, out MonsterView monsterView)
		{
			if (_activeMonstersNumber >= _maxMonsterSize)
			{
				monsterView = null;
				return false;
			}
			
			_activeMonstersNumber++;
			var monster = _monsterFactory.CreateMonster(monsterType);
			monster.transform.position = spawnPosition;
			
			monster.Destroyed += OnMonsterDestroyed; 
			monsterView = monster;
			return true;
		}

		private void OnMonsterDestroyed(MonsterView monsterView)
		{
			monsterView.Destroyed -= OnMonsterDestroyed;
			_monsterFactory.Release(monsterView);
			_activeMonstersNumber--;
		}
	}
}