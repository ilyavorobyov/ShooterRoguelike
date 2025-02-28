using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;

        private PlayerMover _mover;
        private Transform _target;
        private bool _isEnemyVisible;
        private Vector3 _moveDirection;
        private Vector3 _startRotation = Vector3.zero;

        private void Awake()
        {
            _mover = GetComponent<PlayerMover>();
        }

        private void OnEnable()
        {
            _gameUI.GameBeguned += OnReset;
            _gameUI.GameReseted += OnReset;
        }

        private void OnDisable()
        {
            _gameUI.GameBeguned -= OnReset;
            _gameUI.GameReseted -= OnReset;
        }

        private void FixedUpdate()
        {
            _moveDirection = _mover.MoveDirection;
            TryLookAtTarget();
        }

        public void SetRotationTarget(Transform target)
        {
            _target = target;
            _isEnemyVisible = true;
        }

        public void SetMoveDirectionRotation()
        {
            _isEnemyVisible = false;
        }

        private void TryLookAtTarget()
        {
            if (_isEnemyVisible)
                transform.LookAt(_target);
            else if (_moveDirection != Vector3.zero)
                transform.forward = _moveDirection;
        }

        private void OnReset()
        {
            transform.rotation = Quaternion.Euler(_startRotation);
        }
    }
}