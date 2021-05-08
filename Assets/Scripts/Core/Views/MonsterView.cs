using System;
using Core.Data.Monster;
using Core.Logic;
using UnityEngine;

namespace Core.Views
{
	public sealed class MonsterView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private HealthComponent _health;
		[SerializeField] private DamageComponent _damage;
		
		public event Action<MonsterView> Destroyed;
		
		private bool _isDead;

		public bool IsDead
		{
			get => _isDead;
			private set
			{
				_isDead = value;
				gameObject.SetActive(!_isDead);
				
				if (_isDead)
					Destroyed?.Invoke(this);
			}
		}

		public void Init(IMonsterData monsterData)
		{
			_spriteRenderer.sprite = monsterData.MonsterSprite;
			_health.Init(monsterData.MaxHp, monsterData.Defense);
			_damage.Init(monsterData.Damage);
			
			IsDead = false;
		}

		private void Awake()
		{
			_health.HealthChanged += OnHealthChanged;
		}

		private void OnDestroy()
		{
			_health.HealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged(int newValue)
		{
			if (IsDead)
				return;
			
			if (newValue <= 0)
				IsDead = true;
		}

		//TODO: debug
		private void OnMouseDown()
		{
			_health.ApplyDamage(20);
		}
	}
}