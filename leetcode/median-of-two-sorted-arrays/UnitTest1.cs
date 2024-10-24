namespace median_of_two_sorted_arrays;

public class UnitTest1
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length == 0) {
            return GetMedian(nums2);
        }
        
        if (nums2.Length == 0) {
            return GetMedian(nums1);
        }
        
        var result = MergeArrays(nums1, nums2);
        return GetMedian(result);
    }
    
    public static int[] MergeArrays(int[] nums1, int[] nums2) {
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
                if (secondIndex < nums2.Length - 1) {
                    secondIndex++;
                }
            }
        }

        return result;
    }
    
    public static double GetMedian(int[] result) {
        if (result.Length == 0) {
            return 0;
        }
        
        if (result.Length == 1) {
            return result[0];
        }
        
        if (result.Length == 2) {
            return ((double)result[0] + (double)result[1]) / 2.0;
        }

        if (result.Length % 2 == 0)
        {
            return (result[(result.Length / 2) - 1] + result[result.Length / 2]) / 2.0;
        }
        else
        {
            return result[result.Length / 2];
        }
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        int[] arr1 =  new int[] { 1, 2, 4};
        int[] arr2 = new int[] {3, 5};
        Assert.Equal(3.0, test.FindMedianSortedArrays(arr1, arr2));
    }

    [Fact]
    public void Test2()
    {
        var test = new UnitTest1();
        int[] arr1 =  new int[] { 1, 3};
        int[] arr2 = new int[] {2};
        Assert.Equal(2.0, test.FindMedianSortedArrays(arr1, arr2));
    }
    
    [Fact]
    public void Test3()
    {
        var test = new UnitTest1();
        int[] arr1 =  new int[] { 1, 3};
        int[] arr2 = new int[] {2, 4};
        Assert.Equal(2.5, test.FindMedianSortedArrays(arr1, arr2));
    }
    
    [Fact]
    public void Test5()
    {
        var test = new UnitTest1();
        int[] arr1 = new int[] { 100_001 };
        int[] arr2 = new int[] { 100_000 };
        Assert.Equal(100_000.5, test.FindMedianSortedArrays(arr1, arr2));
    }
}