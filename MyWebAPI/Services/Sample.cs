public interface ISample
{
    int Id { get; }
}

public class Sample : ISample
{
    private static int _counter;
    private int _id;

    public Sample()
    {
        _id = ++_counter;
    }

    public int Id => _id;
}