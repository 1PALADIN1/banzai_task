using System.Collections.Generic;
using Core.Views;
using UnityEngine;

namespace Core.Data.Monster
{
	public interface IMonsterDatabase
	{
		MonsterView ViewPrefab { get; }
		IDictionary<MonsterType, IMonsterData> GetMonsterData();
	}
	
	[CreateAssetMenu(fileName = "MonsterDatabase", menuName = "Game/Data/MonsterDatabase")]
	public sealed class MonsterDatabase : ScriptableObject, IMonsterDatabase
	{
		[SerializeField] private MonsterView _viewPrefab;
		[SerializeField] private MonsterData[] _monsterData;
		
		public MonsterView ViewPrefab => _viewPrefab;
		
		public IDictionary<MonsterType, IMonsterData> GetMonsterData()
		{
			var result = new Dictionary<MonsterType, IMonsterData>();
			foreach (var monsterData in _monsterData)
			{
				if (result.ContainsKey(monsterData.MonsterType))
				{
					Debug.LogError($"Duplicate monster type key {monsterData.MonsterType} is found!");
					return null;
				}
				
				result.Add(monsterData.MonsterType, monsterData);
			}

			return result;
		}
	}
}