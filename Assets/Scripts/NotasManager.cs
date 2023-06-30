using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class NotasManager : MonoBehaviour
{
    public Text nombreText;
    public Text apellidoText;
    public Text codigoText;
    public Text correoText;
    public Text edadText;
    public Toggle aprobadoToggle;
    public Text notaText;

    private List<Estudiante> estudiantes;
    private int currentIndex = 0;
    
    public Button verificarButton;

    private const string jsonFilePath = "Assets/Scripts/DatosEstudiantes.json";

    void Start()
    {
        // Cargar los datos de estudiantes desde el archivo JSON
        LoadEstudiantesData();

        // Mostrar los datos del estudiante actual en la interfaz
        ShowEstudianteData(currentIndex);
    }

    // Cargar los datos de estudiantes desde el archivo JSON
    private void LoadEstudiantesData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(jsonData);
        }
        else
        {
            Debug.LogError("El archivo DatosEstudiantes.json no existe.");
            estudiantes = new List<Estudiante>();
        }
    }

    // Mostrar los datos del estudiante actual en la interfaz
    private void ShowEstudianteData(int index)
    {
        Estudiante estudiante = estudiantes[index];

        nombreText.text = estudiante.nombre;
        apellidoText.text = estudiante.apellido;
        codigoText.text = estudiante.codigo;
        correoText.text = estudiante.correo;
        edadText.text = estudiante.edad;
        notaText.text = estudiante.nota.ToString();
        aprobadoToggle.isOn = estudiante.aprobado;
    }

    // Guardar los datos de estudiantes en el archivo JSON
    private void SaveEstudiantesData()
    {
        string jsonData = JsonConvert.SerializeObject(estudiantes, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    // Acción del botón Siguiente
    public void NextButton()
    {
        currentIndex = (currentIndex + 1) % estudiantes.Count;
        ShowEstudianteData(currentIndex);
    }

    // Acción del botón Anterior
    public void PreviousButton()
    {
        currentIndex = (currentIndex + estudiantes.Count - 1) % estudiantes.Count;
        ShowEstudianteData(currentIndex);
    }

    // Acción del botón Guardar
    public void SaveButton()
    {
        float nota;
        if (float.TryParse(notaText.text, out nota))
        {
            // Actualizar la nota y la casilla de aprobado/reprobado del estudiante actual
            estudiantes[currentIndex].nota = nota;
            estudiantes[currentIndex].aprobado = aprobadoToggle.isOn;

            // Guardar los cambios en el archivo JSON
            SaveEstudiantesData();
        }
        else
        {
            Debug.LogError("Formato de nota inválido.");
        }
    }

    // Método para ajustar la nota a un valor proporcional de 0 a 100 o revertirlo
    public void AjustarNotaProporcional()
    {
        estudiantes[currentIndex].estadoNota = !estudiantes[currentIndex].estadoNota;

        if (estudiantes[currentIndex].estadoNota)
        {
            float notaActual = estudiantes[currentIndex].nota;
            float notaProporcional = notaActual * 20f;
            notaText.text = notaProporcional.ToString();
            estudiantes[currentIndex].nota=notaProporcional;
        }
        else
        {
            float notaActual = float.Parse(notaText.text);
            float notaOriginal = notaActual / 20f;
            notaText.text = notaOriginal.ToString();
            estudiantes[currentIndex].nota = notaOriginal;
        }

        SaveButton();
    }
    // Método para verificar las notas aprobadas y mostrar una alerta si hay casillas marcadas incorrectamente
    public void VerificarNotasAprobadasescala5()
    {
        foreach (Estudiante estudiante in estudiantes)
        {

            if (estudiante.estadoNota)
            {
                if ((estudiante.nota >= 60f && estudiante.aprobado)||(estudiante.nota < 60f && estudiante.aprobado==false))
                {
                    Debug.Log(estudiante.nombre +" "+ "esta marcado correctamente");
                }
                else
                {
                    Debug.LogError(estudiante.nombre + " " + "esta marcado incorrectamente");    
                }
            }

            else
            {
                
                if ((estudiante.nota >= 3f && estudiante.aprobado) || (estudiante.nota < 3f && estudiante.aprobado == false))
                {
                    Debug.Log(estudiante.nombre + " " + "esta marcado correctamente");
                }
                else
                {
                    Debug.LogError(estudiante.nombre + " " + "esta marcado incorrectamente");
                }

            }
        }
    
    }


}

[System.Serializable]
public class Estudiante
{
    public string nombre;
    public string apellido;
    public string codigo;
    public string correo;
    public string edad;
    public float nota;
    public bool aprobado;
    public bool estadoNota;
}
