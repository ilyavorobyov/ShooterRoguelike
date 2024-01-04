public class LiftableBullet : LiftableObject
{
    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}