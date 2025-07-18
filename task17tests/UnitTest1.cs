using task17;

namespace task17tests;

public class UnitTest1
{
    [Fact]
    public void testLongRunningCommand()
    {
        var scheduler = new Scheduler();
        var serverThread = new ServerThread(scheduler);
        var longCommand = new LongCommand(new WriteCommand(), scheduler, 3);
        serverThread.commandQueue.Add(longCommand);
        serverThread.commandQueue.Add(new SoftStop(serverThread));
        serverThread.thread.Join();

        Assert.False(serverThread.active);
        Assert.True(serverThread.SoftStopRequest && serverThread.commandQueue.Count == 0 &&
        !scheduler.HasCommand());
    }
}