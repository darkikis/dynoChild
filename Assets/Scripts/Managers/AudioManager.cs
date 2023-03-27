using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] listAudio;

    private AudioSource audioSource;

    private AudioSource audioSourceAmbient;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject audioAmbient = this.transform.Find("AudioAmbient").gameObject;

        GameObject audioAmbientGO = GameObject.Find("AudioAmbient");
        if (audioAmbientGO != null)
        {
            audioSourceAmbient = audioAmbientGO.GetComponent<AudioSource>();
            audioSourceAmbient.Play();
        }
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
            audioSource.PlayOneShot(listAudio[AudioNames.START]);
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
        audioSource.PlayOneShot(listAudio[AudioNames.INTRO]);
    }

    public void StopMusic() {
        audioSource.Stop();
    }

    public void PlayStep()
    {
        audioSource.PlayOneShot(listAudio[AudioNames.STEP]);
        
        //audioSource.Stop();
        //audioSource.loop = true;
        audioSource.clip = listAudio[AudioNames.STEP];
        audioSource.volume = 0.8f;
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        
        
    }

    public void StopStep()
    {   
        //AudioClip listAudio[AudioNames.STEP].s
        //audioSource.PlayOneShot();
        /*
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = listAudio[AudioNames.STEP];
        audioSource.volume = 0.8f;
        audioSource.Play();
        */
    }

    public void PlayTakeEnergy() {
        audioSource.PlayOneShot(listAudio[AudioNames.TAKE_ENERGY]);
    }

    public void PlayPause()
    {
        audioSource.PlayOneShot(listAudio[AudioNames.PAUSE]);
    }

    public void PlayContinue()
    {
        audioSource.PlayOneShot(listAudio[AudioNames.CONTINUE]);
    }


    public void PlayDamagePlayer()
    {
        audioSource.PlayOneShot(listAudio[AudioNames.RECEIVE_DAMAGE_PLAYER]);
    }

}

