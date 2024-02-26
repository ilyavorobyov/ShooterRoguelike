using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Player.Weapon;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerRotator))]
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private WeaponPlacement _weaponPlacement;

        private PlayerRotator _rotator;
        private float _range = 3f;
        private float _iterationTime = 0.3f;
        private bool _isScanning = false;
        private Coroutine _searchEnemy;

        private void Awake()
        {
            _rotator = GetComponent<PlayerRotator>();
        }

        private void Start()
        {
            StopSearchEnemy();
            _isScanning = true;
            _searchEnemy = StartCoroutine(TrySearchEnemy());
        }

        private void OnDestroy()
        {
            StopSearchEnemy();
        }

        private void StopSearchEnemy()
        {
            if (_searchEnemy != null)
            {
                _isScanning = false;
                StopCoroutine(_searchEnemy);
            }
        }

        private IEnumerator TrySearchEnemy()
        {
            var waitForSeconds = new WaitForSeconds(_iterationTime);
            Enemy enemy;

            while (_isScanning)
            {
                Collider[] hitColliders = Physics.OverlapSphere(
                    transform.position,
                    _range);
                List<Enemy> enemiesWithinAbilityRange = new List<Enemy>();

                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.transform.TryGetComponent(out Enemy enemyTemplate))
                    {
                        enemiesWithinAbilityRange.Add(enemyTemplate);
                    }
                }

                if (enemiesWithinAbilityRange.Count > 0)
                {
                    enemy = enemiesWithinAbilityRange.OrderBy(enemy =>
                    Vector3.Distance(
                        enemy.transform.position,
                        transform.position)).
                        FirstOrDefault();
                    _rotator.SetRotationTarget(enemy.transform);
                    _weaponPlacement.TryShoot(enemy.transform);
                }
                else
                {
                    _rotator.SetMoveDirectionRotation();
                }

                yield return waitForSeconds;
            }
        }
    }
}