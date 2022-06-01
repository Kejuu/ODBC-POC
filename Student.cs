public class Student
{
    public string Level;
    public string Id;
    public string Name;
    public string Last_Name;
    public string Email;

    public override string ToString()
    {
        return $"Id: {Id}\n Name: {Name}\n Last Name: {Last_Name}\n Email: {Email} ";
    }
}