namespace StateMachine {
    public interface IStateMachine {
        void _StateLogic (float delta);
        sbyte _GetTransition (float delta);
        void _EnterState (sbyte newState, sbyte oldState);
        void _ExitState (sbyte oldState, sbyte newState);
    }
}