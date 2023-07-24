using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace DataPersistence
{
    public class JSONReadWrite : MonoBehaviour, IDataPersistence
    {
        public Data Read(string fullPath)
        {
            Data loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = File.ReadAllText(fullPath);

                    loadedData = JsonUtility.FromJson<Data>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error ocurred when trying to load the data " + fullPath + "\n" + e);
                }
            }
            return loadedData;
        }

        public void Write(Data data, string fullPath)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(fullPath, json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to save the data " + fullPath + "\n" + e);
            }
        }


    }
}

