using System.Collections;

namespace counting_binary_substrings;

public class UnitTest1
{
        /*
     * Complete the function below.
     */
    static int counting(string s) {
        var counter = 0;
        for (int i = 0; i < s.Length; i++) {
            int currentIndex = i;
            var stack = new Stack();
            stack.Push(s[currentIndex]);
            var pushedValue = s[i];

            if (currentIndex + 1 < s.Length) {
                currentIndex++;
            } else {
                continue;
            }

            for (int j = currentIndex; j < s.Length; j++) {
                if (pushedValue == s[j]) {
                    stack.Push(s[j]);
                } else {
                    stack.Pop();
                }
                if (stack.Count == 0) {
                    counter++;
                    break;
                }
            }
        }
        return counter;
    }

    [Fact]
    public void Test1()
    {
        Xunit.Assert.Equal(1, counting("01"));
        Xunit.Assert.Equal(2, counting("0011"));
        Xunit.Assert.Equal(1, counting("011"));
        Xunit.Assert.Equal(2, counting("010"));
        Xunit.Assert.Equal(4, counting("001101"));
        Xunit.Assert.Equal(11, counting("10010011100011"));
        //                               1 0 0 1 0 0 1 1 1 0 0 0 1 1
        //                               0 1 2 3 4 5 6 7 8 9 10111213 
        // 10010011100011
        // 0-1 - 1
        // 0-3 - 2
        // 2-3 - 3
        // 3-4 - 4
        // 3-6 - 5
        // 4-7 - 6
        // 5-6 - 7
        // 7-10 - 8
        // 8-9 - 9
        // 10-13 - 10
        // 11-12 - 11
        // 
    }
}
