namespace remove_duplicates_from_sorted_array;

public class UnitTest2
{
    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 0) return 0;

        int behindIndex = 0;
        for (int forwardIndex = 1; forwardIndex < nums.Length; forwardIndex++)
        {
            if (nums[forwardIndex] != nums[behindIndex])
            {
                behindIndex++;
                nums[behindIndex] = nums[forwardIndex];
            }
        }
        return behindIndex + 1;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest2();
        Xunit.Assert.Equal(2, test.RemoveDuplicates([1, 2, 2]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 3, 3]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 2, 3, 3]));
        Xunit.Assert.Equal(1, test.RemoveDuplicates([1, 1]));
        Xunit.Assert.Equal(3, test.RemoveDuplicates([1, 2, 3]));
        Xunit.Assert.Equal(2, test.RemoveDuplicates([1, 1, 2]));
    }
}
