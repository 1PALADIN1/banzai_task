using System;
using UnityEngine;

namespace Core.Data.Gun
{
	public interface IGunData
	{
		int Damage { get; }
		float CooldownTime { get; }
		Sprite GunSprite { get; }
	}
	
	[Serializable]
	public sealed class GunData : IGunData
	{
		[SerializeField] private int _damage;
		[SerializeField] private float _cooldownTime;
		[SerializeField] private Sprite _gunSprite;

		public int Damage => _damage;
		public float CooldownTime => _cooldownTime;
		public Sprite GunSprite => _gunSprite;
	}
}