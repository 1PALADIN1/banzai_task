using System.Collections.Generic;
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
		[SerializeField] private MonsterType[] _spawnMonsters;

		private readonly HashSet<MonsterView> _sceneMonsterViews = new HashSet<MonsterView>();
		
		private IMonsterFactory _monsterFactory;
		private ISceneController _sceneController;
		private int _monsterSize;
		
		[Inject]
		public void Init(IMonsterFactory monsterFactory, ISceneController sceneController)
		{
			_monsterFactory = monsterFactory;
			_sceneController = sceneController;
			_monsterSize = 0;
		}

		private void CreateMonster(Vector2 spawnPosition)
		{
			if (_spawnMonsters.Length == 0)
			{
				Debug.LogError("Spawn monster types are not specified!", this);
				return;
			}
			
			if (_monsterSize >= _maxMonsterSize)
				return;

			var monster = _monsterFactory.CreateMonster(GetRandomMonster());
			monster.transform.position = spawnPosition;
			_sceneMonsterViews.Add(monster);
			monster.Destroyed += OnMonsterDestroyed;
			_monsterSize++;
		}

		private MonsterType GetRandomMonster()
		{
			var randIndex = Random.Range(0, _spawnMonsters.Length);
			return _spawnMonsters[randIndex];
		}
		
		private void OnDestroy()
		{
			foreach (var monsterView in _sceneMonsterViews)
				monsterView.Destroyed -= OnMonsterDestroyed;
			
			_sceneMonsterViews.Clear();
		}

		private void OnMonsterDestroyed(MonsterView monsterView)
		{
			monsterView.Destroyed -= OnMonsterDestroyed;
			_sceneMonsterViews.Remove(monsterView);
			_monsterFactory.Release(monsterView);
			_monsterSize--;
		}
		
		//TODO: debug
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				var spawnPoint = _sceneController.GetRandomPointOutOfScene();
				CreateMonster(spawnPoint);
			}
		}
	}
}