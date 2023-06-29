using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class JsonReader : MonoBehaviour
{

    private string path = Application.dataPath + "/Assets/Scripts/estudiantes.json";
    private List<Data> dataList = new List<Data>();

    void Start()
    {
        string jsonString = File.ReadAllText(path);
        dataList = JsonUtility.FromJson<List<Data>>(jsonString);

        foreach (Data data in dataList)
        {
            Debug.Log("Name: " + data.name);
            Debug.Log("Age: " + data.age);
        }
    }

    [System.Serializable]
    public class Data
    {
        public string name;
        public string lastname;
        public int age;
        public int id;
        public string email;
        public float grade;
    }
}