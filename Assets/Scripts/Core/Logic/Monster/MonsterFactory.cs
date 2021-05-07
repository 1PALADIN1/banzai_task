using System.Collections.Generic;
using Core.Data.Monster;
using Core.Utils;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Monster
{
	public sealed class MonsterFactory : IMonsterFactory
	{
		private readonly MonsterView _viewPrefab;
		private readonly IDictionary<MonsterType, IMonsterData> _monsters;
		private readonly GameObjectPool<MonsterView> _monsterPool;

		public MonsterFactory(IMonsterDatabase monsterDatabase)
		{
			_viewPrefab = monsterDatabase.ViewPrefab;
			_monsters = monsterDatabase.GetMonsterData();
			_monsterPool = new GameObjectPool<MonsterView>();
		}
		
		public MonsterView CreateMonster(MonsterType monsterType)
		{
			if (!_monsters.TryGetValue(monsterType, out var monsterData))
			{
				Debug.LogError($"Monster data for type {monsterType} is not found!");
				return null;
			}
			
			return CreateMonster(monsterData);
		}

		private MonsterView CreateMonster(IMonsterData monsterData)
		{
			if (!_monsterPool.TryGetObject(out var monsterView))
				monsterView = Object.Instantiate(_viewPrefab);
			
			monsterView.Init(monsterData);
			return monsterView;
		}
	}
}