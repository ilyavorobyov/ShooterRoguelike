using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _speed = 8;

    public int Damage { get; private set; }

    public void Init(int damage, Vector3 moveDirection)
    {
        Damage = damage;
        _moveDirection = moveDirection;
    }

    private void Update()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }
}