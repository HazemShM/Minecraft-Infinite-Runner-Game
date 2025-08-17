using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;

    [Header("Background Music Clips")]
    [SerializeField]
    private AudioClip [] backgroundMusic;

    [Header("Sound Effects")]
    [SerializeField]
    private AudioClip [] soundEffects;

    private AudioSource musicSource;
    private AudioSource sfxSource;


    private bool isMuted = false;

    private void Awake()
    {
        if (manager == null){
            manager = this;
            DontDestroyOnLoad(gameObject);
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; 
            // AudioListener.volume  = 0.8f;

        }else{
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {   
        isMuted = !isMuted;
        if(isMuted){
            AudioListener.volume = 0f;
        }else{
            AudioListener.volume  = 1f;
        }
    }
    public bool MuteStatus(){
        return isMuted;
    }

    public void PlayMusic(String name)
    {   
        if(name == "Main Theme"){
            musicSource.volume = 1.3f;
        }else{
            musicSource.volume = 1f;
        }
        AudioClip clip = null;
		for (int i = 0; i < backgroundMusic.Length; i++){
			if (backgroundMusic[i].name == name){
				clip = backgroundMusic[i];
                break;
			}
		}
		if (clip == null || musicSource.clip == clip) return; 
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(String name)
    {    
        AudioClip clip = null;
		for (int i = 0; i < soundEffects.Length; i++){
			if (soundEffects[i].name == name){
				clip = soundEffects[i];
                break;
			}
		}
        sfxSource.PlayOneShot(clip);
    }

}
