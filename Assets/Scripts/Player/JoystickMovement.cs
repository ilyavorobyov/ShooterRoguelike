using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Player
{
    [RequireComponent(typeof(DynamicJoystick))]
    public class JoystickMovement : MonoBehaviour
    {
        [SerializeField] private Image _joystickHandle;
        [SerializeField] private Image _joystickBackground;

        private DynamicJoystick _joystick;
        private Vector3 _moveDirection;
        private bool _isMobile;

        public event Action<Vector3> Moving;

        private void Awake()
        {
            _isMobile = YandexGame.EnvironmentData.isMobile;

            if (_isMobile)
            {
                _joystick = GetComponent<DynamicJoystick>();
                _joystick.gameObject.SetActive(true);
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
}