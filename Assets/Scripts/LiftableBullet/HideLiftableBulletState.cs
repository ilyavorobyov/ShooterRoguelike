namespace LiftableBullet
{
    public class HideLiftableBulletState : State
    {
        private LiftableBulletObject _liftableBullet;

        public HideLiftableBulletState(LiftableBulletObject liftableBullet)
        {
            _liftableBullet = liftableBullet;
        }

        public override void Enter()
        {
            _liftableBullet.gameObject.SetActive(false);
        }
    }
}