using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Handling_Files_View : MonoBehaviour
{
    public Text displayText; // Referencia al componente text
    public float maxHeight = 1000f; // Altura máxima del componente Text
    

    void Start()
    {
        string dataText = ReadingJsonFile();
        //displayText.text = dataText;//
        UpdateTextComponent(dataText);
    }

    void UpdateTextComponent(string newText)
    {
        displayText.text = newText;

        // Ajustar dinámicamente la altura del componente Text según el contenido
        TextGenerator textGenerator = new TextGenerator();
        TextGenerationSettings generationSettings = displayText.GetGenerationSettings(displayText.rectTransform.rect.size);
        float textHeight = textGenerator.GetPreferredHeight(displayText.text, generationSettings);

        displayText.rectTransform.sizeDelta = new Vector2(displayText.rectTransform.sizeDelta.x, Mathf.Min(textHeight, maxHeight));
    }

    static string ReadingJsonFile()
    {
        string path = Path.Combine(Application.dataPath, "Scripts/estudiantes.json");
        try
        {
            string jsonString = File.ReadAllText(path);
            ContainerParsedJson jsonParsedTransformed = JsonUtility.FromJson<ContainerParsedJson>(jsonString);
            string dataText = "";
            foreach (Student studentData in jsonParsedTransformed.basedatos)
            {
               dataText += "Nombre: " + studentData.nombre + "\t";
                dataText += "Apellido: " + studentData.apellido + "\t";
                dataText += "Codigo: " + studentData.codigo + "\t";
                dataText += "Nota: " + studentData.nota + "\t";
                dataText += "Correo: " + studentData.correo + "\t";
                dataText += "Edad: " + studentData.edad + "\n";
                //aqui deberia venir parte del codigo del checkbox
            }
            return dataText;
        }
        catch (Exception e)
        {
            Debug.Log("Fatal error reading JSON file: " + e);
            return "Cannot handle JSON metadata";
        }
    }
}

[System.Serializable]
public class Student
{
    public string nombre;
    public string apellido;
    public int codigo;
    public string correo;
    public float nota;
    public int edad;
}

[System.Serializable]
public class ContainerParsedJson
{
    public Student[] basedatos;
}
