namespace playground;

public class UnitTest1
{
    [Fact]
    public void ChangeReferenceTypeInAnotherMethod()
    {
        var test = new UnitTest1();
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
        int origin = 0;
        int changed = ChangeInt(origin);
        Assert.Equal(0, origin);
    }
    
    public int ChangeInt(int origin)
    {
        origin = 1;
        return origin;
    }
}