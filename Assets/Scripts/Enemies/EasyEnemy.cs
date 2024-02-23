namespace Enemies
{
    public class EasyEnemy : Enemy
    {
        public override void Attack()
        {
            base.Attack();
            PlayerHealth.TakeDamage(Damage);
        }
    }
}