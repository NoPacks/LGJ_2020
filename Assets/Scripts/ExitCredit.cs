using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCredit : MonoBehaviour
{
    LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = gameObject.GetComponent<LevelLoader>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(GoToMainMenu());
        }
    }

    IEnumerator GoToMainMenu()
    {
        Debug.Log("MainMenu");
        yield return new WaitForSeconds(3.0f);
        levelLoader.LoadMainMenu();
    }
}
