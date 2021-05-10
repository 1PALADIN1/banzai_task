using UnityEngine;

namespace Core.Logic.Scene
{
    public class SceneBounds
    {
        private readonly Vector2 _leftBottomCorner; 
        private readonly Vector2 _rightTopCorner;
        
        public SceneBounds(Vector2 leftBottomCorner, Vector2 rightTopCorner)
        {
            _leftBottomCorner = leftBottomCorner;
            _rightTopCorner = rightTopCorner;
        }

        public bool IsOutOfBounds(Vector2 point)
        {
            var isOutOfX = point.x >= _rightTopCorner.x || point.x <= _leftBottomCorner.x;
            var isOutOfY = point.y >= _rightTopCorner.y || point.y <= _leftBottomCorner.y;
            return isOutOfX || isOutOfY;
        }
    }
}