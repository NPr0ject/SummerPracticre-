using PluginHost;

namespace Plugin1;

[PluginLoad]
public class plugin1
{
    public void Execute() => Console.WriteLine("1");
}
