using System;
using System.Collections.Generic;
using System.IO;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Project> Projects { get; set; } = new List<Project>();
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}


class Solution {

    static void Main(String[] args) {
        var employees = TestData.GetSampleEmployees();
        var queries = new EmployeeProjectQueries();

        // Task 1: Get all employees working on "Project B"
        var employeesOnProjectB = queries.GetEmployeesByProject("Project B", employees);
        Console.WriteLine("Employees on Project B:");
        employeesOnProjectB.ForEach(e => Console.WriteLine(e.Name));

        // Task 2: Get the total number of employees working on any project
        var totalEmployees = queries.GetTotalEmployees(employees);
        Console.WriteLine($"Total employees working on projects: {totalEmployees}");

        // Task 3: List all employees with their project counts
        var employeeProjectCounts = queries.GetEmployeeProjectCounts(employees);
        Console.WriteLine("Employee project counts:");
        employeeProjectCounts.ForEach(e => Console.WriteLine($"{e.EmployeeName}: {e.ProjectCount} projects"));

        // Task 4: Get employees with no projects
        var employeesWithNoProjects = queries.GetEmployeesWithNoProjects(employees);
        Console.WriteLine("Employees with no projects:");
        employeesWithNoProjects.ForEach(e => Console.WriteLine(e.Name));

        // Task 5: Get all unique project names
        var uniqueProjectNames = queries.GetAllUniqueProjectNames(employees);
        Console.WriteLine("Unique project names:");
        uniqueProjectNames.ForEach(p => Console.WriteLine(p));
    }
}      

public class EmployeeProjectQueries
{
    // Task 1: Fetch all employees working on a specific project by project name.
    // Input: projectName (string), employees (List<Employee>)
    // Output: List of Employees working on that project.
    public List<Employee> GetEmployeesByProject(string projectName, List<Employee> employees)
    {
        // TODO: Implement a LINQ query to return employees whose project list contains the specified projectName.
        return employees
            .Where(e =>
                e.Projects.Any(p => p.Name == projectName))
            .ToList();
    }

    // Task 2: Calculate the total number of employees working across all projects.
    // Input: employees (List<Employee>)
    // Output: Integer count of employees involved in at least one project.
    public int GetTotalEmployees(List<Employee> employees)
    {
        // TODO: Implement a LINQ query to count the number of employees assigned to any project.
        return employees.Count(e => e.Projects.Count > 0);
    }

    // Task 3: List the names of all employees along with the number of projects they are assigned to.
    // Input: employees (List<Employee>)
    // Output: List of tuples (employee name, project count)
    public List<(string EmployeeName, int ProjectCount)> GetEmployeeProjectCounts(List<Employee> employees)
    {
        // TODO: Implement a LINQ query to return a list of employee names and their project counts.
        // return new List<(string EmployeeName, int ProjectCount)>();
        return employees.Select(e => (e.Name, e.Projects.Count)).ToList();
    }

    // Task 4: List all employees who are not assigned to any project.
    // Input: employees (List<Employee>)
    // Output: List of employees with no projects.
    public List<Employee> GetEmployeesWithNoProjects(List<Employee> employees)
    {
        // TODO: Implement a LINQ query to return employees who have an empty project list.
        return employees.Where(e => e.Projects.Count == 0).ToList();
    }

    // Task 5: Get a list of all unique project names across all employees.
    // Input: employees (List<Employee>)
    // Output: List of unique project names.
    public List<string> GetAllUniqueProjectNames(List<Employee> employees)
    {
        // TODO: Implement a LINQ query to return a list of unique project names across all employees.
        return employees
                    .SelectMany(e => e.Projects)
                    .Select(p => p.Name)
                    .Distinct()
                    .ToList();
    }
}

public class TestData
{
    public static List<Employee> GetSampleEmployees()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Alice",
                Projects = new List<Project>
                {
                    new Project { Id = 1, Name = "Project A" },
                    new Project { Id = 2, Name = "Project B" }
                }
            },
            new Employee
            {
                Id = 2,
                Name = "Bob",
                Projects = new List<Project>
                {
                    new Project { Id = 3, Name = "Project B" }
                }
            },
            new Employee
            {
                Id = 3,
                Name = "Charlie",
                Projects = new List<Project>()
            },
            new Employee
            {
                Id = 4,
                Name = "Daisy",
                Projects = new List<Project>
                {
                    new Project { Id = 4, Name = "Project C" }
                }
            }
        };
    }
}

