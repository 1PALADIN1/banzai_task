using System;
using UnityEngine;

namespace Core.Data.Monster
{
	public interface IMonsterData
	{
		MonsterType MonsterType { get; }
		int MaxHp { get; }
		int Damage { get; }
		float Defense { get; }
		float MoveSpeed { get; }
		Sprite MonsterSprite { get; }
	}
	
	[Serializable]
	public sealed class MonsterData : IMonsterData
	{
		[SerializeField] private MonsterType _monsterType;
		[SerializeField] private int _maxHp;
		[SerializeField] private int _damage;
		[SerializeField] private float _defense;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private Sprite _monsterSprite;

		public MonsterType MonsterType => _monsterType;
		public int MaxHp => _maxHp;
		public int Damage => _damage;
		public float Defense => _defense;
		public float MoveSpeed => _moveSpeed;
		public Sprite MonsterSprite => _monsterSprite;
	}
}