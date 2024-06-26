using System;

namespace Axvemi.Commons;

public interface IStateMachine<T>
{
	public class StateChangedEventArgs : EventArgs
	{
		BaseState<T> PreviousState;
		BaseState<T> CurrentState;

		public StateChangedEventArgs(BaseState<T> previousState, BaseState<T> currentState)
		{
			this.PreviousState = previousState;
			this.CurrentState = currentState;
		}
	}

	public T Context { get; }
	public BaseState<T> CurrentState { get; set; }
	public EventHandler<StateChangedEventArgs> StateChanged { get; }
}