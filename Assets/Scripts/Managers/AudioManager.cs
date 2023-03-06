using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] listAudio;

    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDamage()
    {
        audioSource.PlayOneShot(listAudio[0]);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(listAudio[1]);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(listAudio[2]);
    }

    public void PlayStart()
    {
        try
        {
            audioSource.PlayOneShot(listAudio[0]);
        }
        catch (UnityException e)
        {
            Debug.Log(e.ToString());
           
        }
       
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(listAudio[4]);
    }

    public void PlayItem()
    {
        audioSource.PlayOneShot(listAudio[5]);
    }

    public void PlayInitGame() {
        Debug.Log("PlayIntro");
        audioSource.PlayOneShot(listAudio[1]);
    }

    public void StopMusic() {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
