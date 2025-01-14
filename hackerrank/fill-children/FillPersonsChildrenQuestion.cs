using System;
using System.Collections.Generic;
using System.IO;
class Solution
{
    // # Fill Person Children

    // You receive list of Persons from DB.
    // You need to fill Children property programmatically.

    // Rules:
    // - If p1 have Id which is p2's ParentId => p1 is a child of p2.
    // - If persons's ParentId is no one's Id => He's no one's child.
    // - Order of persons in list should not be matter
    // - Ids and ParendIds can be ANY int value (><=0)
    // - persons list can be null and empty
    // ## Example

    // Input:
    // { Id = 1,ParentId = 0,Children = []}
    // {Id = 2,ParentId = 1,Children = []}
    // { Id = 3,ParentId = 1,Children = []}

    // Result:
    // { Id = 1,ParentId = 0,Children = [2, 3]}
    // { Id = 2,ParentId = 1,Children = []}
    // { Id = 3,ParentId = 1,Children = []}
    public class Person
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public IList<int> Children { get; set; } = new List<int>();
    }
    
    // Your code here
    public static IList<Person> FillChildren(IList<Person> persons)
    {
        throw new NotImplementedException();
    }

    static void Main(String[] args)
    {
        var p1 = new Person()
        {
            Id = 1,
            Name = "John",
            ParentId = -1,
        };
        var p2 = new Person()
        {
            Id = 2,
            Name = "Max",
            ParentId = 1,
        };
        var p3 = new Person()
        {
            Id = 3,
            Name = "Peter",
            ParentId = 1,
        };

        var persons = new List<Person>() { p1, p2, p3 };

        var result = FillChildren(persons);

        Console.WriteLine(string.Join(", ", result));
    }
}      
