namespace general;

class Person
{
    public int Id { get; init; }

    public string Name { get; set; }

    public override bool Equals(object other)
    {
        if (other is Person)
        {
            return this.Id == ((Person) other).Id && this.Name == ((Person) other).Name;
        }

        return false;
    }
}

public class TrickyDictionary
{
    [Fact]
    public void Test1()
    {
        var dict = new Dictionary<Person, int>();

        var p1 = new Person() { Id = 1, Name = "Michael Scott" };

        dict.Add(p1, 1);

        var p2 = new Person() { Id = 1, Name = "Toby Flenderson" };

        dict.Add(p2, 2);

        var p3 = new Person() { Id = 1, Name = "Michael Scott" };
        
        dict[p3] = 3;

        Assert.Equal(3, dict.Count);
    }
}
