using System;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UI;

[RequireComponent(typeof(DynamicJoystick))]
public class JoystickMovement : MonoBehaviour
{
    [SerializeField] private Image _joystickHandle;
    [SerializeField] private Image _joystickBackground;

    private DynamicJoystick _joystick;
    private Vector3 _moveDirection;

    public static event Action<Vector3> Moving;

    private void Awake()
    {
        if (Device.IsMobile)
        {
            _joystick = GetComponent<DynamicJoystick>();
        }
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveDirection = new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical);
            _moveDirection.Normalize();
            Moving?.Invoke(_moveDirection);
        }
        else
        {
            Moving?.Invoke(Vector3.zero);
        }
    }

    public void Reset()
    {
        _joystickBackground.gameObject.SetActive(false);
        _joystick.Input = Vector2.zero;
        _moveDirection = Vector3.zero;
        _joystickHandle.rectTransform.localPosition = Vector3.zero;
    }
}