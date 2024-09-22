namespace general;

public class ReferenceAndValueTests
{
    [Fact]
    public void ChangeReferenceTypeInAnotherMethod()
    {
        var test = new ReferenceAndValueTests();
        var origin = new System.Collections.Generic.List<int>();
        var changed = test.ChangeList(origin);
        Assert.NotEmpty(origin);
    }

    public List<int> ChangeList(List<int> origin)
    {
        origin.Add(1);
        return origin;
    }
    
    [Fact]
    public void ChangeValueTypeInAnotherMethod()
    {
        var test = new ReferenceAndValueTests();
        int origin = 0;
        int changed = test.ChangeInt(origin);
        Assert.Equal(0, origin);
    }
    
    public int ChangeInt(int origin)
    {
        origin = 1;
        return origin;
    }
}
