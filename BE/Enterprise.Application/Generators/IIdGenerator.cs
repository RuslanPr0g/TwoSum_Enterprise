namespace Enterprise.Application.Generators;

public interface IIdGenerator<T>
{
    T Generate();
}