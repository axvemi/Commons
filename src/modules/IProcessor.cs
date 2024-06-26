namespace Axvemi.Commons;

public interface IProcessor<T>
{
    ModuleController<T> ModuleController { get; set; }
}