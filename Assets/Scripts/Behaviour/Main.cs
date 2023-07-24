using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;

namespace Behaviour
{
    public class Main : MonoBehaviour
    {
        public void DefaultSet(ManagerEmployees managerEmployees)
        {
            Role hrRole = managerEmployees.CreateRole("HR");
            Role engRole = managerEmployees.CreateRole("Engineering");
            Role artRole = managerEmployees.CreateRole("Artist");
            Role desRole = managerEmployees.CreateRole("Design");
            Role pmRole = managerEmployees.CreateRole("PMs");
            Role ceoRole = managerEmployees.CreateRole("Ceo");

            Seniority seniorSeniority = managerEmployees.CreateSeniority(Constants.Senior);
            Seniority semiSeniorSeniority = managerEmployees.CreateSeniority(Constants.SemiSenior);
            Seniority juniorSeniority = managerEmployees.CreateSeniority(Constants.Junior);
            Seniority noSeniority = managerEmployees.CreateSeniority(Constants.noSeniority);

            Salary hrSeniorSalary = managerEmployees.CreateSalary(hrRole, seniorSeniority, 1500, 5);
            Salary hrSemiSeniorSalary = managerEmployees.CreateSalary(hrRole, semiSeniorSeniority, 1000, 2);
            Salary hrJuniorSalary = managerEmployees.CreateSalary(hrRole, juniorSeniority, 500, 0.5f);
            Salary engSeniorSalary = managerEmployees.CreateSalary(engRole, seniorSeniority, 5000, 10);
            Salary engSemiSeniorSalary = managerEmployees.CreateSalary(engRole, semiSeniorSeniority, 3000, 7);
            Salary engJuniorSalary = managerEmployees.CreateSalary(engRole, juniorSeniority, 1500, 5);
            Salary artSeniorSalary = managerEmployees.CreateSalary(artRole, seniorSeniority, 2000, 5);
            Salary artSemiSeniorSalary = managerEmployees.CreateSalary(artRole, semiSeniorSeniority, 1200, 2.5f);
            Salary desSeniorSalary = managerEmployees.CreateSalary(desRole, seniorSeniority, 2000, 7);
            Salary desJuniorSalary = managerEmployees.CreateSalary(desRole, juniorSeniority, 800, 4);
            Salary pmSeniorSalary = managerEmployees.CreateSalary(pmRole, seniorSeniority, 4000, 10);
            Salary pmSemiSeniorSalary = managerEmployees.CreateSalary(pmRole, semiSeniorSeniority, 2400, 5);
            Salary ceoSalary = managerEmployees.CreateSalary(ceoRole, noSeniority, 20000, 100);

            for (int i = 0; i < 5; i++)
            {
                managerEmployees.CreateEmployee("Empleado", hrSeniorSalary);
            }
            for (int i = 0; i < 2; i++)
            {
                managerEmployees.CreateEmployee("Empleado", hrSemiSeniorSalary);
            }
            for (int i = 0; i < 13; i++)
            {
                managerEmployees.CreateEmployee("Empleado", hrJuniorSalary);
            }
            for (int i = 0; i < 50; i++)
            {
                managerEmployees.CreateEmployee("Empleado", engSeniorSalary);
            }
            for (int i = 0; i < 68; i++)
            {
                managerEmployees.CreateEmployee("Empleado", engSemiSeniorSalary);
            }
            for (int i = 0; i < 32; i++)
            {
                managerEmployees.CreateEmployee("Empleado", engJuniorSalary);
            }
            for (int i = 0; i < 5; i++)
            {
                managerEmployees.CreateEmployee("Empleado", artSeniorSalary);
            }
            for (int i = 0; i < 20; i++)
            {
                managerEmployees.CreateEmployee("Empleado", artSemiSeniorSalary);
            }
            for (int i = 0; i < 10; i++)
            {
                managerEmployees.CreateEmployee("Empleado", desSeniorSalary);
            }
            for (int i = 0; i < 15; i++)
            {
                managerEmployees.CreateEmployee("Empleado", desJuniorSalary);
            }
            for (int i = 0; i < 10; i++)
            {
                managerEmployees.CreateEmployee("Empleado", pmSeniorSalary);
            }
            for (int i = 0; i < 20; i++)
            {
                managerEmployees.CreateEmployee("Empleado", pmSemiSeniorSalary);
            }
            managerEmployees.CreateEmployee("Empleado", ceoSalary);
        }

    }
}

