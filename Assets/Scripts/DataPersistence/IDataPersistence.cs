using DataPersistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    public Data Read(string fullPath);

    public void Write(Data data, string fullPath);
}
