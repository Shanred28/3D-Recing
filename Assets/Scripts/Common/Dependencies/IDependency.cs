namespace Race
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }
}

