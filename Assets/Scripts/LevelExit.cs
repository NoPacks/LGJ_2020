using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowMoFactor = 0.2f;
    [SerializeField] ParticleSystem particlesEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        ShowExitEffect();
        Time.timeScale = levelExitSlowMoFactor;
        yield return new WaitForSeconds(levelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void ShowExitEffect()
    {
        ParticleSystem partycleSystem = GameObject.Instantiate(particlesEffect, transform.position, transform.rotation);
    }
}
