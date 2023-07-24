using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private TextMeshProUGUI textTotal;
    [SerializeField] private TextMeshProUGUI textGold;
    [SerializeField] private TextMeshProUGUI textSilver;
    [SerializeField] private TextMeshProUGUI textBronze;
    [SerializeField] private TextMeshProUGUI textIncrement;
    [SerializeField] private TextMeshProUGUI textSalary;

    private IslandManager islandManager;

    public ManagerEmployees managerEmployees;

    void Start()
    {
        managerEmployees = new ManagerEmployees();
        FindObjectOfType<AudioController>().Play("Ocean");
        islandManager = gameObject.GetComponent<IslandManager>();
    }

    private void OnEnable()
    {
        Island.IslandIsSelected += showRoleInfo;
    }

    private void OnDisable()
    {
        Island.IslandIsSelected -= showRoleInfo;
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
        Data data = gameObject.GetComponent<JSONReadWrite>().ReadJSON(input.text);
        managerEmployees.SaveSalaryInSystem(data.salary);
        InitGame();
    }
    private void InitGame()
    {
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
        List<Role> roles = managerEmployees.getRoles();
        int countRoles = roles.Count;
        foreach (var role in roles)
        {
            islandManager.CreateIsland(role);
        }
    }

    private void showRoleInfo(Island island)
    {
        if(!roleInfo.activeSelf)
        {
            mainCamera.SetTrigger("zoomIn");
            roleInfo.SetActive(true);
            //textTotal.text = island.role.Name;
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
    }

    public void ApplyIncrement()
    {
        applyButton.GetComponent<Animator>().SetTrigger("ok");
        managerEmployees.ApplyIncrementToAllBaseSalaries();
    }
}
