using Core.Views;
using UnityEngine;

namespace Core.Logic.Scene
{
    public class SceneController : ISceneController
    {
        private const float PointOffset = 1.5f;
        
        private readonly Vector2 _cameraPosition;
        private readonly float _sceneHalfHeight;
        private readonly float _sceneHalfWidth;

        private readonly Vector2Int[] _pointDirections =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.right,
            Vector2Int.left,
        };

        public SceneController(CameraView cameraView)
        {
            var (height, width) = cameraView.GetCameraSize();
            _sceneHalfHeight = height / 2f;
            _sceneHalfWidth = width / 2f;
            _cameraPosition = cameraView.Position;
        }
        
        public Vector2 GetRandomPointOutOfScene()
        {
            var x = 0f;
            var y = 0f;

            var direction = GetRandomPointDirection();
            if (direction.x != 0)
            {
                x = _cameraPosition.x + direction.x * (PointOffset + _sceneHalfWidth);
                y = Random.Range(_cameraPosition.y - _sceneHalfHeight, _cameraPosition.y + _sceneHalfHeight);
            }

            if (direction.y != 0)
            {
                x = Random.Range(_cameraPosition.x - _sceneHalfWidth, _cameraPosition.x + _sceneHalfWidth);
                y = _cameraPosition.y + direction.y * (PointOffset + _sceneHalfHeight);
            }
            
            return new Vector2(x, y);
        }

        private Vector2Int GetRandomPointDirection()
        {
            var randIndex = Random.Range(0, _pointDirections.Length);
            return _pointDirections[randIndex];
        }
    }
}