using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rotator))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private WeaponPlacement _weaponPlacement;

    private Rotator _rotator;
    private float _range = 3f;
    private Coroutine _searchEnemy;

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
    }

    private void Start()
    {
        StopSearchEnemy();
        _searchEnemy = StartCoroutine(TrySearchEnemy());
    }

    private void OnDestroy()
    {
        StopSearchEnemy();
    }

    private void StopSearchEnemy()
    {
        if (_searchEnemy != null)
            StopCoroutine(_searchEnemy);
    }

    private IEnumerator TrySearchEnemy()
    {
        float iterationTime = 0.3f;
        var waitForSeconds = new WaitForSeconds(iterationTime);
        bool isScanning = true;
        Enemy enemy;

        while (isScanning)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _range);
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
                Vector3.Distance(enemy.transform.position, transform.position)).FirstOrDefault();
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