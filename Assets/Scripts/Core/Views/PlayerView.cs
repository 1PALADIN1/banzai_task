using Core.Data.Player;
using UnityEngine;

namespace Core.Views
{
	public sealed class PlayerView : MonoBehaviour
	{
		private IPlayerData _playerData;
		private Vector2 _moveDirection = Vector2.zero;
		private Vector2 _rotateDirection = Vector2.zero;
		private bool _isFireActive;
		private Transform _transform;
		
		public void Init(IPlayerData playerData)
		{
			_playerData = playerData;
			_transform = transform;
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

			var positionOffset = _transform.up * (Time.deltaTime * _playerData.MoveSpeed * _moveDirection.y);
			_transform.position += new Vector3(positionOffset.x, positionOffset.y, 0f);
		}

		private void Rotate()
		{
			if (_rotateDirection == Vector2.zero)
				return;


			var rotateValue = -1 * Time.deltaTime * _playerData.RotateSpeed * _rotateDirection.x;
			transform.rotation *= Quaternion.Euler(0f, 0f, rotateValue);
		}

		private void Fire()
		{
			if (!_isFireActive)
				return;
			
		}
	}
}