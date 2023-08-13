namespace Axvemi.Commons.Modules;

public interface IProcessor<T>
{
	ModuleController<T> ModuleController { get; set; }
}