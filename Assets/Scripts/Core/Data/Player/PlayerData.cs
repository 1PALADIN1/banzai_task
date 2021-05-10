using System;
using UnityEngine;

namespace Core.Data.Player
{
	public interface IPlayerData
	{
		int MaxHp { get; }
		float Defence { get; }
		float MoveSpeed { get; }
		float RotateSpeed { get; }
	}
	
	[Serializable]
	public sealed class PlayerData : IPlayerData
	{
		[SerializeField] private int _maxHp;
        [SerializeField, Range(0f, 1f)] private float _defence;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        public int MaxHp => _maxHp;
        public float Defence => _defence;
        public float MoveSpeed => _moveSpeed;
        public float RotateSpeed => _rotateSpeed;
	}
}