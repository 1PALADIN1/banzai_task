using UnityEngine;

namespace Core.Logic
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private float _moveSpeed;
        private Vector2 _moveDirection;
        private bool _canMove;
        
        public bool IsMoving => _canMove;
        
        public void Init(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public void SetMoveDirection(Vector2 moveDirection)
        {
            _moveDirection = moveDirection.normalized;
        }

        public void StartMoving()
        {
            _canMove = true;
        }

        public void StopMoving()
        {
            _canMove = false;
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;

            _rigidbody2D.velocity = _moveDirection * _moveSpeed;
        }
    }
}