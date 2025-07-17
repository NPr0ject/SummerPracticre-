using System.Threading;
using System.Collections.Concurrent;

namespace task17;

public interface ICommand
{
    void Execute();
}
public class ServerThread
{
    public Thread thread;
    public BlockingCollection<ICommand> commandQueue;
    public bool active;
    public bool SoftStopRequest;

    public ServerThread()
    {
        thread = new Thread(Run);
        commandQueue = new BlockingCollection<ICommand>();
        active = true;
        SoftStopRequest = false;
        thread.Start();
    }

    public void Run()
    {
        while (active)
        {
            if (commandQueue.TryTake(out ICommand? command))
            {
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"исключение: {ex.Message}");
                }
            }

            if (SoftStopRequest && commandQueue.Count == 0)
            {
                active = false;
            }
        }
    }
}
public class HardStop : ICommand
{
    public ServerThread serverThread;

    public HardStop(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread != serverThread.thread) 
        throw new InvalidOperationException("!!!HardStop has problem!!!");

        serverThread.active = false;
    }
}
public class SoftStop : ICommand
{
    public ServerThread serverThread;

    public SoftStop(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread != serverThread.thread) 
        throw new InvalidOperationException("!!!SoftStop has problem!!!");
        
        serverThread.SoftStopRequest = true;
    }
}
public class WriteCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("WriteCommand");
    }
}

public class ExceptionCommand : ICommand
{
    public void Execute()
    {
        int x = 1;
        int y = 0;
        Console.WriteLine(x / y);
    }
}
