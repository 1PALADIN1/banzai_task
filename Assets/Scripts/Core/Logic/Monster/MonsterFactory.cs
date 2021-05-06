using System.Collections.Generic;
using Core.Data.Monster;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Monster
{
	public sealed class MonsterFactory : IMonsterFactory
	{
		private readonly MonsterView _viewPrefab;
		private readonly IDictionary<MonsterType, IMonsterData> _monsters;

		public MonsterFactory(IMonsterDatabase monsterDatabase)
		{
			_viewPrefab = monsterDatabase.ViewPrefab;
			_monsters = monsterDatabase.GetMonsterData();
		}
		
		public MonsterView CreateMonster(MonsterType monsterType)
		{
			if (!_monsters.TryGetValue(monsterType, out var monsterData))
			{
				Debug.LogError($"Monster data for type {monsterType} is not found!");
				return null;
			}
			
			var view = Object.Instantiate(_viewPrefab);
			view.Init(monsterData);
			return view;
		}
	}
}