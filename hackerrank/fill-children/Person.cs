namespace fill_children;

public class Person
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "Unnamed";
    public IList<int>? Children { get; set; } = new List<int>();
}