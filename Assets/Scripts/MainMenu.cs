using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   
    [SerializeField] private Text muteText;
    void Start()
    {
        AudioManager.manager.PlayMusic("Main Theme");
        if(!AudioManager.manager.MuteStatus()){
            muteText.text = "ON";
        }else{
            muteText.text = "OFF";
        }
    }
    public void Play(){
        SceneManager.LoadScene("Game Scene");
        // DynamicGI.UpdateEnvironment();
    }
    public void Quit(){
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void ToggleMute()
    {   
        AudioManager.manager.ToggleMute();
        if(AudioManager.manager.MuteStatus()){
            muteText.text = "OFF";
        }else{
            muteText.text = "ON";
        }
    }
    public void OnButtonClick(){
        AudioManager.manager.PlaySFX("click");
    }
}
