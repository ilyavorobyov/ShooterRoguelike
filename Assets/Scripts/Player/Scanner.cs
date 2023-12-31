using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent (typeof(Mover))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _weapon;

    private Mover _mover;
    private float _range = 3;
    private Coroutine _searchEnemy;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
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

    private void Update()
    {
        var actualForward = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
    }
    private void StopSearchEnemy()
    {
        if( _searchEnemy != null )
            StopCoroutine( _searchEnemy );
    }

    private IEnumerator TrySearchEnemy()
    {
        float iterationTime = 0.5f;
        var waitForSeconds = new WaitForSeconds(iterationTime);
        bool isScanning = true;
        Enemy enemy;

        while(isScanning)
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
                _weapon.TryShoot(enemy.transform);
                _mover.SetRotationTarget(enemy.transform);
            }
            else
            {
                _mover.SetRotationMoveDirection();
            }

            yield return waitForSeconds;
        }
    }
}