using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerProfileManager : MonoBehaviour
{

    public static PlayerProfileManager instance;

    public PlayerData playerData;
    public Transform playerPostion;


    public LoaderSceneManager loaderSceneManager;

    

    public string fileName;

    private string fileNameCurrent = "currentData.json";

    private StreamWriter sw;
    private StreamReader sr;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadData()
    {
        Debug.Log("LoadData...");
        bool isExistFileToLoad = false;

        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            sr = new StreamReader(Application.persistentDataPath + "/" + fileName);
            string objString = sr.ReadToEnd();
            PlayerDataVO playerVO = JsonUtility.FromJson<PlayerDataVO>(objString);
            if (playerVO != null)
            {
                playerData.currentLevel = playerVO.currentLevel;
                playerData.lifePoints = playerVO.lifePoints;
                playerData.lifes = playerVO.lifes;
                playerData.sceneName = playerVO.sceneName;
                playerData.counterEnemies = playerVO.counterEnemies;
            }
            isExistFileToLoad = true;
            sr.Close();
        }
        else
        {
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 10;
            playerData.sceneName = LoadSceneNames.LEVEL1_1_SCENE;
            playerData.counterEnemies = 0;

        }

        if (playerData.sceneName != null) {

            if (!isExistFileToLoad) { 
                this.SaveDataCurrent();
            }
            Debug.Log("::::" + playerData.sceneName);
            loaderSceneManager.LoadSceneByName(playerData.sceneName);
            
        }

        

    }

    public PlayerData LoadDataCurrent()
    {
        PlayerData playerDatToLoad = new PlayerData();

        Debug.Log(Application.persistentDataPath);

        if (File.Exists(Application.persistentDataPath + "/" + fileNameCurrent))
        {
            Debug.Log("File with data");
            sr = new StreamReader(Application.persistentDataPath + "/" + fileNameCurrent);
            string objString = sr.ReadToEnd();
            Debug.Log(objString);
            PlayerDataVO playerVO = JsonUtility.FromJson<PlayerDataVO>(objString);
            if(playerVO != null){
                playerDatToLoad.currentLevel = playerVO.currentLevel;
                playerDatToLoad.lifePoints = playerVO.lifePoints;
                playerDatToLoad.lifes = playerVO.lifes;
                playerDatToLoad.sceneName = playerVO.sceneName;
            }


            sr.Close();
        }else{
            Debug.Log("No file with data");
            playerDatToLoad.currentLevel = 1;
            playerDatToLoad.lifePoints = 100;
            playerDatToLoad.lifes = 10;
            playerDatToLoad.sceneName = LoadSceneNames.LEVEL1_1_SCENE;

        }

        return playerDatToLoad;
    }


    public void SaveDataCurrent()
    {


        if (playerData != null)
        {
            
            if (playerData.sceneName == null)
            {
                playerData.sceneName = SceneManager.GetActiveScene().name;
            }
            playerData.playerPosition = new Vector3(this.playerPostion.position.x, this.playerPostion.position.y, this.playerPostion.position.z);
            playerData.playerTransform = playerPostion;

        }
        else {
            playerData = new PlayerData();
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 10;

        }

        
        
        Debug.Log(Application.persistentDataPath);
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileNameCurrent, false);

        string objString = JsonUtility.ToJson(playerData);
        sw.WriteLine(objString);
        sw.Close();
    }


    public void ReturnLevelExplorerCurrent()
    {
        PlayerData playerDatToLoad = new PlayerData();

        Debug.Log(Application.persistentDataPath);

        if (File.Exists(Application.persistentDataPath + "/" + fileNameCurrent))
        {
            Debug.Log("File with data");
            sr = new StreamReader(Application.persistentDataPath + "/" + fileNameCurrent);
            string objString = sr.ReadToEnd();
            Debug.Log(objString);
            PlayerDataVO playerVO = JsonUtility.FromJson<PlayerDataVO>(objString);
            if (playerVO != null)
            {
                playerDatToLoad.currentLevel = playerVO.currentLevel;
                playerDatToLoad.lifePoints = playerVO.lifePoints;
                playerDatToLoad.lifes = playerVO.lifes;
                playerDatToLoad.sceneName = playerVO.sceneName;
            }


            sr.Close();
        }
        else
        {
            Debug.Log("No file with data");
            playerDatToLoad.currentLevel = 1;
            playerDatToLoad.lifePoints = 100;
            playerDatToLoad.lifes = 10;
            playerDatToLoad.sceneName = LoadSceneNames.LEVEL1_1_SCENE;

        }
        loaderSceneManager.LoadSceneByName(playerDatToLoad.sceneName);


    }
}
