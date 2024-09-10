namespace merge_two_sorted_lists;

public class UnitTest2
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        var current = new ListNode();
        var head = current;
        
        while (list1 != null && list2 != null)
        {
            if (list1.val <= list2.val)
            {
                current.next = list1;
                list1 = list1.next;
            }
            else
            {
                current.next = list2;
                list2 = list2.next;
            }
            current = current.next;
        }
        
        if (list1 != null)
        {
            current.next = list1;
        }
        if (list2 != null)
        {
            current.next = list2;
        }
        
        return head.next;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest2();

        var first = new ListNode(1, new ListNode(2, new ListNode(4)));
        var second = new ListNode(1, new ListNode(3, new ListNode(4)));
        var expected = new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(4))))));
        var actual = test.MergeTwoLists(first, second);

        Xunit.Assert.Equal(System.Text.Json.JsonSerializer.Serialize(expected), System.Text.Json.JsonSerializer.Serialize(actual));
    }
}