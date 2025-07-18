using System.Text.RegularExpressions;

namespace task02;

public class Student
{
    public required string Name { get; set; }
    public required string Faculty { get; set; }
    public required List<int> Grades { get; set; }
}

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students) => _students = students;

    public IEnumerable<Student> GetStudentsByFaculty(string faculty)
    => _students.Where(stud => string.Equals(stud.Faculty, faculty, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade) =>
    _students.Where(stud => stud.Grades.Average() >= minAverageGrade);

    public IEnumerable<Student> GetStudentsOrderedByName()
        => _students.OrderBy(stud => stud.Name);

    public ILookup<string, Student> GroupStudentsByFaculty()
        => _students.ToLookup(stud => stud.Faculty);

    public string GetFacultyWithHighestAverageGrade()
        => _students.GroupBy(stud => stud.Faculty).Select(gr => new
        {
            Faculty = gr.Key,
            avgGrade = gr.Average(st => st.Grades.Average())
        }).OrderByDescending(gr => gr.avgGrade).FirstOrDefault()?.Faculty ?? string.Empty;
}
