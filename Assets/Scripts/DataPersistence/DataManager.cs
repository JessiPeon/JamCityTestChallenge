using DataPersistence;
using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    private IDataPersistence dataLoader;

    public DataManager(IDataPersistence loader)
    {
        dataLoader = loader;
    }

    public void SaveData(string path, ManagerEmployees managerEmployees)
    {
        Data data = new Data();
        data.salary = managerEmployees.getAllSalaries().ToArray();
        data.employee = managerEmployees.getAllEmployees().ToArray();
        dataLoader.Write(data, path);
    }

    public void LoadData(string path,ManagerEmployees managerEmployees)
    {
        Data data = dataLoader.Read(path);
        managerEmployees.SaveSalaryInSystem(data.salary);
        managerEmployees.SaveEmployeeInSystem(data.employee);
    }
}
