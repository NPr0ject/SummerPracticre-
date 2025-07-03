using PluginHost;
using Plugin1;

namespace Plugin2;

[PluginLoad]
[DependsOn(typeof(plugin1))]
public class plugin2
{
    public void Execute() => Console.WriteLine("2");
}
