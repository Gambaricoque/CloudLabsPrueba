using UnityEngine;
using UnityEngine.UI;

public class CheckboxColorChanger : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Puedes cambiar KeyCode.Space por cualquier tecla que desees
        {
            CambiarColor();
        }
    }

    private void CambiarColor()
    {
        if (image.color == Color.red)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
    }
}
