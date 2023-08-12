using System;
using System.Collections.Generic;

namespace Axvemi.Commons.Modules;

public class ModuleController
{
	
	private List<IModule> _modules;
	private List<IProcessor> _processors;
	
	public ModuleController(List<IModule> modules, List<IProcessor> processors)
	{
		_modules = modules;
		_processors = processors;

		foreach (IModule module in _modules)
		{
			module.ModuleController = this;
		}

		foreach (IProcessor processor in _processors)
		{
			processor.ModuleController = this;
		}
	}
	
	public TU GetModuleOfType<TU>()
	{
		foreach (IModule module in _modules)
		{
			if (module is TU moduleResult) return moduleResult;
		}

		throw new Exception($"No module of Type {typeof(TU)} found");
	}

	public TU GetProcessorOfType<TU>()
	{
		foreach (IProcessor processor in _processors)
		{
			if (processor is TU moduleResult) return moduleResult;
		}

		throw new Exception($"No processor of Type {typeof(TU)} found");
	}
}