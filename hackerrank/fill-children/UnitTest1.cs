using System.Text;
using Xunit.Abstractions;

namespace fill_children;

public class Person
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "Unnamed";
    public IList<int> Children { get; set; } = new List<int>();
}

public class UnitTest1
{
    private ITestOutputHelper _console;

    public UnitTest1(ITestOutputHelper console) { _console = console; }
    
    // Your code here
    public static IList<Person> FillChildrenBruteForce(IList<Person> persons)
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

    public static IList<Person> FillChildrenDictionary(IList<Person> persons)
    {
        if (persons.Count == 0) {
            return persons;
        }

        var dictionary = new Dictionary<int, List<int>>();
        foreach (Person candidate in persons) {
            if (dictionary.ContainsKey(candidate.Id)) {
                dictionary[candidate.Id].Add(candidate.ParentId);
            } else {
                dictionary.Add(candidate.Id, new List<int>() { candidate.ParentId });
            }
        }

        foreach (Person candidate in persons) {
            candidate.Children = dictionary[candidate.Id];
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

        var result = FillChildrenDictionary(persons);
        
        Xunit.Assert.Equivalent(expected, result);
    }
}