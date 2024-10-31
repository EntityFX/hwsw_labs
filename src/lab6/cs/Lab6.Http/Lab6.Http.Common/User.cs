namespace Lab6.Http.Common;

public class User
{
    public User()
    {
    }

    public User(int id, string name, bool active, int age)
    {
        Id = id;
        Name = name;
        Active = active;
        Age = age; 
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; }

    public int Age { get; set; }
}
