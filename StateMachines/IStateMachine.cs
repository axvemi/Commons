using System;

namespace Axvemi.Commons.StateMachines;

public interface IStateMachine<T>
{
	public T Context { get; }
	public BaseState<T> CurrentState { get; set; }
	public Action<BaseState<T>, BaseState<T>> OnChangeState { get; }
}