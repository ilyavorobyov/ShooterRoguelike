using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _speed = 4;
    private Transform _target;

    public int Damage { get; private set; }

    public void Init(int damage, Transform target)
    {
        Damage = damage;
        _target = target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            _target.position, _speed * Time.deltaTime);
    }
}