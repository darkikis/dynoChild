using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneManager : MonoBehaviour
{
    public GameEvent stopMusicEvent;
    public void LoadLevel1_1Scene()
    {
        
            SceneManager.LoadScene(LoadSceneNames.LEVEL1_1_SCENE, LoadSceneMode.Single);
            stopMusicEvent.Raise();
            
        
        
    }

    public void LoadBattle1_1Scene() {
        SceneManager.LoadScene(LoadSceneNames.BATTLE1_1_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
    }


    public void LoadSceneByName(string nameScene)
    {
        SceneManager.LoadScene(nameScene, LoadSceneMode.Single);
    }
}
