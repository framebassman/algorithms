using System.Text;
using Xunit.Abstractions;

namespace fill_children;

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

        var dictionary = new Dictionary<int, List<int>?>();
        foreach (Person candidate in persons) {
            if (candidate.Id == candidate.ParentId) {
                throw new Exception();
            }

            if (dictionary.ContainsKey(candidate.ParentId)) {
                dictionary[candidate.ParentId].Add(candidate.Id);
            } else {
                dictionary.Add(candidate.ParentId, new List<int>() { candidate.Id });
            }
        }

        foreach (Person candidate in persons) {
            if (dictionary.ContainsKey(candidate.Id)) {
                candidate.Children = dictionary[candidate.Id];
            }
        }

        return persons;
    }

    [Fact]
    public void Test1()
    {
        var persons1 = new List<Person>()
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

        var expected1 = new List<Person>()
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
        Xunit.Assert.Equivalent(expected1, FillChildrenDictionary(persons1));

        var persons2 = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "John",
                ParentId = 1,
            }
        };
        Xunit.Assert.Throws<Exception>(() => FillChildrenDictionary(persons2));
    }
}
