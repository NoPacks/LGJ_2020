using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Canvas MessageCanvas;
    [SerializeField] PlayerSkills altarSkill;

    bool triggerObjectEvent = false;
    bool playerWantInteract = false;
    MessageInteraction messageInteraction;
    // Start is called before the first frame update
    void Start()
    {
        messageInteraction = MessageCanvas.GetComponent<MessageInteraction>();
    }

    private void Update()
    {
        InteractWithObject();
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2");
        }
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
                Invoke("ShowObjectMessage", 0.2f);
            }
        }

        if(playerWantInteract)
        {
            messageInteraction.ShowMessage();
            if (Input.GetButtonDown("Fire2"))
            {
                GameObject.Find("Player").GetComponent<PlayerController>().GetNewSkill(altarSkill);
                messageInteraction.CloseMessage();
            }
        }

    }

    private void ShowObjectMessage()
    {
        playerWantInteract = true;
    }
}
