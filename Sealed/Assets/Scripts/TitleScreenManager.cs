using System.Collections; // Needed for IEnumerator
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScreenScript : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public string levelName;
    public Slider volumeSlider; // Reference to the volume slider
    public AudioSource backgroundMusic; // Reference to the AudioSource

    private bool isSettingsOpen = false; // Toggle for settings visibility

    private void Start()
    {
        StartCoroutine(FadeIn());

        // Initialize slider if assigned
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(AdjustVolume);
            volumeSlider.gameObject.SetActive(false); // Hide slider initially
        }

        // Start playing background music if assigned
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void PlayGame()
    {
        StartCoroutine(FadeOutAndLoadScene(levelName));
    }

    public void OpenSettings()
    {
        if (volumeSlider != null)
        {
            isSettingsOpen = !isSettingsOpen; // Toggle settings state
            volumeSlider.gameObject.SetActive(isSettingsOpen); // Show/hide slider
        }
        else
        {
            Debug.LogWarning("VolumeSlider is not assigned in the Inspector!");
        }
    }

    public void AdjustVolume(float value)
    {
        AudioListener.volume = value; // Update the global game volume

        if (backgroundMusic != null)
        {
            backgroundMusic.volume = value; // Adjust the background music volume
        }
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

