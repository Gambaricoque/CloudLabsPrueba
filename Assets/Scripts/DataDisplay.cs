using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{
    public string jsonFilePath = "Assets/Scripts/estudiantes.json"; // Ruta del archivo JSON
    public Text displayText; // Referencia al componente TextMeshProUGUI

    [System.Serializable]
    public class BaseDatos
    {
        public List<Persona> datos;
    }

    [System.Serializable]
    public class Persona
    {
        public string nombre;
        public string apellido;
        public int codigo;
        public string correo;
        public int edad;
        public float nota;
    }

    private void Start()
    {
        // Leer el contenido del archivo JSON
        string jsonString = System.IO.File.ReadAllText(jsonFilePath);

        // Deserializar el JSON en un objeto BaseDatos
        BaseDatos baseDatos = JsonUtility.FromJson<BaseDatos>(jsonString);

        // Crear una cadena de texto para mostrar los datos
        string dataText = "";

        // Recorrer la lista de personas y agregar los datos a la cadena de texto
        foreach (Persona persona in baseDatos.datos)
        {
            dataText += "Nombre: " + persona.nombre + "\n";
            dataText += "Apellido: " + persona.apellido + "\n";
            dataText += "Código: " + persona.codigo + "\n";
            dataText += "Correo: " + persona.correo + "\n";
            dataText += "Edad: " + persona.edad + "\n";
            dataText += "Nota: " + persona.nota + "\n\n";
        }

        // Actualizar el texto en el componente TextMeshProUGUI
        displayText.text = dataText;
    }
}
