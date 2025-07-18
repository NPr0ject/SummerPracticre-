using task17;

namespace task17tests;

public class UnitTest1
{
    [Fact]
    public void TestCommand()
    {
        var scheduler = new Scheduler();
        var serverThread = new ServerThread(scheduler);
        for (int i = 0; i < 5; i++)
        {
            serverThread.commandQueue.Add(new LongCommand(new TestCommand(i + 1), scheduler, 3));
        }
        serverThread.commandQueue.Add(new HardStop(serverThread));
        serverThread.thread.Join();
        Assert.False(serverThread.active);
        Assert.Empty(serverThread.commandQueue);
        Assert.False(scheduler.HasCommand());
    }
}