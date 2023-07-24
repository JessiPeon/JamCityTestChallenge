using System.Collections;
using System.Collections.Generic;

namespace Logic
{
    [System.Serializable]
    public class Employee
    {
        public string Nombre;
        public Role Role;
        public Seniority Seniority;
        public Salary Salary;

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

}
