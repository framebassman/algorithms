using System.Transactions;

namespace median_of_two_sorted_arrays;

public class UnitTest1
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        int pointer1 = 0;
        int pointer2 = 0;

        int halfLength = (nums1.Length + nums2.Length) / 2;

        for (int index = 0; index < halfLength - 1; index++)
        {
            if (nums1[pointer1] <= nums2[pointer2])
            {
                pointer1++;
            }
            else
            {
                pointer2++;
            }
        }
        return (nums1[pointer1] + nums2[pointer2]) / 2.0;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        Xunit.Assert.Equal(1.5, test.FindMedianSortedArrays(new [] {1, 2}, new [] {1, 2}));
        Xunit.Assert.Equal(2, test.FindMedianSortedArrays(new [] {1, 2}, new [] {1, 2, 3}));
    }
}