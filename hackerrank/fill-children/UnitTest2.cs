using Xunit.Abstractions;

namespace fill_children;

public class UnitTest2
{
    private ITestOutputHelper _console;

    public UnitTest2(ITestOutputHelper console) { _console = console; }

    // вопросы по задаче
    // 1. Переспросить условие
    // 2. Написать тесты на те случаи, которые даны
    // 3. Привести пример невалидных данных и спросить чё в этом случае, тоже написать тесты
    // 4. Сформулировать как решать полным перебором
    // 5. Попытаться придумать меньше чем за N квадрат
    
    // Input:
    // { Id = 1,ParentId = 0,Children = []}
    // { Id = 2,ParentId = 1,Children = []}
    // { Id = 3,ParentId = 1,Children = []}

    // Result:
    // { Id = 1,ParentId = 0,Children = [2, 3]}
    // { Id = 2,ParentId = 1,Children = []}
    // { Id = 3,ParentId = 1,Children = []}

    public static IList<Person> FillChildrenDictionary(IList<Person> persons)
    {
        var parentIdById = new Dictionary<int, List<int>>();
        foreach (var person in persons)
        {
            if (person.ParentId == person.Id)
            {
                throw new Exception();
            }

            List<int> children = null;
            parentIdById.Remove(person.ParentId, out children);
            if (children == null)
            {
                children = new List<int>();
            }
            children.Add(person.Id);
            parentIdById.Add(person.ParentId, children);
        }

        foreach (var person in persons)
        {
            parentIdById.TryGetValue(person.Id, out List<int> children);
            if (children == null)
            {
                children = new List<int>();
            }
            person.Children = children;
        }

        return persons;
    }

    [Fact]
    public void PositiveCase()
    {
        var persons1 = new List<Person>
        {
            new Person
            {
                Id = 1,
                Name = "John",
                ParentId = -1,
            },
            new()
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
    }

    [Fact]
    public void ThrowExceptionIfIdEqualsToParentId()
    {
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