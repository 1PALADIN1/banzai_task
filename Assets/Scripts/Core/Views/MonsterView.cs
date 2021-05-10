using System;
using Core.Data.Monster;
using Core.Logic;
using UnityEngine;

namespace Core.Views
{
	public class MonsterView : MonoBehaviour
	{
		private const float StopMoveDistanceSqr = 0.4f * 0.4f;
		
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private HealthComponent _healthComponent;
		[SerializeField] private DamageComponent _damageComponent;
		[SerializeField] private MoveComponent _moveComponent;
		
		public event Action<MonsterView> Destroyed;
		
		private Transform _moveTarget;
		private bool _isMoveTargetSet;
		private bool _isDead;

		public void Init(IMonsterData monsterData)
		{
			_spriteRenderer.sprite = monsterData.MonsterSprite;
			_healthComponent.Init(monsterData.MaxHp, monsterData.Defense);
			_damageComponent.Init(monsterData.Damage);
			_moveComponent.Init(monsterData.MoveSpeed);
			
			SetIsDead(false);
		}

		public void SetMoveTarget(Transform moveTarget)
		{
			_moveTarget = moveTarget;
			_moveComponent.StartMoving();
			_isMoveTargetSet = true;
		}

		public void StopMoving()
		{
			_isMoveTargetSet = false;
			_moveComponent.StopMoving();
		}

		private void Awake()
		{
			_healthComponent.HealthChanged += OnHealthChanged;
		}

		private void OnDestroy()
		{
			_healthComponent.HealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged(int newValue)
		{
			if (_isDead)
				return;
			
			if (newValue <= 0)
				SetIsDead(true);
		}

		private void SetIsDead(bool isDead)
		{
			_isDead = isDead;
			gameObject.SetActive(!_isDead);
				
			if (_isDead)
				Destroyed?.Invoke(this);
		}

		private void FixedUpdate()
		{
			if (!_isMoveTargetSet)
				return;

			var moveDirection = (Vector2) (_moveTarget.position - transform.position);
			if (Vector2.SqrMagnitude(moveDirection) <= StopMoveDistanceSqr)
			{
				if (_moveComponent.IsMoving) 
					_moveComponent.StopMoving();
				
				return;
			}
			
			_moveComponent.SetMoveDirection(moveDirection);
			if (!_moveComponent.IsMoving)
				_moveComponent.StartMoving();
		}
	}
}