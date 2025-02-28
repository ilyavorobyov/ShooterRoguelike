using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PointingArrow : MonoBehaviour
    {
        [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
        [SerializeField] private Image _arrow;
        [SerializeField] private Transform _perkChoisePlace;
        [SerializeField] private GameUI _gameUI;

        private int _decreasingAngleValue = 90;
        private Transform _target;
        private Camera _camera;

        private void OnEnable()
        {
            _playerCollisionHandler.TokenTaked += OnTokenTaked;
            _gameUI.GameReseted += OnHide;
            _gameUI.MenuWented += OnHide;
        }

        private void OnDisable()
        {
            _playerCollisionHandler.TokenTaked -= OnTokenTaked;
            _gameUI.GameReseted -= OnHide;
            _gameUI.MenuWented -= OnHide;
        }

        private void Awake()
        {
            _camera = Camera.main;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_target != null)
            {
                var targetPosLocal = _camera.transform.InverseTransformPoint(_target.position);
                var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg;
                targetAngle -= _decreasingAngleValue;
                _arrow.transform.eulerAngles = new Vector3(0, 0, targetAngle);
            }
        }

        public void PointTokenSpawn(Transform tokenSpawnPoint)
        {
            gameObject.SetActive(true);
            _target = tokenSpawnPoint;
        }

        public void OnHide()
        {
            gameObject.SetActive(false);
        }

        private void OnTokenTaked()
        {
            _target = _perkChoisePlace;
        }
    }
}