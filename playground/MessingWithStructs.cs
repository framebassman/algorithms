namespace playground;

public struct TrickyStruct
{
    public OrdinalClass OrdinalClassProperty { get; set; }
    public int IntProperty { get; set; }
}

public class OrdinalClass
{
    public int Id { get; set; }

    public override int GetHashCode()
    {
        return this.Id;
    }
}

public class MessingWithStructs
{
    [Fact]
    public void Test1()
    {
        var first = new TrickyStruct()
        {
            IntProperty = 33,
            OrdinalClassProperty = new OrdinalClass() { Id = 1 }
        };
        
        var second = new TrickyStruct()
        {
            IntProperty = 33,
            OrdinalClassProperty = new OrdinalClass() { Id = 1 }
        };
        
        Assert.False(first.Equals(second));
    }
}