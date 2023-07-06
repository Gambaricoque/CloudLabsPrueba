using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class EstudiantesManager : MonoBehaviour
{
    public Transform contenedorEstudiantes;
    public Transform contenedorAprobados;
    public Transform contenedorReprobados;
    public GameObject estudiantePrefab;
    public Text alertaText;
    
    public const string jsonFilePath = "Assets/Scripts/DatosEstudiantes.json";

    private List<GameObject> estudiantes;
    private int estudiantesAprobados = 0;
    private int estudiantesReprobados = 0;
    


    public void Start()
    {
        // Cargar datos de estudiantes desde el archivo JSON
        
        string json = File.ReadAllText(jsonFilePath);
        Estudiante[] estudiantesData = JsonConvert.DeserializeObject<Estudiante[]>(json);
        

        // Crear estudiantes
        estudiantes = new List<GameObject>();
        foreach (Estudiante estudiante in estudiantesData)
        {
            GameObject nuevoEstudiante = Instantiate(estudiantePrefab, contenedorEstudiantes);
            nuevoEstudiante.GetComponentInChildren<Text>().text = estudiante.nombre + " " + estudiante.apellido;
            nuevoEstudiante.GetComponent<EstudianteDragHandler>().esAprobado = estudiante.aprobado;
            estudiantes.Add(nuevoEstudiante);
        }
    }

    public void VerificarUbicacionCorrecta()
    {
        int estudiantesIncorrectos = 0;

        foreach (GameObject estudiante in estudiantes)
        {
            EstudianteDragHandler dragHandler = estudiante.GetComponent<EstudianteDragHandler>();

            if (dragHandler != null && dragHandler.EnUbicacionCorrecta() != dragHandler.esAprobado)
            {
                estudiantesIncorrectos++;
                estudiante.GetComponent<Image>().color = Color.red;
            }
            else
            {
                estudiante.GetComponent<Image>().color = Color.green;
            }
        }

        if (estudiantesIncorrectos > 0)
        {
            alertaText.text = "Revisa la ubicación de los estudiantes incorrectos";
        }
        else
        {
            alertaText.text = "¡Felicidades! Todos los estudiantes están ubicados correctamente.";
        }
    }

    public void IncrementarContador(bool esAprobado)
    {
        if (esAprobado)
            estudiantesAprobados++;
        else
            estudiantesReprobados++;

        if (estudiantesAprobados + estudiantesReprobados == estudiantes.Count)
            VerificarUbicacionCorrecta();
    }
}

