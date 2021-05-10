using Core.Data.Gun;
using Core.Data.Player;
using Core.Logic;
using Core.Logic.Gun;
using Core.Logic.Scene;
using UnityEngine;

namespace Core.Views
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField] private GunView _gunView;
		[SerializeField] private LayerMask _hitSide;
		[SerializeField] private Transform _rotateRoot;
		[SerializeField] private HealthComponent _healthComponent;
		
		private IPlayerData _playerData;
		private IGunData[] _guns;
		private SceneBounds _sceneBounds;
		
		private Vector2 _moveDirection = Vector2.zero;
		private Vector2 _rotateDirection = Vector2.zero;
		private bool _isFireActive;
		private int _currentGunIndex;
		
		private Transform _transform;

		public HealthComponent HealthComponent => _healthComponent;
		
		public void Init(IPlayerData playerData, IGunData[] guns, IBulletSpawner bulletSpawner, SceneBounds sceneBounds)
		{
			_playerData = playerData;
			_guns = guns;
			_transform = transform;
			_sceneBounds = sceneBounds;
			_currentGunIndex = 0;
			_gunView.Init(bulletSpawner, _hitSide);
			_healthComponent.Init(playerData.MaxHp, playerData.Defence);

			if (_guns == null || _guns.Length == 0)
			{
				Debug.LogError("Guns are not specified!", this);
				return;
			}
			
			SetActiveGun(_currentGunIndex);
		}

		public void SetMoveDirection(Vector2 moveDirection)
		{
			_moveDirection = moveDirection;
		}

		public void SetRotateDirection(Vector2 rotateDirection)
		{
			_rotateDirection = rotateDirection;
		}

		public void SetFireState(bool isFirActive)
		{
			_isFireActive = isFirActive;
		}

		public void SetNextGun()
		{
			_currentGunIndex++;
			if (_currentGunIndex >= _guns.Length)
				_currentGunIndex = 0;
			
			SetActiveGun(_currentGunIndex);
		}

		public void SetPreviousGun()
		{
			_currentGunIndex--;
			if (_currentGunIndex < 0)
				_currentGunIndex = _guns.Length - 1;
			
			SetActiveGun(_currentGunIndex);
		}

		private void Update()
		{
			Move();
			Rotate();
			Fire();
		}

		private void Move()
		{
			if (_moveDirection == Vector2.zero)
				return;

			var positionOffset = _rotateRoot.up * (Time.deltaTime * _playerData.MoveSpeed * _moveDirection.y);
			var nextPoint = _transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f);
			
			if (!_sceneBounds.IsOutOfBounds(nextPoint))
				_transform.position = nextPoint;
		}

		private void Rotate()
		{
			if (_rotateDirection == Vector2.zero)
				return;

			var rotateValue = -1 * Time.deltaTime * _playerData.RotateSpeed * _rotateDirection.x;
			_rotateRoot.rotation *= Quaternion.Euler(0f, 0f, rotateValue);
		}

		private void Fire()
		{
			if (!_isFireActive)
				return;
			
			_gunView.Fire();
		}

		private void SetActiveGun(int gunIndex)
		{
			var gunData = _guns[gunIndex];
			_gunView.SetGun(gunData);
		}
	}
}