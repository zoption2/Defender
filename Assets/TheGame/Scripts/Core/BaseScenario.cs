namespace TheGame
{
    public interface IScenario
    {
        void StartScenario();
    }

    public abstract class BaseScenario : StateMachine, IScenario
    {
        public BaseScenario()
        {

        }

        public void StartScenario()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DefaultScenario : BaseScenario
    {

    }


    public class StateMachine
    {
        public IState State { get; private set; }
        public void ChangeState(IState state)
        {
            State?.OnStateExit();
            State = state;
            state.OnStateEnter();
        }
    }

    public interface IState
    {
        public void Init(IScenario scenario);
        public void OnStateEnter();
        public void OnStateProcesing();
        public void OnStateExit();
    }

    public abstract class State<TScenario> : IState where TScenario : IScenario
    {
        protected TScenario _scenario;

        public void Init(IScenario scenario)
        {
            _scenario = (TScenario)scenario;
        }

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateProcesing()
        {
        }

        public virtual void OnStateExit()
        {
        }
    }

    public class StartingState : Sta
}

