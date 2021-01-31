using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerGenericDialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textmeshPro;
    [SerializeField] Image dialogBox;
    public void CloseMessage()
    {
        dialogBox.gameObject.SetActive(false);
    }

    public void DisplayGenericDialog(string message)
    {
        dialogBox.gameObject.SetActive(true);
        textmeshPro.SetText(message);
    }
}
