using System.Threading;
using System.Collections.Concurrent;

namespace task17;

public interface IScheduler
{
    bool HasCommand();
    ICommand Select();
    void Add(ICommand cmd);
}
public interface ICommand
{
    void Execute();
}
public class ServerThread
{
    public Scheduler scheduler;
    public Thread thread;
    public BlockingCollection<ICommand> commandQueue;
    public bool active;
    public bool SoftStopRequest;

    public ServerThread(Scheduler scheduler)
    {
        this.scheduler = scheduler;
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
            ICommand command = null!;
            if (scheduler.HasCommand())
            {
                command = scheduler.Select();
            }
            else if (commandQueue.TryTake(out command!))
            {
                if (command is LongCommand)
                {
                    scheduler.Add(command);
                }
            }
            
            if (command != null)
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

public class Scheduler : IScheduler
{
    public ConcurrentQueue<ICommand> commandQ = new ConcurrentQueue<ICommand>();

    public bool HasCommand()
    {
        return !commandQ.IsEmpty;
    }

    public ICommand Select()
    {
        if (commandQ.TryDequeue(out ICommand? command))
        {
            return command;
        }
        throw new InvalidOperationException("Планировщик пуст.");
    }

    public void Add(ICommand command)
    {
        commandQ.Enqueue(command);
    }
}

public class LongCommand : ICommand
{
    public ICommand command;
    public Scheduler scheduler;
    public int executions;

    public LongCommand(ICommand command, Scheduler scheduler, int executions)
    {
        this.command = command;
        this.scheduler = scheduler;
        this.executions = executions;
    }

    public void Execute()
    {
        if(executions > 0)
        {
            command.Execute();
            executions--;
            if(executions > 0) scheduler.Add(this);
        }
    }
}
public class TestCommand(int id) : ICommand
{
    int counter = 0;

    public void Execute()
    {
        Console.WriteLine($"Поток {id} вызов {++counter}");
    }
}