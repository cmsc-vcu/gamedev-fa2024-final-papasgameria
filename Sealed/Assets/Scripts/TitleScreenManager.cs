using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeDuration = 1f; 
    public string levelName;

    private void Start()
    {

        StartCoroutine(FadeIn());
    }

    public void PlayGame()
    {
        StartCoroutine(FadeOutAndLoadScene(levelName)); 
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked");
    }

    public void ExitGame()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; 
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }
}
