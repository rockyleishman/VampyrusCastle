using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SoundManager :  Singleton<SoundManager>
{
public AudioSource BGAudioSource;
public AudioSource reactionAudioSource;
    public SoundData soundData;
    private bool isGameOver=false;
    private bool isPlay=false;

    private float tempPlayerHealth=100f;
    
    // Start is called before the first frame update
    void Start()
    {
        BGAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
        PlayBeginSound();
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
        PlayGameSound();
        }
        
    }
    void PlayBeginSound()
    {
        if (isPlay == false)
        {

                BGAudioSource.clip = soundData.beginSound;
                BGAudioSource.Play();
                isPlay = true;
            
        }
    }
    void PlayGameSound()
    {
        // Check if game is over at the beginning of the method
        if (DataManager.Instance.LevelDataObject.CrystalHP <= 0 || DataManager.Instance.PlayerDataObject.CurrentHP <= 0)
        {
            if (!isGameOver)  // Ensure game over sound only plays once
            {
                BGAudioSource.clip = soundData.GameOverSound;
                BGAudioSource.Play();
                isPlay = true;
                isGameOver = true;
            }
            return; // Don't proceed further if game is over
        }

        if (isPlay == false)
        {

                if(isGameOver == false)
                {
                    BGAudioSource.clip = soundData.GameSound;
                    BGAudioSource.Play();
                    isPlay = true;
                }
            
        }
    }
    public void PlayHitSound()
    {
        reactionAudioSource.PlayOneShot(soundData.hitSound);
    }
    
}
