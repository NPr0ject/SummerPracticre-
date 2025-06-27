using System.Reflection;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }
    public IEnumerable<string> GetPublicMethods()
    {
        return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(m => m.Name).Distinct();
    }
    public IEnumerable<string> GetMethodParams(string methodname)
    {
        return _type.GetMethods()
            .Where(m => m.Name == methodname)
            .SelectMany(m => m.GetParameters())
            .Select(p => p.Name)
            .Distinct()!;
    }

    public IEnumerable<string> GetAllFields()
    {
        return _type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Select(method => method.Name);
    }

    public IEnumerable<string> GetProperties()
    {
        
        return _type.GetProperties().Select(pr => pr.Name);
    }
    public bool HasAttribute<T>() where T : Attribute
    {
        return _type.GetCustomAttribute<T>(inherit: true) != null;
    }

}

