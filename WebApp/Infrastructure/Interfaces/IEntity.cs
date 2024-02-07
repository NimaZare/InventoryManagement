namespace Infrastructure.Interfaces;

public interface IEntity<T>
{
    public T ID { get; }

    public DateTime InsertDateTime { get; }
}
