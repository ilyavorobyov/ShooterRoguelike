using System.Collections;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] private PointerIcon _pointIconTransformSample;
    [SerializeField] private Canvas _canvasPointerIcon;

    private Player _player;
    private Camera _camera;
    private Vector3 _directionFromPlayer;
    private bool _isRenderIcon;
    private Coroutine _renderIcon;
    private PointerIcon _pointIconTransform;

    private void Awake()
    {
        _camera = Camera.main;
        _pointIconTransform = Instantiate(_pointIconTransformSample, _canvasPointerIcon.transform);
    }

    private void OnEnable()
    {
        _isRenderIcon = true;

        if(_player != null)
        {
            if (_renderIcon != null)
                StopCoroutine(_renderIcon);

            _renderIcon = StartCoroutine(RenderIcon());
        }
    }

    private void OnDisable()
    {
        _isRenderIcon = false;

        if (_renderIcon != null)
            StopCoroutine(_renderIcon);
    }

    public void Init(Player player)
    {
        _player = player;
    }

    private Quaternion GetIconRotation(int planeIndex)
    {
        switch (planeIndex)
        {
            case 0:
                return Quaternion.Euler(0, 0, 90);
            case 1:
                return Quaternion.Euler(0, 0, -90);
            case 2:
                return Quaternion.Euler(0, 0, 180);
            case 3:
                return Quaternion.Euler(0, 0, 0);
        }

        return Quaternion.identity;
    }

    private IEnumerator RenderIcon()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while(_isRenderIcon)
        {
            _directionFromPlayer = transform.position - _player.transform.position;
            Ray ray = new Ray(_player.transform.position, _directionFromPlayer);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            float minDistance = Mathf.Infinity;
            int planeIndex = 0;

            for (int i = 0; i < 4; i++)
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
            
            minDistance = Mathf.Clamp(minDistance, 0.0f, _directionFromPlayer.magnitude);

            Vector3 worldPosition = ray.GetPoint(minDistance);
            _pointIconTransform.transform.position = _camera.WorldToScreenPoint(worldPosition);
            _pointIconTransform.transform.rotation = GetIconRotation(planeIndex);

            yield return waitForFixedUpdate;
        }
    }

    private void OnBecameInvisible()
    {
        _pointIconTransform.gameObject.SetActive(true);
    }

    private void OnBecameVisible()
    {
        _pointIconTransform.gameObject.SetActive(false);
    }
}