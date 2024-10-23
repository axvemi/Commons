using System;

namespace Axvemi.Commons;

public abstract class StateFactory<T>
{
    public IStateMachine<T> StateMachine { get; }

    public StateFactory(IStateMachine<T> stateMachine)
    {
        StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }
}