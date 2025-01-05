using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    // Reference to the item audio source
    public AudioSource soundtrackSource;
    public AudioClip soundtrackClip;
    public float volume = 0.25f;
    private Scene gameplayScene;

    public static SoundtrackManager instance;
    private void Awake()
    {
        // Keep soundtrack playing if GameplayScene is reloaded
        gameplayScene = SceneManager.GetActiveScene();
        if (gameplayScene.name == "GameplayScene")
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (instance != null)
            {
                instance.gameObject.SetActive(false);
                instance = null;
                Destroy(instance);
            }
            
            //PlaySoundTrack();

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        PlaySoundTrack();
    }

    private void PlaySoundTrack()
    {
        soundtrackSource.clip = soundtrackClip;
        soundtrackSource.volume = volume; // set the volume
        soundtrackSource.loop = true; // play on loop
        soundtrackSource.Play(); // start the sound    
    }

}
