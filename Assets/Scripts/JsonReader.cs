using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;  // Archivo JSON que contiene los datos
    public Transform contentParent;  // Transform del objeto padre de las filas de la tabla
    public GameObject rowPrefab;  // Prefab de la fila de la tabla

    private List<Dictionary<string, string>> rowData;  // Lista para almacenar los datos del JSON

    private void Start()
    {
        // Cargar y analizar el archivo JSON
        rowData = JsonUtility.FromJson<List<Dictionary<string, string>>>(jsonFile.text);

        // Generar las filas de la tabla
        GenerateTable();
    }

    private void GenerateTable()
    {
        // Crear una fila por cada entrada en los datos del JSON
        foreach (Dictionary<string, string> data in rowData)
        {
            // Crear una nueva instancia de la fila de la tabla
            GameObject rowObject = Instantiate(rowPrefab, contentParent);

            // Obtener los componentes de texto de la fila
            Text[] texts = rowObject.GetComponentsInChildren<Text>();

            // Rellenar los componentes de texto con los datos del JSON
            int i = 0;
            foreach (KeyValuePair<string, string> entry in data)
            {
                texts[i].text = entry.Value;
                i++;
            }
        }
    }
}
