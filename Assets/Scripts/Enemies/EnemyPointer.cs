using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyPointer : MonoBehaviour
    {
        private const int LeftSideIndex = 0;
        private const int RightSideIndex = 1;
        private const int DownSideIndex = 2;
        private const int UpSideIndex = 3;

        [SerializeField] private PointerIcon _pointIconSample;
        [SerializeField] private Canvas _canvasPointerIcon;

        private Player _player;
        private Camera _camera;
        private Coroutine _pointArrow;
        private PointerIcon _pointIcon;
        private bool _isRenderedIcon = true;
        private Vector3 _leftSideRotation = new Vector3(0, 0, 90);
        private Vector3 _rightSideRotation = new Vector3(0, 0, -90);
        private Vector3 _downSideRotation = new Vector3(0, 0, 180);
        private Vector3 _upSideRotation = new Vector3(0, 0, 0);

        private void Awake()
        {
            _camera = Camera.main;
            _pointIcon = Instantiate(_pointIconSample, _canvasPointerIcon.transform);
        }

        private void OnEnable()
        {
            _isRenderedIcon = true;

            if (_player != null)
            {
                if (_pointArrow != null)
                {
                    StopCoroutine(_pointArrow);
                }

                _pointArrow = StartCoroutine(PointArrow());
            }
        }

        private void OnDisable()
        {
            _isRenderedIcon = false;

            if (_pointArrow != null)
            {
                StopCoroutine(_pointArrow);
            }
        }

        private void OnBecameInvisible()
        {
            _pointIcon.gameObject.SetActive(true);
        }

        private void OnBecameVisible()
        {
            _pointIcon.gameObject.SetActive(false);
        }

        public void Init(Player player)
        {
            _player = player;
        }

        private Quaternion GetIconRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case LeftSideIndex:
                    return Quaternion.Euler(_leftSideRotation);
                case RightSideIndex:
                    return Quaternion.Euler(_rightSideRotation);
                case DownSideIndex:
                    return Quaternion.Euler(_downSideRotation);
                case UpSideIndex:
                    return Quaternion.Euler(_upSideRotation);
            }

            return Quaternion.identity;
        }

        private IEnumerator PointArrow()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            int planeIndex = 0;
            int sidesNumber = 4;
            Vector3 directionFromPlayer;
            Vector3 worldPosition;
            Plane[] planes;
            Ray ray;

            while (_isRenderedIcon)
            {
                if (_pointIcon.isActiveAndEnabled)
                {
                    directionFromPlayer = transform.position - _player.transform.position;
                    ray = new Ray(_player.transform.position, directionFromPlayer);
                    planes = GeometryUtility.CalculateFrustumPlanes(_camera);
                    float minDistance = Mathf.Infinity;

                    for (int i = 0; i < sidesNumber; i++)
                    {
                        if (planes[i].Raycast(ray, out float distance))
                        {
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                planeIndex = i;
                            }
                        }
                    }

                    worldPosition = ray.GetPoint(minDistance);
                    _pointIcon.transform.position = _camera.WorldToScreenPoint(worldPosition);
                    _pointIcon.transform.rotation = GetIconRotation(planeIndex);
                }

                yield return waitForFixedUpdate;
            }
        }
    }
}