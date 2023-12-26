using UnityEngine;

public class BulletsInfoText : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if( _camera != null )
        {
            transform.LookAt(_camera.transform);
            transform.rotation = _camera.transform.rotation;
        }
    }
}