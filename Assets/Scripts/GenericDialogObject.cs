using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDialogObject : MonoBehaviour
{
    [SerializeField] Canvas messageCanvas;
    [SerializeField] string[] messages;

    MessageInteraction messageInteraction;
    bool triggerObjectEvent = false;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        messageInteraction = messageCanvas.GetComponent<MessageInteraction>();
    }
    private void Update()
    {
        ShowNextMessage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && messages.Length > 0)
        {
            messageInteraction.OpenMessage();
            messageInteraction.DisplayGenericDialog(messages[index]);
            triggerObjectEvent = true;
            index += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        messageInteraction.CloseMessage();
        triggerObjectEvent = false;
        index = 0;
    }
    private void ShowNextMessage()
    {
        if (triggerObjectEvent)
        { 
            if (Input.GetButtonDown("Fire2"))
            {
                if(index < messages.Length)
                {
                    messageInteraction.DisplayGenericDialog(messages[index]);
                    index += 1;
                }
                else
                {
                    messageInteraction.CloseMessage();
                    triggerObjectEvent = false;
                    index = 0;
                    StartCoroutine("DestroyThisObject");
                }
            }
        }
    }


    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject.Destroy(this.gameObject);
    }

}
