using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneManager : MonoBehaviour
{
    public GameEvent stopMusicEvent;
    public void LoadLevel1_1Scene()
    {
        
            SceneManager.LoadScene("Level1-1", LoadSceneMode.Single);
            stopMusicEvent.Raise();
            
        
        
    }

    public void LoadBattle1_1Scene() {
        SceneManager.LoadScene("Battle1-1", LoadSceneMode.Single);
        stopMusicEvent.Raise();
    }
}
