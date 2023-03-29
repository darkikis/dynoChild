using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameEvent stopMusicEvent;
    public GameEvent setNewGameEvent;


    public void LoadLevel1_1Scene()
    {
        
            SceneManager.LoadScene(LoadSceneNames.LEVEL1_1_SCENE, LoadSceneMode.Single);
            stopMusicEvent.Raise();
            setNewGameEvent.Raise();
            
        
        
    }

    public void LoadBattle1_1Scene() {
        SceneManager.LoadScene(LoadSceneNames.BATTLE1_1_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
    }


    public void LoadSceneByName(string nameScene)
    {
        if (string.IsNullOrEmpty(nameScene)) {
            nameScene = LoadSceneNames.LEVEL1_1_SCENE;
        }
        SceneManager.LoadScene(nameScene, LoadSceneMode.Single);
    }


    public void NewGameLoadScene()
    {
        stopMusicEvent.Raise();
        setNewGameEvent.Raise();
        SceneManager.LoadScene(LoadSceneNames.LEVEL1_1_SCENE, LoadSceneMode.Single);
        

    }

    public void HomeLoadScene()
    {
        stopMusicEvent.Raise();
        //setNewGameEvent.Raise();
        SceneManager.LoadScene(LoadSceneNames.MAIN_SCENE, LoadSceneMode.Single);


    }

    public void LoadLevel1_2Scene()
    {

        SceneManager.LoadScene(LoadSceneNames.LEVEL1_2_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
        setNewGameEvent.Raise();

    }

    public void LoadLevel1_3Scene()
    {

        SceneManager.LoadScene(LoadSceneNames.LEVEL1_3_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
        setNewGameEvent.Raise();

    }

    public void LoadBattle1_2Scene()
    {
        SceneManager.LoadScene(LoadSceneNames.BATTLE1_2_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
    }

    public void LoadBattle1_3Scene()
    {
        SceneManager.LoadScene(LoadSceneNames.BATTLE1_3_SCENE, LoadSceneMode.Single);
        stopMusicEvent.Raise();
    }
}
