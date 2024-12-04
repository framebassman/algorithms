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
    }
}