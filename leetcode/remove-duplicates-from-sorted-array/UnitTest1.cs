namespace remove_duplicates_from_sorted_array;

public class UnitTest1
{
    public int RemoveDuplicates(int[] nums)
    {
        int k = 0;
        int behindIndex = 0;
        int forwardIndex = 0;
        while (behindIndex + 1 <= nums.Length)
        {
            while (forwardIndex + 1 < nums.Length && nums[behindIndex] == nums[forwardIndex + 1])
            {
                forwardIndex++;
            }
            forwardIndex++;
            behindIndex = forwardIndex;
            k++;
        }
        return k;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        Xunit.Assert.Equal(2, test.RemoveDuplicates([1, 2, 2]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 3, 3]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 2, 3, 3]));
        Xunit.Assert.Equal(1, test.RemoveDuplicates([1, 1]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 3]));
        Xunit.Assert.Equal(2, test.RemoveDuplicates([1, 1, 2]));
    }
}
