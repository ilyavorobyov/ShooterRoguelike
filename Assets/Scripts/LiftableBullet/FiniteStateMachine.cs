namespace LiftableBullet
{
    public class FiniteStateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            if (newState != CurrentState)
            {
                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }
    }
}