using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    [System.Serializable]
    public class Salary
    {
        public Role Role;
        public Seniority Seniority;
        public float BaseSalary;
        public float NextIncrement;

        internal Salary(Role role, Seniority seniority, float baseSalary, float increment)
        {
            Role = role;
            Seniority = seniority;
            BaseSalary = baseSalary;
            NextIncrement = increment;
        }

        public float GetBaseSalary()
        {
            return BaseSalary;
        }

        public float GetIncrement()
        {
            return NextIncrement;
        }

        public void ApplyIncrementToBaseSalary()
        {
            BaseSalary = BaseSalary + (BaseSalary * NextIncrement)/100;
        }

        public override bool Equals(object obj)
        {
            Salary salary = (Salary)obj;
            return (salary.Role.Equals(Role) && salary.Seniority.Equals(Seniority));
        }
    }
}


