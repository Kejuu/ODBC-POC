using System.Data;
using System.Data.Odbc;
Random rnd = new Random();
OdbcConnection cnn = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\Json\Students1.accdb;Uid=Admin;Pwd=;");

var studentToInsert = new Student
{
    Id = rnd.Next(999).ToString(),
    Email = "test@test.test",
    Name = "Nombre",
    Last_Name = "Apellido",
    Level = "test level"
};
CreateStudent(studentToInsert);
Console.WriteLine("Get All");
GetAllStudents();
Console.WriteLine("Get By Id");
GetStudentById(99);
var studentToUpdate = new Student
{
    Id = "99",
    Email = "test@test.test",
    Name = "Nombre",
    Last_Name = "Apellido",
    Level = "test level"
};
UpdateStudentById(studentToUpdate);
DeleteStudentById(76);

void DeleteStudentById(int id)
{
    using (OdbcCommand cmd = cnn.CreateCommand())
    {
        List<Student> students = null;
        cnn.Open();
        cmd.CommandText = $"DELETE [Students.*] FROM Students WHERE Students.[Student ID] = '{id}'";
        cmd.ExecuteNonQuery();
        cnn.Close();

    }
}

void UpdateStudentById(Student student)
{
    using (OdbcCommand cmd = cnn.CreateCommand())
    {
        List<Student> students = null;
        cnn.Open();
        cmd.CommandText = $"UPDATE Students SET Students.[First Name] = '{student.Name}', Students.[Last Name] = '{student.Last_Name}', Students.[Student ID] = {student.Id}, Students.[E-mail Address] = '{student.Email}', Students.[Level] = '{student.Level}' WHERE Students.[Student ID] = '{student.Id}';";
        cmd.ExecuteNonQuery();
        cnn.Close();

    }
}
void CreateStudent(Student student)
{
    using (OdbcCommand cmd = cnn.CreateCommand())
    {
        List<Student> students = null;
        cnn.Open();
        cmd.CommandText = $"INSERT INTO Students (Students.[First Name], Students.[Last Name], Students.[Student ID], Students.[E-mail Address], Students.[Level]) VALUES ('{student.Name}', '{student.Last_Name}', {student.Id}, '{student.Email}', '{student.Level}')";
        cmd.ExecuteNonQuery();
        cnn.Close();

    }
}

void GetAllStudents()
{
    using (OdbcCommand cmd = cnn.CreateCommand())
    {
        List<Student> students = new List<Student>();
        cnn.Open();
        cmd.CommandText = "SELECT Students.[First Name], Students.[Last Name], Students.[E-mail Address], Students.[Student ID], Students.[Level] FROM Students;";
        
        using (OdbcDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                students.Add(new Student { 
                    Name = reader.GetString("First Name"),
                    Last_Name = reader.GetString("Last Name"),
                    Email = reader.IsDBNull("E-mail Address") ? null : reader.GetString("E-mail Address"),
                    Id = reader.IsDBNull("Student ID") ? null : reader.GetString("Student ID"),
                    Level = reader.IsDBNull("Level") ? null : reader.GetString("Level")
                });
            }
        }
        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
    }
    cnn.Close();
    }

//}

void GetStudentById(int id)
{
    using (OdbcCommand cmd = cnn.CreateCommand())
    {
        Student student = null;
        cmd.CommandText = $"SELECT Students.[First Name], Students.[Last Name], Students.[E-mail Address], Students.[Student ID], Students.[Level] FROM Students WHERE Students.[Student ID] = '{id}';";

        cnn.Open();
        using (OdbcDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                student = new Student
                {
                    Name = reader.GetString("First Name"),
                    Last_Name = reader.GetString("Last Name"),
                    Email = reader.IsDBNull("E-mail Address") ? null : reader.GetString("E-mail Address"),
                    Id = reader.IsDBNull("Student ID") ? null : reader.GetString("Student ID"),
                    Level = reader.IsDBNull("Level") ? null : reader.GetString("Level")
                };            
            }
        }
        Console.WriteLine(student.ToString());
        cnn.Close();
    }

}