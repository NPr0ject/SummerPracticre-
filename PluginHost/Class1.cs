using System.Reflection;

namespace PluginHost;

public class PluginLoadAttribute : Attribute {}

public class DependsOnAttribute : Attribute
{
    public Type Dependency { get; }
    public DependsOnAttribute(Type dependency)
    {
        Dependency = dependency;
    }
}

public class PluginLoader
{
    private Dictionary<Type, List<Type>> dependencyGraph = new();
    private List<Type> TopologicalSort(Dictionary<Type, List<Type>> graph)
    {
        var result = new List<Type>();
        var visited = new HashSet<Type>();
        var visiting = new HashSet<Type>();

        void Visit(Type node)
        {
            if (visited.Contains(node)) return;
            if (visiting.Contains(node))
                throw new Exception($"asd");

            visiting.Add(node);

            if (graph.TryGetValue(node, out var dependencies))
            {
                foreach (var dep in dependencies)
                {
                    Visit(dep);
                }
            }

            visiting.Remove(node);
            visited.Add(node);
            result.Add(node);
        }

        foreach (var node in graph.Keys)
        {
            Visit(node);
        }

        return result;
    }
    
    public void LoadPlugins(string folderPath)
    {
        var dllFiles = Directory.GetFiles(folderPath, "*.dll");
        var assemblies = dllFiles.Select(Assembly.LoadFrom).ToList();

        foreach (var asm in assemblies)
        {
            foreach (var type in asm.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoadAttribute>() != null)
                {
                    var dependencies = type.GetCustomAttributes<DependsOnAttribute>()
                                           .Select(d => d.Dependency)
                                           .ToList();

                    dependencyGraph[type] = dependencies;
                }
            }
        }

        var sortedPlugins = TopologicalSort(dependencyGraph);

        foreach (var pluginType in sortedPlugins)
        {
            var instance = Activator.CreateInstance(pluginType);
            var executeMethod = pluginType.GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            executeMethod?.Invoke(instance, null);
        }
    }
}
