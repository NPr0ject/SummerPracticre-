using PluginHost;

class Program
{
    static void Main(string[] args)
    {
        var loader = new PluginLoader();
        loader.LoadPlugins(@"Plugins/");
    }
}
