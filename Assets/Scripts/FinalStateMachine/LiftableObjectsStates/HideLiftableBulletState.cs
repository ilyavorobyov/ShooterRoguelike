public class HideLiftableBulletState : State
{
    private LiftableBullet _liftableBullet;

    public HideLiftableBulletState(LiftableBullet liftableBullet)
    {
        _liftableBullet = liftableBullet;
    }

    public override void Enter()
    {
        _liftableBullet.gameObject.SetActive(false);
    }
}