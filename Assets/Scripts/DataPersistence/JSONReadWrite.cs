using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JSONReadWrite : MonoBehaviour
{
    public Data ReadJSON(string fullPath)
    {
        Data loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = File.ReadAllText(fullPath);

                //deserialize
                loadedData = JsonUtility.FromJson<Data>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load the data " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
}
