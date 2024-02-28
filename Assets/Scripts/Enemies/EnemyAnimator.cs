using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private const string Attack = nameof(Attack);

        private readonly int _attackAnimationHash = Animator.StringToHash(nameof(Attack));

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(_attackAnimationHash);
        }
    }
}