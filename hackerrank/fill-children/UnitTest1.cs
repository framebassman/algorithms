using System.Text;
using Xunit.Abstractions;

namespace fill_children;

public class Person
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "Unnamed";
    public IList<int> Children { get; set; } = new List<int>();

    // public override string ToString()
    // {
    //     var children = string.Join(";", Children);
    //     return $"Id: {Id}, ParentId: {ParentId}, Name: {Name}, Children: {children} \n";
    // }
}

public class UnitTest1
{
    private ITestOutputHelper _console;

    public UnitTest1(ITestOutputHelper console) { _console = console; }
    
    // Your code here
    public static IList<Person> FillChildren(IList<Person> persons)
    {
        if (persons.Count == 0) {
            return persons;
        }

        var hashSet = new HashSet<int>();
        foreach (Person parent in persons) {
            if (!hashSet.Add(parent.Id)) {
                throw new Exception();
            }

            foreach (Person child in persons) {
                if (parent.Id == child.ParentId) {
                    if (child.Id == parent.ParentId) {
                        throw new Exception();
                    }
                    
                    parent.Children.Add(child.Id);
                }
            }
        }
        
        return persons;
    }

    [Fact]
    public void Test1()
    {
        var persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "John",
                ParentId = -1,
            },
            new Person()
            {
                Id = 2,
                Name = "Max",
                ParentId = 1,
            },
            new Person()
            {
                Id = 3,
                Name = "Peter",
                ParentId = 1,
            }
        };

        var expected = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "John",
                ParentId = -1,
                Children = new List<int> { 2, 3 }
            },
            new Person()
            {
                Id = 2,
                Name = "Max",
                ParentId = 1,
            },
            new Person()
            {
                Id = 3,
                Name = "Peter",
                ParentId = 1,
            }
        };

        var result = FillChildren(persons);
        
        Xunit.Assert.Equivalent(expected, result);
    }
}