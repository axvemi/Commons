namespace Axvemi.Commons.Modules;

public interface IModule<T>
{
    ModuleController<T> ModuleController { get; set; }

    void OnModulesReady();
}