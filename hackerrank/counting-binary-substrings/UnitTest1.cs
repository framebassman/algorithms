using System.Collections;

namespace counting_binary_substrings;

public class UnitTest1
{
    /*
     * Complete the function below.
     */
    static int counting(string s) {
        var counter = 0;
        for (int i = 0; i < s.Length - 1; i++) {
            var stack = new Stack();
            stack.Push(s[i]);
            var pushedValue = s[i];
            bool hasBeenPoped = false;
            for (int j = i + 1; j < s.Length; j++) {
                if (pushedValue == s[j]) {
                    if (hasBeenPoped) {
                        break;
                    }
                    stack.Push(s[j]);
                } else {
                    stack.Pop();
                    hasBeenPoped = true;
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
        Xunit.Assert.Equal(10, counting("10010011100011"));
        //                               1 0 0 1 0 0 1 1 1 0 0 0 1 1
        //                               0 1 2 3 4 5 6 7 8 9 10111213 
        // 10010011100011
        // 0-1 - 1
        // 2-3 - 2
        // 3-4 - 3
        // 4-7 - 4
        // 5-6 - 5
        // 6-11 - 6
        // 7-10 - 7
        // 8-9 - 8
        // 10-13 - 9
        // 11-12 - 10
        // 
    }
}
