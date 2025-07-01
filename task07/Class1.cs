using System.Reflection;
namespace task07;


public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; set; }
    public DisplayNameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

public class VersionAttribute : Attribute
{
    public int Major { get; }
    public int Minor { get; }
    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}

[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    [DisplayName("Тестовый метод")]
    public int TestMethod() { return 1; }
    
    [DisplayName("Числовое свойство")]
    public int Number { get; }
}

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Console.WriteLine("Name:");
        if (displayNameAttr != null)
        {
            Console.WriteLine($"{displayNameAttr.DisplayName}");
        }
        else
        {
            Console.WriteLine("No attribute DisplayName");
        }
        Console.WriteLine("Version:");
        if (attribute != null)
        {
            Console.WriteLine($"{attribute.Major},{attribute.Minor}");
        }
        else
        {
            Console.WriteLine("No version attribute");
        }
        Console.WriteLine("Methods and properties:");
        foreach (var a in type.GetProperties())
        {
            if (a != null)
            {
                Console.WriteLine(a.GetCustomAttribute<DisplayNameAttribute>()!.DisplayName);
            }
            else
            {
                Console.WriteLine("No methodAttribute");
            }
        }
        foreach (var a in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName &&
            !m.Name.StartsWith("get_") &&
            !m.Name.StartsWith("set_") &&
            m.DeclaringType != typeof(object)))
        {
            if (a != null)
            {
                Console.WriteLine(a.GetCustomAttribute<DisplayNameAttribute>()!.DisplayName);
            }
            else
            {
                Console.WriteLine("No methodAttribute");
            }
        }
    }
}