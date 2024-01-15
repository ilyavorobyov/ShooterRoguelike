public class EasyEnemy : Enemy
{
    public override void Attack()
    {
        PlayerHealth.TakeDamage(Damage);
    }
}