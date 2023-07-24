using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEmployees
{
    public List<Role> roles;
    public List<Seniority> seniorities;
    public Dictionary<Role, Dictionary<Seniority, List<Employee>>> employeesByRoleAndSenirity;
    public Dictionary<Role, Dictionary<Seniority, Salary>> salaryByRoleAndSeniroity;

    public ManagerEmployees()
    {
        roles = new List<Role>();
        seniorities = new List<Seniority>();
        employeesByRoleAndSenirity = new Dictionary<Role, Dictionary<Seniority, List<Employee>>>();
        salaryByRoleAndSeniroity = new Dictionary<Role, Dictionary<Seniority, Salary>>();
    }

    public Role CreateRole(string name)
    {
        Role role = new Role(name);
        int i = 0;
        while (i < roles.Count)
        {
            if (roles[i].Equals(role))
            {
                return roles[i];
            }
            i++;
        }
        roles.Add(role);
        return role;
    }

    public Seniority CreateSeniority(string level)
    {
        Seniority seniority = new Seniority(level);
        int i = 0;
        while (i < seniorities.Count)
        {
            if (seniorities[i].Equals(level))
            {
                return seniorities[i];
            }
            i++;
        }
        seniorities.Add(seniority);
        return seniority;
    }

    public Employee CreateEmployee(string nombre, Salary salary)
    {
        Employee employee = new Employee(nombre, salary);
        AddEmployee(employee);
        return employee;
    }

    public Salary CreateSalary(Role role, Seniority seniority, float baseSalary, float increment)
    {

        Salary salary = new Salary(role, seniority, baseSalary, increment);
        foreach (var roleEntry in salaryByRoleAndSeniroity)
        {
            if (roleEntry.Key.Equals(role))
            {
                Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;
                foreach (var seniorityEntry in salariesBySeniority)
                {
                    if (seniorityEntry.Key.Equals(seniority)){
                        return seniorityEntry.Value;
                    }
                }
            }
        }        
        AddSalary(salary);
        return salary;
    }

    private void AddEmployee(Employee employee)
    {

        if (!employeesByRoleAndSenirity.ContainsKey(employee.Role))
        {
            employeesByRoleAndSenirity[employee.Role] = new Dictionary<Seniority, List<Employee>>();
        }

        Dictionary<Seniority, List<Employee>> employeesBySeniority = employeesByRoleAndSenirity[employee.Role];

        if (!employeesBySeniority.ContainsKey(employee.Seniority))
        {
            employeesBySeniority[employee.Seniority] = new List<Employee>();
        }

        List<Employee> employees = employeesBySeniority[employee.Seniority];
        employees.Add(employee);

    }
 
    private void AddSalary(Salary salary)
    {
        if (!salaryByRoleAndSeniroity.ContainsKey(salary.Role))
        {
            salaryByRoleAndSeniroity[salary.Role] = new Dictionary<Seniority, Salary>();
        }

        Dictionary<Seniority, Salary> salariesBySeniority = salaryByRoleAndSeniroity[salary.Role];

        if (salariesBySeniority.ContainsKey(salary.Seniority))
        {
            salariesBySeniority[salary.Seniority] = salary;
        }
        else
        {
            salariesBySeniority.Add(salary.Seniority, salary);
        }
    }

    public string EmployeeSumary()
    {
        var text = "In the company there are:\n";
        int counterRole = 0;
        foreach (var roleEntry in employeesByRoleAndSenirity)
        {
            Role role = roleEntry.Key;
            Dictionary<Seniority, List<Employee>> employeesBySeniority = roleEntry.Value;
            counterRole++;
            var text2 = "";
            var countTotalRole = 0;
            int counterSeniority = 0;
            foreach (var seniorityEntry in employeesBySeniority)
            {
                Seniority seniority = seniorityEntry.Key;
                List<Employee> employees = seniorityEntry.Value;
                if (seniorityEntry.Key.Level != Constants.noSeniority)
                {
                    counterSeniority++;
                    if (counterSeniority == employeesBySeniority.Count)
                    {
                        text2 += "and " + employees.Count + " " + seniority.Level;
                    }
                    else
                    {
                        text2 += employees.Count + " " + seniority.Level + ", ";
                    }
                }
                countTotalRole += employees.Count;
            }
            text += "* " + countTotalRole.ToString() + " " + role.Name;
            if (counterSeniority > 0)
            {
                text += " - (" + text2 + ")";
            }
            if(counterRole!= employeesByRoleAndSenirity.Count)
            {
                text += "\n";
            }
        }
        return text;
    }
    
    public string BaseSalarySumary()
    {
        var text = "The base salary is:\n";
        int counterRole = 0;
        foreach (var roleEntry in salaryByRoleAndSeniroity)
        {
            Role role = roleEntry.Key;
            Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;
            counterRole++;
            var text2 = "";
            int counterSeniority = 0;
            foreach (var seniorityEntry in salariesBySeniority)
            {
                Seniority seniority = seniorityEntry.Key;
                Salary salary = seniorityEntry.Value;
                if (seniorityEntry.Key.Level != Constants.noSeniority)
                {
                    counterSeniority++;
                    if (counterSeniority == salariesBySeniority.Count)
                    {
                        text2 += "and $" + salary.GetBaseSalary() + " " + seniority.Level;
                    }
                    else
                    {
                        text2 += "$" + salary.GetBaseSalary() + " " + seniority.Level + ", ";
                    }
                } else
                {
                    text2 += "$" + salary.GetBaseSalary();
                }
            }
            text += "* "+ role.Name + " - (" + text2 + ")";
            if (counterRole != employeesByRoleAndSenirity.Count)
            {
                text += "\n";
            }
        }
        return text;
    }

    public string IncrementSumary()
    {
        var text = "The salary increment percentage is:\n";
        int counterRole = 0;
        foreach (var roleEntry in salaryByRoleAndSeniroity)
        {
            Role role = roleEntry.Key;
            Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;
            counterRole++;
            var text2 = "";
            int counterSeniority = 0;
            foreach (var seniorityEntry in salariesBySeniority)
            {
                Seniority seniority = seniorityEntry.Key;
                Salary salary = seniorityEntry.Value;
                if (seniorityEntry.Key.Level != Constants.noSeniority)
                {
                    counterSeniority++;
                    if (counterSeniority == salariesBySeniority.Count)
                    {
                        text2 += "and " + salary.GetIncrement() + "% " + seniority.Level;
                    }
                    else
                    {
                        text2 += salary.GetIncrement() + "% " + seniority.Level + ", ";
                    }
                } else
                {
                    text2 += salary.GetIncrement() + "%";
                }
            }
            text += "* " + role.Name + " - (" + text2 + ")";
            if (counterRole != employeesByRoleAndSenirity.Count)
            {
                text += "\n";
            }
        }
        return text;
    }

    public void ApplyIncrementToAllBaseSalaries()
    {
        foreach (var roleEntry in salaryByRoleAndSeniroity)
        {
            Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;

            foreach (var seniorityEntry in salariesBySeniority)
            {
                Salary salary = seniorityEntry.Value;
                salary.ApplyIncrementToBaseSalary();
            }
        }
    }

    public List<Role> getRoles()
    {
        List<Role> roles = new List<Role>();
        foreach (var entry in salaryByRoleAndSeniroity)
        {
            Role role = entry.Key;
            roles.Add(role);
        }
        return roles;
    }


    public void SaveSalaryInSystem(Salary[] salaries)
    {
        foreach (var salary in salaries)
        {
            Role role = CreateRole(salary.Role.Name);
            Seniority seniority = CreateSeniority(salary.Seniority.Level);
            Salary newSalary = CreateSalary(role, seniority, salary.BaseSalary, salary.NextIncrement);
        }
    }
}
