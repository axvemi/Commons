namespace Axvemi.Commons.StateMachines;

public abstract class BaseState<T>
{
	protected IStateMachine<T> StateMachine { get; }
	protected StateFactory<T> Factory { get; }

	protected T Context => StateMachine.Context;

	private BaseState<T> _currentSuperstate;
	private BaseState<T> _currentSubstate;

	public BaseState(IStateMachine<T> iStateMachine, StateFactory<T> factory)
	{
		StateMachine = iStateMachine;
		Factory = factory;

		//Those methods can't depend on a parameter that gets init in this constructor.
		EnterState();
		InitializeSubstate();
	}

	public virtual void Update(double delta)
	{
		_currentSubstate?.Update(delta);
	}

	/// <summary>
	/// Switch to another state
	/// </summary>
	/// <param name="next"></param>
	public void SwitchState(BaseState<T> next)
	{
		ExitState();
		next.EnterState();
		if (_currentSuperstate == null)
		{
			StateMachine.CurrentState = next;
		}
		else
		{
			_currentSuperstate.SetSubState(next);
		}

		StateMachine.OnChangeState?.Invoke(this, next);
	}

	protected abstract string GetStateName();

	/// <summary>
	/// Call on the constructor.
	/// Used to set a substate if we have to. (ie. Grounded -> Idle/Walk)
	/// </summary>
	protected virtual void InitializeSubstate()
	{
	}

	/// <summary>
	/// Init substate and itself
	/// </summary>
	protected virtual void EnterState()
	{
		_currentSubstate?.EnterState();
	}

	/// <summary>
	/// De-init substate and itself
	/// </summary>
	protected virtual void ExitState()
	{
		_currentSubstate?.ExitState();
	}

	protected void SetSuperState(BaseState<T> newSuperstate)
	{
		_currentSuperstate = newSuperstate;
	}

	protected void SetSubState(BaseState<T> newSubstate)
	{
		_currentSubstate = newSubstate;
		newSubstate.SetSuperState(this);
	}

	public override string ToString()
	{
		string substateName = _currentSubstate == null ? "" : " > " + _currentSubstate;
		return GetStateName() + substateName;
	}
}