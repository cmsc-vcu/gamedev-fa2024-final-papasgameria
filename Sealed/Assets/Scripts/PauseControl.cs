using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    float previousTimeScale = 1;
    public TextMeshProUGUI pauseLabel;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }

    void TogglePause(){
        if(Time.timeScale > 0){
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            pauseLabel.enabled = true;
        } else {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            pauseLabel.enabled = false;
        }
    }
}
