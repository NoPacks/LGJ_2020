using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textmeshPro;
    [SerializeField] Image dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.gameObject.SetActive(false);
    }

    public void AskForInteraction()
    {
        dialogBox.gameObject.SetActive(true);
        textmeshPro.SetText("Interactuar?");
    }

    public void CloseMessage()
    {
        dialogBox.gameObject.SetActive(false);
    }

    public void DisplayInteractableMessage(string message)
    {
        textmeshPro.SetText(message);
    }

    public void OpenMessage()
    {
        dialogBox.gameObject.SetActive(true);
    }
    public void DisplayGenericDialog(string message)
    {
        textmeshPro.SetText(message);
    }
}
