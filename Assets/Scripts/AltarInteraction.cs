using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarInteraction : MonoBehaviour
{
    [SerializeField] Canvas MessageCanvas;
    [SerializeField] PlayerSkills altarSkill;
    [SerializeField] string[] messages;

    bool triggerObjectEvent = false;
    bool playerWantInteract = false;
    MessageInteraction messageInteraction;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        messageInteraction = MessageCanvas.GetComponent<MessageInteraction>();
    }

    private void Update()
    {
        InteractWithObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if( (!other.GetComponent<PlayerController>().hasDoubleJump && altarSkill == PlayerSkills.doubleJump) ||
                    (!other.GetComponent<PlayerController>().hasDash && altarSkill == PlayerSkills.dash) )
            {
                messageInteraction.AskForInteraction();
                triggerObjectEvent = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        messageInteraction.CloseMessage();
        triggerObjectEvent = false;
        playerWantInteract = false;
    }

    private void InteractWithObject()
    {
        if (triggerObjectEvent)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (index < messages.Length)
                {
                    messageInteraction.DisplayInteractableMessage(messages[index]);
                    index += 1;
                }
                else
                {
                    messageInteraction.CloseMessage();
                    GameObject.Find("Player").GetComponent<PlayerController>().GetNewSkill(altarSkill);
                    index = 0;
                    triggerObjectEvent = false;
                }
            }
        }
    }
}
