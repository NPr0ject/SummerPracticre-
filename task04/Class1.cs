namespace task04;

public interface ISpaceship{
    void MoveForward();
    void Rotate(int angle);
    void Fire();
    int Speed { get; }
    int FirePower { get; }
}
public class Cruiser : ISpaceship
{
    public void MoveForward()
    {
        Console.WriteLine($"Cruiser is moving forward with a speed of {Speed} distance value/time value");
    }
    public void Rotate(int angle)
    {
        Console.WriteLine($"Cruiser turned {angle} radians");
    }
    public void Fire()
    {
        Console.WriteLine($"{FirePower} damage dealt");
    }
    public int Speed => 1;
    public int FirePower => 10;
}

public class Fighter : ISpaceship
{
    public void MoveForward()
    {
        Console.WriteLine($"Fighter is moving forward with a speed of {Speed} distance value/time value");
    }
    public void Rotate(int angle)
    {
        Console.WriteLine($"Fighter turned {angle} radians");
    }
    public void Fire()
    {
        Console.WriteLine($"{FirePower} damage dealt");
    }
    public int Speed => 10;
    public int FirePower => 1;
}

