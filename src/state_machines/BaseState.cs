namespace Axvemi.Commons;

public abstract class BaseState<T>
{
    protected IStateMachine<T> StateMachine { get; }
    protected StateFactory<T> Factory { get; }

    protected T Context => StateMachine.Context;

    protected BaseState<T> CurrentSuperstate;
    protected BaseState<T> CurrentSubstate;

    public BaseState(IStateMachine<T> iStateMachine, StateFactory<T> factory)
    {
        StateMachine = iStateMachine;
        Factory = factory;
    }

    public virtual void Update(double delta)
    {
        CurrentSubstate?.Update(delta);
    }

    /// <summary>
    /// Switch to another state
    /// </summary>
    /// <param name="next"></param>
    public void SwitchState(BaseState<T> next)
    {
        ExitState();
        if (CurrentSuperstate == null)
        {
            StateMachine.CurrentState = next;
        }
        else
        {
            CurrentSuperstate.SetSubState(next);
        }

        StateMachine.StateChanged?.Invoke(new IStateMachine<T>.StateChangedEventArgs(this, next));
    }

    protected abstract string GetStateName();

    /// <summary>
    /// De-init substate and itself
    /// </summary>
    protected virtual void ExitState()
    {
        CurrentSubstate?.ExitState();
    }

    protected void SetSubState(BaseState<T> newSubstate)
    {
        CurrentSubstate = newSubstate;
        newSubstate.SetSuperState(this);
    }

    private void SetSuperState(BaseState<T> newSuperstate)
    {
        CurrentSuperstate = newSuperstate;
    }

    public override string ToString()
    {
        string substateName = CurrentSubstate == null ? "" : " > " + CurrentSubstate;
        return GetStateName() + substateName;
    }
}