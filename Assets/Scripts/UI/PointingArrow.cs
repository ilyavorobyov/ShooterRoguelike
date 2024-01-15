using UnityEngine;
using UnityEngine.UI;

public class PointingArrow : MonoBehaviour
{
    [SerializeField] private Image _arrow;
    [SerializeField] private Transform _perkChoisePlace;
    [SerializeField] private Transform _tokenSpawnPoint;

    private Transform _target;
    private Camera _camera;

    private void OnEnable()
    {
        PlayerCollisionHandler.TokenTaked += OnTokenTaked;
        GameUI.GameReseted += OnHide;
        GameUI.MenuWented += OnHide;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.TokenTaked -= OnTokenTaked;
        GameUI.GameReseted -= OnHide;
        GameUI.MenuWented -= OnHide;
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
            var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
            _arrow.transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
    }

    public void PointTokenSpawn()
    {
        gameObject.SetActive(true);
        _target = _tokenSpawnPoint;
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