using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private float _speed = 7;
    private Transform _target;

    public float Damage { get; private set; }

    private void Update()
    {
        if (_target != null && _target.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, _target.position, _speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(float damage, Transform target)
    {
        Damage = damage;
        _target = target;
    }

    protected void StopMovement()
    {
        _speed = 0;
    }
}