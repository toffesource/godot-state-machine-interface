using Godot;

namespace StateMachine
{
    public class StateMachine : Node, IStateMachine
    {
        [Export] public sbyte PreviousState { get; private set; }
        [Export] public sbyte State { get; private set; }

        public virtual void _EnterState(sbyte newState, sbyte oldState) { }
        public virtual void _ExitState(sbyte oldState, sbyte newState) { }

        /// <summary>
        /// State transition code block called during physics update.
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public virtual sbyte _GetTransition(float delta) { return -1; }

        /// <summary>
        /// Statemachine class physics update.
        /// </summary>
        /// <param name="delta"></param>
        public virtual void _StateLogic(float delta) { }

        /// <summary>
        /// Set new state.
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(sbyte newState)
        {
            PreviousState = State;
            State = newState;

            if (PreviousState >= 0)
            {
                _ExitState(PreviousState, newState);
            }
            if (newState >= 0)
            {
                _EnterState(newState, PreviousState);
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            _StateLogic(delta);
            if (State >= 0)
            {
                _StateLogic(delta);
                sbyte transition = _GetTransition(delta);
                if (transition >= 0)
                {
                    SetState(transition);
                }
            }
        }
    }
}
