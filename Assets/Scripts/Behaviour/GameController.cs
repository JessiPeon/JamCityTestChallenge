using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using DataPersistence;

namespace Behaviour
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Animator mainCamera;
        [SerializeField] private Main main;
        [SerializeField] private Animator logoAnimator;
        [SerializeField] private Animator[] buttonsAnimator;
        [SerializeField] private GameObject roleInfo;
        [SerializeField] private GameObject applyButton;
        [SerializeField] private Transform centerPosition;

        [Header("RoleInfo")]
        [SerializeField] private GameObject summaryUnique;
        [SerializeField] private GameObject summarySeniorities;
        [SerializeField] private TextMeshProUGUI textTotal;
        [SerializeField] private TextMeshProUGUI textGold;
        [SerializeField] private TextMeshProUGUI textSilver;
        [SerializeField] private TextMeshProUGUI textBronze;
        [SerializeField] private TextMeshProUGUI textIncrementGold;
        [SerializeField] private TextMeshProUGUI textSalaryGold;
        [SerializeField] private TextMeshProUGUI textIncrementSilver;
        [SerializeField] private TextMeshProUGUI textSalarySilver;
        [SerializeField] private TextMeshProUGUI textIncrementBronze;
        [SerializeField] private TextMeshProUGUI textSalaryBronze;
        [SerializeField] private TextMeshProUGUI textIncrementUnique;
        [SerializeField] private TextMeshProUGUI textSalaryUnique;

        private IslandManager islandManager;
        private IDataPersistence jsonLoader;
        private DataManager dataManager;
        public ManagerEmployees managerEmployees;

        void Start()
        {
            managerEmployees = new ManagerEmployees();
            FindObjectOfType<AudioController>().Play("Ocean");
            islandManager = gameObject.GetComponent<IslandManager>();

            jsonLoader = new JSONReadWrite();
            dataManager = new DataManager(jsonLoader);
        }

        private void OnEnable()
        {
            Island.IslandIsSelected += ShowRoleInfo;
        }

        private void OnDisable()
        {
            Island.IslandIsSelected -= ShowRoleInfo;
        }

        public void InitPath(TMP_InputField inputField)
        {
            inputField.text = Application.dataPath + "/Data/data.json";
        }

        public void InitGameDefault()
        {
            gameObject.GetComponent<Main>().DefaultSet(managerEmployees);
            InitGame();
        }

        public void InitGameFile(TMP_InputField input)
        {
            dataManager.LoadData(input.text,managerEmployees);
            
            InitGame();
        }
        
        private void InitGame()
        {
            FindObjectOfType<AudioController>().Play("Wood");
            logoAnimator.SetTrigger("disappear");
            mainCamera.SetTrigger("zoomOut");
            applyButton.SetActive(true);
            foreach (var button in buttonsAnimator)
            {
                button.SetTrigger("disappear");
            }
            CreateIslands();
        }

        private void CreateIslands()
        {
            IslandManager islandManager = gameObject.GetComponent<IslandManager>();
            foreach (var role in managerEmployees.roles)
            {
                islandManager.CreateIsland(role);
            }
        }

        private void ShowRoleInfo(Island island)
        {
            if(!roleInfo.activeSelf)
            {
                mainCamera.SetTrigger("zoomIn");
                roleInfo.SetActive(true);
                FindObjectOfType<AudioController>().Play("Island");

                #region displayData
                Dictionary<Seniority, List<Employee>> employeesBySeniority = managerEmployees.employeesByRoleAndSeniority[island.role];
                int total = 0;
                foreach (var seniorityEntry in employeesBySeniority)
                {
                    if (seniorityEntry.Key.Level == Constants.noSeniority)
                    {
                        summaryUnique.SetActive(true);
                        summarySeniorities.SetActive(false);
                    } else
                    {
                        summaryUnique.SetActive(false);
                        summarySeniorities.SetActive(true);
                        if (seniorityEntry.Key.Level == Constants.Senior)
                        {
                            textGold.text = seniorityEntry.Value.Count.ToString();
                        }
                        else if (seniorityEntry.Key.Level == Constants.SemiSenior)
                        {
                            textSilver.text = seniorityEntry.Value.Count.ToString();
                        }
                        else if (seniorityEntry.Key.Level == Constants.Junior)
                        {
                            textBronze.text = seniorityEntry.Value.Count.ToString();
                        }
                    }
                    total += seniorityEntry.Value.Count;
                }
                textTotal.text = total.ToString();

                Dictionary<Seniority, Salary> salariesBySeniority = managerEmployees.salaryByRoleAndSenirity[island.role];
                foreach (var seniorityEntry in salariesBySeniority)
                {
                    if (seniorityEntry.Key.Level == Constants.Senior)
                    {
                        textIncrementGold.text = seniorityEntry.Value.NextIncrement.ToString();
                        textSalaryGold.text = seniorityEntry.Value.BaseSalary.ToString();
                    }
                    else if (seniorityEntry.Key.Level == Constants.SemiSenior)
                    {
                        textIncrementSilver.text = seniorityEntry.Value.NextIncrement.ToString();
                        textSalarySilver.text = seniorityEntry.Value.BaseSalary.ToString();
                    }
                    else if (seniorityEntry.Key.Level == Constants.Junior)
                    {
                        textIncrementBronze.text = seniorityEntry.Value.NextIncrement.ToString();
                        textSalaryBronze.text = seniorityEntry.Value.BaseSalary.ToString();
                    }
                    else if (seniorityEntry.Key.Level == Constants.noSeniority)
                    {
                        textIncrementUnique.text = seniorityEntry.Value.NextIncrement.ToString();
                        textSalaryUnique.text = seniorityEntry.Value.BaseSalary.ToString();
                    }
                }
                #endregion displayData

                islandManager.ResetIslands();
                island.targetPosition = centerPosition.position;
                applyButton.SetActive(false);
            }
        }

        public void BackToIslands()
        {
            mainCamera.SetTrigger("zoomOut");
            roleInfo.SetActive(false);
            islandManager.ResetIslands();
            applyButton.SetActive(true);
            CleanTexts();
        }

        private void CleanTexts()
        {
            textTotal.text = "0";
            textGold.text = "0";
            textSilver.text = "0";
            textBronze.text = "0";

            textIncrementGold.text = "0";
            textSalaryGold.text = "0";

            textIncrementSilver.text = "0";
            textSalarySilver.text = "0";

            textIncrementBronze.text = "0";
            textSalaryBronze.text = "0";

            textIncrementUnique.text = "0";
            textSalaryUnique.text = "0";
        }

        public void ApplyIncrement()
        {
            FindObjectOfType<AudioController>().Play("Apply");
            applyButton.GetComponent<Animator>().SetTrigger("ok");

            managerEmployees.ApplyIncrementToAllBaseSalaries();

            DateTime dateNow = DateTime.Now;
            int day = dateNow.Day;
            int month = dateNow.Month;
            int year = dateNow.Year;
            string date = year.ToString() + "_" + month.ToString() + "_" + day.ToString();
            dataManager.SaveData(Application.dataPath + "/Data/"+ date + "_data.json", managerEmployees);
        }
    }
}

