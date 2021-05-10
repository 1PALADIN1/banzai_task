using UnityEngine;

namespace Core.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public Vector2 Position => _camera.transform.position;
        
        public (float height, float width) GetCameraSize()
        {
            var height = 2f * _camera.orthographicSize;
            var width = height * _camera.aspect;

            return (height, width);
        }
    }
}