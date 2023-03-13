using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneManager : MonoBehaviour
{
    public GameEvent stopMusicEvent;
    public void LoadLevel11Scene()
    {
        Debug.Log("LoadLevel11Scene");
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        
            SceneManager.LoadScene("Level1-1", LoadSceneMode.Single);
            stopMusicEvent.Raise();
            
        
        
    }
}
