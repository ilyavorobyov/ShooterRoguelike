public class Healer : LiftableObject
{
    public int HealValue { get; private set; } = 10;

    public override void Hide()
    {
        Destroy(gameObject);
    }
}