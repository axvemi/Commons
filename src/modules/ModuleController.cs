using System;
using System.Collections.Generic;

namespace Axvemi.Commons.Modules;

public class ModuleController<T>
{
    public T Owner { get; }

    protected List<IModule<T>> Modules;
    protected List<IProcessor<T>> Processors;

    public ModuleController(T owner, List<IModule<T>> modules, List<IProcessor<T>> processors)
    {
        Owner = owner;
        Modules = modules;
        Processors = processors;

        foreach (IModule<T> module in Modules)
        {
            module.ModuleController = this;
        }

        foreach (IProcessor<T> processor in Processors)
        {
            processor.ModuleController = this;
        }
    }

    public void Initialize()
    {
        foreach (IModule<T> module in Modules)
        {
            module.OnModulesReady();
        }
    }

    public TU GetModuleOfType<TU>()
    {
        foreach (IModule<T> module in Modules)
        {
            if (module is TU moduleResult) return moduleResult;
        }

        throw new Exception($"No module of Type {typeof(TU)} found");
    }

    public TU GetProcessorOfType<TU>()
    {
        foreach (IProcessor<T> processor in Processors)
        {
            if (processor is TU moduleResult) return moduleResult;
        }

        throw new Exception($"No processor of Type {typeof(TU)} found");
    }
}