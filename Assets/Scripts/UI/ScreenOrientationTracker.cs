using UnityEngine;

public class ScreenOrientationTracker : MonoBehaviour
{
    private Camera _camera;
    private float _landscapeFiedOfViewValue = 33;
    private float _portraitFiedOfViewValue = 50;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnRectTransformDimensionsChange()
    {
        if(_camera == null)
        {
            _camera = Camera.main;
        }

        if (Screen.width > Screen.height)
        {
            _camera.fieldOfView = _landscapeFiedOfViewValue;
        }
        else
        {
            _camera.fieldOfView = _portraitFiedOfViewValue;
        }
    }
}