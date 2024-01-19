using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string AttackAnimationName = "Attack";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(AttackAnimationName);
    }
}