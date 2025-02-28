using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(HealthView))]
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] private float _startMaxHealth;
        [SerializeField] private AudioSource _hitSound;

        private float _currentHealth;

        private HealthView _healthView;
        private float _minHealth = 0;
        private float _currentMaxHealth;

        public float CurrentMaxHealth => _currentMaxHealth;

        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            _healthView = GetComponent<HealthView>();
            _currentMaxHealth = _startMaxHealth;
        }

        private void OnEnable()
        {
            _currentHealth = _currentMaxHealth;
            _healthView.SetInfo();
        }

        private void OnDisable()
        {
            OnReset();
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage > 0)
            {
                _currentHealth -= damage;
                _healthView.SetInfo();
                _hitSound.PlayDelayed(0);

                if (_currentHealth <= _minHealth)
                    Die();
            }
        }

        public virtual void Add(float addingHealth)
        {
            if (_currentHealth + addingHealth <= _currentMaxHealth)
            {
                _currentHealth += addingHealth;
            }
            else
            {
                _currentHealth = _currentMaxHealth;
            }

            _healthView.SetInfo();
        }

        public abstract void Die();

        public virtual void OnReset()
        {
            _currentMaxHealth = _startMaxHealth;
            _currentHealth = _startMaxHealth;
            _healthView.SetInfo();
        }

        protected void IncreaseMax(int addedHealth)
        {
            _currentMaxHealth += addedHealth;
            Add(addedHealth);
            _healthView.SetInfo();
        }
    }
}