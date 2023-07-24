using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    public class ManagerEmployees
    {
        public List<Role> roles;
        public List<Seniority> seniorities;
        public Dictionary<Role, Dictionary<Seniority, List<Employee>>> employeesByRoleAndSeniority;
        public Dictionary<Role, Dictionary<Seniority, Salary>> salaryByRoleAndSenirity;

        public ManagerEmployees()
        {
            roles = new List<Role>();
            seniorities = new List<Seniority>();
            employeesByRoleAndSeniority = new Dictionary<Role, Dictionary<Seniority, List<Employee>>>();
            salaryByRoleAndSenirity = new Dictionary<Role, Dictionary<Seniority, Salary>>();
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
            foreach (var roleEntry in salaryByRoleAndSenirity)
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

            if (!employeesByRoleAndSeniority.ContainsKey(employee.Role))
            {
                employeesByRoleAndSeniority[employee.Role] = new Dictionary<Seniority, List<Employee>>();
            }

            Dictionary<Seniority, List<Employee>> employeesBySeniority = employeesByRoleAndSeniority[employee.Role];

            if (!employeesBySeniority.ContainsKey(employee.Seniority))
            {
                employeesBySeniority[employee.Seniority] = new List<Employee>();
            }

            List<Employee> employees = employeesBySeniority[employee.Seniority];
            employees.Add(employee);

        }
 
        private void AddSalary(Salary salary)
        {
            if (!salaryByRoleAndSenirity.ContainsKey(salary.Role))
            {
                salaryByRoleAndSenirity[salary.Role] = new Dictionary<Seniority, Salary>();
            }

            Dictionary<Seniority, Salary> salariesBySeniority = salaryByRoleAndSenirity[salary.Role];

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
            foreach (var roleEntry in employeesByRoleAndSeniority)
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
                if(counterRole!= employeesByRoleAndSeniority.Count)
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
            foreach (var roleEntry in salaryByRoleAndSenirity)
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
                if (counterRole != employeesByRoleAndSeniority.Count)
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
            foreach (var roleEntry in salaryByRoleAndSenirity)
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
                if (counterRole != employeesByRoleAndSeniority.Count)
                {
                    text += "\n";
                }
            }
            return text;
        }

        public void ApplyIncrementToAllBaseSalaries()
        {
            foreach (var roleEntry in salaryByRoleAndSenirity)
            {
                Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;

                foreach (var seniorityEntry in salariesBySeniority)
                {
                    Salary salary = seniorityEntry.Value;
                    salary.ApplyIncrementToBaseSalary();
                }
            }
        }

        public List<Salary> getAllSalaries()
        {
            List<Salary> salaries = new List<Salary>();
            foreach (var roleEntry in salaryByRoleAndSenirity)
            {
                Dictionary<Seniority, Salary> salariesBySeniority = roleEntry.Value;
                foreach (var seniorityEntry in salariesBySeniority)
                {
                    salaries.Add(seniorityEntry.Value);
                }
            }
            return salaries;
        }

        public List<Employee> getAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            foreach (var roleEntry in employeesByRoleAndSeniority)
            {
                Dictionary<Seniority, List<Employee>> employeesBySeniority = roleEntry.Value;
                foreach (var seniorityEntry in employeesBySeniority)
                {
                    employees.AddRange(seniorityEntry.Value);
                }
            }
            return employees;
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

        public void SaveEmployeeInSystem(Employee[] employees)
        {
            foreach (var employee in employees)
            {
                Role role = CreateRole(employee.Role.Name);
                Seniority seniority = CreateSeniority(employee.Seniority.Level);
                Salary salary = CreateSalary(role, seniority, employee.Salary.BaseSalary,employee.Salary.NextIncrement);
                Employee newEmployee = CreateEmployee(employee.Nombre, salary);
            }
        }
    }

}
