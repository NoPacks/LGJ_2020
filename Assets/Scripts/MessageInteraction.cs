using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textmeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro.enabled = false;
    }

    public void AskForInteraction()
    {
        textmeshPro.enabled = true;
        textmeshPro.SetText("Interactuar?");
    }
    public void ShowMessage()
    {
        textmeshPro.SetText("¿Rezar al altar para seguir avanzando?");
    }

    public void CloseMessage()
    {
        textmeshPro.enabled = false;
    }

    public void DisplayInteractableMessage(string message)
    {
        textmeshPro.SetText(message);
    }
}
