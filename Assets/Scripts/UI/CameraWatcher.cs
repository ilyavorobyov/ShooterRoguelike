using UnityEngine;

namespace UI
{
    public class CameraWatcher : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform.position);
            transform.rotation = _camera.transform.rotation;
        }
    }
}