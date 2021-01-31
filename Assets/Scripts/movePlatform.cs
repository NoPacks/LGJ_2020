using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    float movSpeed = 5;
    public bool moving ;
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    private void Update() {
        if (!moving) {
            transform.position = Vector2.MoveTowards(transform.position, start.position, movSpeed*Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, end.position, movSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Sing")) {
            moving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Sing")) {
            moving = false;
        }
    }
}
