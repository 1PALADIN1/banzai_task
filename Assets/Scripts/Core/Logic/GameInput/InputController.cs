using System;
using UnityEngine;

namespace Core.Logic.GameInput
{
    public sealed class InputController : MonoBehaviour, IInputController
    {
        private const float InputPrecision = 0.001f;
        
        [Header("Tank control")]
        [SerializeField] private KeyCode _moveForwardKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode _moveBackwardKey = KeyCode.DownArrow;
        [SerializeField] private KeyCode _rotateLeftKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _rotateRightKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode _fireKey = KeyCode.X;
        [SerializeField] private KeyCode _changeGunForwardKey = KeyCode.W;
        [SerializeField] private KeyCode _changeGunBackwardKey = KeyCode.Q;

        private Vector2 _prevMoveAxis = Vector2.zero;
        private Vector2 _prevRotateAxis = Vector2.zero;

        public event Action<Vector2> MoveAxisStateChanged; 
        public event Action<Vector2> RotateAxisStateChanged; 

        private void Update()
        {
            MoveTank();
            RotateTank();
            Fire();
            ChangeGun();
        }

        private void MoveTank()
        {
            var moveAxis = Vector2.zero;
            
            if (Input.GetKey(_moveForwardKey))
                moveAxis = Vector2.up;
            
            if (Input.GetKey(_moveBackwardKey))
                moveAxis = Vector2.down;

            if (!IsInputStateChanged(_prevMoveAxis, moveAxis))
                return;

            _prevMoveAxis = moveAxis;
            MoveAxisStateChanged?.Invoke(moveAxis);
        }

        private void RotateTank()
        {
            var rotateAxis = Vector2.zero;
            
            if (Input.GetKey(_rotateLeftKey))
                rotateAxis = Vector2.left;
            
            if (Input.GetKey(_rotateRightKey))
                rotateAxis = Vector2.right;
            
            if (!IsInputStateChanged(_prevRotateAxis, rotateAxis))
                return;

            _prevRotateAxis = rotateAxis;
            RotateAxisStateChanged?.Invoke(rotateAxis);
        }

        private void Fire()
        {
            
        }

        private void ChangeGun()
        {
            
        }

        private bool IsInputStateChanged(Vector2 prevValue, Vector2 newValue)
        {
            if (Mathf.Abs(prevValue.x - newValue.x) > InputPrecision)
                return true;
            
            if (Mathf.Abs(prevValue.y - newValue.y) > InputPrecision)
                return true;

            return false;
        }
    }
}