using System.Collections;
using System.Collections.Generic;

public class Employee
{
    public string Nombre { get; }
    public Role Role { get; }
    public Seniority Seniority { get; }
    public Salary Salary { get; }

    internal Employee(string nombre, Salary salary)
    {
        Nombre = nombre;
        Role = salary.Role;
        Seniority = salary.Seniority;
        Salary = salary;
    }

    public override string ToString()
    {
        return Nombre;
    }
}
