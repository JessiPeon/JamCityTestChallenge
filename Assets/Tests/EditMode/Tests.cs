using Logic;
using NUnit.Framework;

namespace EditMode
{
    public class Tests
    {
        [Test]
        public void TestsEmployeeSumary()
        {
            string expectedText = "In the company there are:\n"+
                                    "* 20 HR - (5 Senior, 2 Semi Senior, and 13 Junior)\n"+
                                    "* 150 Engineering - (50 Senior, 68 Semi Senior, and 32 Junior)\n"+
                                    "* 25 Artist - (5 Senior, and 20 Semi Senior)\n"+
                                    "* 25 Design - (10 Senior, and 15 Junior)\n"+
                                    "* 30 PMs - (10 Senior, and 20 Semi Senior)\n"+
                                    "* 1 Ceo";
            ManagerEmployees managerEmployees = createDesfultSet();
            string resultText = managerEmployees.EmployeeSumary();
            Assert.AreEqual(expectedText, resultText);
        }
 
        [Test]
        public void TestsBaseSalarySumary()
        {
            string expectedText = "The base salary is:\n" +
                                    "* HR - ($1500 Senior, $1000 Semi Senior, and $500 Junior)\n" +
                                    "* Engineering - ($5000 Senior, $3000 Semi Senior, and $1500 Junior)\n" +
                                    "* Artist - ($2000 Senior, and $1200 Semi Senior)\n" +
                                    "* Design - ($2000 Senior, and $800 Junior)\n" +
                                    "* PMs - ($4000 Senior, and $2400 Semi Senior)\n" +
                                    "* Ceo - ($20000)";
            ManagerEmployees managerEmployees = createDesfultSet();
            string resultText = managerEmployees.BaseSalarySumary();
            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void TestsIncrementSumary()
        {
            string expectedText = "The salary increment percentage is:\n" +
                                    "* HR - (5% Senior, 2% Semi Senior, and 0,5% Junior)\n" +
                                    "* Engineering - (10% Senior, 7% Semi Senior, and 5% Junior)\n" +
                                    "* Artist - (5% Senior, and 2,5% Semi Senior)\n" +
                                    "* Design - (7% Senior, and 4% Junior)\n" +
                                    "* PMs - (10% Senior, and 5% Semi Senior)\n" +
                                    "* Ceo - (100%)";
            ManagerEmployees managerEmployees = createDesfultSet();
            string resultText = managerEmployees.IncrementSumary();
            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void TestsSalaryIncresed()
        {
            string expectedText = "The base salary is:\n" +
                                    "* HR - ($1575 Senior, $1020 Semi Senior, and $502,5 Junior)\n" +
                                    "* Engineering - ($5500 Senior, $3210 Semi Senior, and $1575 Junior)\n" +
                                    "* Artist - ($2100 Senior, and $1230 Semi Senior)\n" +
                                    "* Design - ($2140 Senior, and $832 Junior)\n" +
                                    "* PMs - ($4400 Senior, and $2520 Semi Senior)\n" +
                                    "* Ceo - ($40000)";
            ManagerEmployees managerEmployees = createDesfultSet();
            managerEmployees.ApplyIncrementToAllBaseSalaries();
            string resultText = managerEmployees.BaseSalarySumary();
            Assert.AreEqual(expectedText, resultText);
        }

        private ManagerEmployees createDesfultSet()
        {
            ManagerEmployees managerEmployees = new ManagerEmployees();

            Role hrRole = managerEmployees.CreateRole("HR");
            Role engRole = managerEmployees.CreateRole("Engineering");
            Role artRole = managerEmployees.CreateRole("Artist");
            Role desRole = managerEmployees.CreateRole("Design");
            Role pmRole = managerEmployees.CreateRole("PMs");
            Role ceoRole = managerEmployees.CreateRole("Ceo");

            Seniority seniorSeniority = managerEmployees.CreateSeniority("Senior");
            Seniority semiSeniorSeniority = managerEmployees.CreateSeniority("Semi Senior");
            Seniority juniorSeniority = managerEmployees.CreateSeniority("Junior");
            Seniority noSeniority = managerEmployees.CreateSeniority("Unique");

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

            return managerEmployees;
        }
    }

}