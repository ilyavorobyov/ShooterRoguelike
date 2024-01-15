using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private float _speed = 7;
    private Transform _target;

    public int Damage { get; private set; }

    public void Init(int damage, Transform target)
    {
        Damage = damage;
        _target = target;
    }

    private void Update()
    {
        if (_target != null && _target.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _target.position, _speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}