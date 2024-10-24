namespace median_of_two_sorted_arrays;

public class UnitTest1
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var result = new int[nums1.Length + nums2.Length];
        
        var firstIndex = 0;
        var secondIndex = 0;
        
        for (var i = 0; i < result.Length; i++)
        {
            if (firstIndex < nums1.Length && nums1[firstIndex] < nums2[secondIndex])
            {
                result[i] = nums1[firstIndex];
                firstIndex++;
            }
            else
            {
                result[i] = nums2[secondIndex];
                secondIndex++;
            }
        }
        
        if (result.Length % 2 == 0)
        {
            return result[(result.Length / 2) - 1] / 2.0;
        }
        else
        {
            return (result[(result.Length / 2) - 1] + result[result.Length / 2]) / 2.0;
        }
    }
    
    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        int[] arr1 =  new int[] { 1, 2, 4};
        int[] arr2 = new int[] {3, 5};
        Assert.Equal(2.5, test.FindMedianSortedArrays(arr1, arr2));
    }
}