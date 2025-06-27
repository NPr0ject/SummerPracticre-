using Xunit;
using task05;

public class customAttribute : Attribute
{

}

[customAttribute]
public class TestClass
{
    private int _privateField;
    public int PublicField;
    public int Property { get; set; }

    public void Method(int a) { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectMethodParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var param = analyzer.GetMethodParams("Method");

        Assert.Contains("a", param);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectMethodProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties();

        Assert.Contains("Property", properties);
    }

    [Fact]
    public void HasAttribute_ReturnsCorrectBoolSequence()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var isattribute = analyzer.HasAttribute<customAttribute>();

        Assert.True(isattribute);
    }
}

