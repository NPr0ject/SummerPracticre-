using System;
using System.Collections.Generic;
using task13;
using Xunit;

public class StudentTests
{
    [Fact]
    public void ShoudReturnCorrectResult()
    {
        var student = new Student
        {
            FirstName = "Mark",
            LastName = "Ivanov",
            BirthDate = new DateTime(2007, 3, 28),
            Grades = new List<Subject>
            {
                new Subject { Name = "Physics", Grade = 92 },
                new Subject { Name = "Russian", Grade = 70 }
            }
        };

        var serializer = new SerializerDeserializer();
        var tempfile = Path.GetTempFileName();
        serializer.SerializerSafeFile(student,tempfile);
        var result = serializer.DeserializerLoadFile(tempfile);

        Assert.Equal(student.FirstName, result.FirstName);
        Assert.Equal(student.LastName, result.LastName);
        Assert.Equal(student.BirthDate, result.BirthDate);
        Assert.Equal(2, result.Grades!.Count);
        Assert.Equal("Physics", result.Grades[0].Name);
        Assert.Equal(92, result.Grades[0].Grade);
        Assert.Equal("Russian", result.Grades[1].Name);
        Assert.Equal(70, result.Grades[1].Grade);
    }
}
