public class EnemyHealth : Health
{
    public override void Die()
    {
        gameObject.SetActive(false);
    }
}