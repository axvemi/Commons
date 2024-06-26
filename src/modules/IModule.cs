namespace Axvemi.Commons;

public interface IModule<T>
{
    ModuleController<T> ModuleController { get; set; }

    void OnModulesReady();
}