using UnityEngine;

public class ScreenOrientationTracker : MonoBehaviour
{
    private Camera _camera;
    private float _landscapeFieldOfViewValue = 40;
    private float _portraitFieldOfViewValue = 50;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnRectTransformDimensionsChange()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        if (Screen.width > Screen.height)
        {
            _camera.fieldOfView = _landscapeFieldOfViewValue;
        }
        else
        {
            _camera.fieldOfView = _portraitFieldOfViewValue;
        }
    }
}