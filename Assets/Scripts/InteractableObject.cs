using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] Canvas messageCanvas;
    [SerializeField] string[] messages;

    MessageInteraction messageInteraction;
    bool triggerObjectEvent = false;
    int index = 0;
    // Start is called before the first frame update
    private void Start()
    {
        messageInteraction = messageCanvas.GetComponent<MessageInteraction>();
    }

    private void Update()
    {
        ShowNextMessage();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            messageInteraction.AskForInteraction();
            triggerObjectEvent = true;
            index = 0;
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
                    messageInteraction.DisplayInteractableMessage(messages[index]);
                    index += 1;
                }
                else
                {
                    messageInteraction.CloseMessage();
                    index = 0;
                    triggerObjectEvent = false;
                }
            }
        }
    }
}
