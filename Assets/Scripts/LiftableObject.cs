using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class LiftableObject : MonoBehaviour
{
    [SerializeField] private float _idleDuration;
    [SerializeField] private float _missingWarningDuration;

    private const string MissingWarningAnimationName = "MissingWarning";

    private Animator _animator;
    private MeshRenderer _meshRenderer; 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Show();
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
        CancelInvoke();
    }

    public abstract void Hide();

    private void Show()
    {
        _meshRenderer.gameObject.SetActive(true);
        Invoke(nameof(ReportMissing), _idleDuration);
    }

    private void ReportMissing()
    {
        _animator.SetTrigger(MissingWarningAnimationName);
        Invoke(nameof(Hide), _missingWarningDuration);
    }
}