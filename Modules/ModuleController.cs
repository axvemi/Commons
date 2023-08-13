using System;
using System.Collections.Generic;

namespace Axvemi.Commons.Modules;

public class ModuleController<T>
{
	public T Owner { get; }
	
	private List<IModule<T>> _modules;
	private List<IProcessor<T>> _processors;
	
	public ModuleController(T owner, List<IModule<T>> modules, List<IProcessor<T>> processors)
	{
		Owner = owner;
		_modules = modules;
		_processors = processors;

		foreach (IModule<T> module in _modules)
		{
			module.ModuleController = this;
		}

		foreach (IProcessor<T> processor in _processors)
		{
			processor.ModuleController = this;
		}
	}
	
	public TU GetModuleOfType<TU>()
	{
		foreach (IModule<T> module in _modules)
		{
			if (module is TU moduleResult) return moduleResult;
		}

		throw new Exception($"No module of Type {typeof(TU)} found");
	}

	public TU GetProcessorOfType<TU>()
	{
		foreach (IProcessor<T> processor in _processors)
		{
			if (processor is TU moduleResult) return moduleResult;
		}

		throw new Exception($"No processor of Type {typeof(TU)} found");
	}
}