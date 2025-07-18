using task17;

namespace task17tests;

public class UnitTest1
{
    [Fact]
    public void SoftStop_ShouldStopAfterAllCommands()
    {
        ServerThread serverThread = new ServerThread();
        for (int i = 0; i < 5; i++)
        {
            serverThread.commandQueue.Add(new WriteCommand());
        }
        serverThread.commandQueue.Add(new SoftStop(serverThread));
        serverThread.thread.Join();

        Assert.False(serverThread.active);
        Assert.True(serverThread.SoftStopRequest && serverThread.commandQueue.Count == 0);
    }

    [Fact]
    public void HardStop_ShouldStopImmediately()
    {
        ServerThread serverThread = new ServerThread();
        for (int i = 0; i < 5; i++)
        {
            serverThread.commandQueue.Add(new WriteCommand());
        }
        serverThread.commandQueue.Add(new HardStop(serverThread));
        serverThread.thread.Join();

        Assert.False(serverThread.active);
    }

    [Fact]
    public void Exception_Test()
    {
        ServerThread serverThread = new ServerThread();
        serverThread.commandQueue.Add(new ExceptionCommand());
        serverThread.commandQueue.Add(new SoftStop(serverThread));
        serverThread.thread.Join();
        Assert.False(serverThread.active);
        Assert.True(serverThread.SoftStopRequest && serverThread.commandQueue.Count == 0);
    }
}