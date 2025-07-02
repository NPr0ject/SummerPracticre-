using System.Reflection;

namespace task09
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string Path = args[0];
            if (!File.Exists(Path))
            {
                Console.WriteLine($"File not found");
                return;
            }

            Assembly assembly = Assembly.LoadFrom(Path);

            Console.WriteLine($"Metadata:\n{assembly.FullName}");

            Type[] types = assembly.GetTypes();

            foreach (Type type in types.OrderBy(t => t.Name))
            {
                if (!type.IsPublic)
                    continue;

                Console.WriteLine($"\nClass:\n{type.Name}");

                PrintConstructors(type);
                PrintMethods(type);
                PrintProperties(type);
                PrintFields(type);
            }
        }

        static void PrintConstructors(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (constructors.Length == 0)
                return;

            Console.WriteLine("\nConstuctors:");
            foreach (ConstructorInfo c in constructors)
            {
                Console.Write($"{type.Name}");
                ParameterInfo[] parameters = c.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                }
            }
        }

        static void PrintMethods(Type type)
        {
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName)
                .ToArray();

            if (methods.Length == 0)
                return;

            Console.WriteLine("\nMethods:");
            foreach (MethodInfo method in methods.OrderBy(m => m.Name))
            {
                Console.Write($"{method.ReturnType.Name} {method.Name}(");
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                }
                Console.WriteLine(")");
            }
        }

        static void PrintProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (properties.Length == 0)
                return;

            Console.WriteLine("\nProperties:");
            foreach (PropertyInfo prop in properties.OrderBy(p => p.Name))
            {
                Console.WriteLine($"{prop.PropertyType.Name} {prop.Name} {{ " +
                    $"{(prop.CanRead ? "get; " : "")}" +
                    $"{(prop.CanWrite ? "set; " : "")}}}");
            }
        }

        static void PrintFields(Type type)
        {
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (fields.Length == 0)
                return;

            Console.WriteLine("\nFiedlds:");
            foreach (FieldInfo field in fields.OrderBy(f => f.Name))
            {
                Console.WriteLine($"{field.FieldType.Name} {field.Name}");
            }
        }
    }
}