using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManagment : MonoBehaviour
{
    public PlayerData playerData;

    public Transform playerTransform;

    public GameEvent drawUIEvent;

    public GameEvent respawnPlayerEvent;

    public string fileName = "dataGame.json";

    public string fileNameCurrent = "currentData.json";

    private StreamWriter sw;
    private StreamReader sr;


    void Start()
    {
   
        drawUIEvent.Raise();

    }

    private void Awake()
    {
        Debug.Log("----------------------------------------------Awake");
        if (playerData.newGame)
        {
            Debug.Log("----------------------------------------------newGame");
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 13;
            playerData.sceneName = LoadSceneNames.LEVEL1_1_SCENE;
            playerData.playerPosition = new Vector3(0f, 0f, 0f);
        }
        else {
            Debug.Log("----------------------------------------------else");
            if (playerData.playerPosition != null
                && playerData.playerPosition.x != 0
                && playerData.playerPosition.y != 0
                && playerData.playerPosition.z != 0) {

                Debug.Log("----------------------------------------------else if");
                if (this.playerTransform != null) {
                    Debug.Log("----------------------------------------------else if if");
                    this.playerTransform.position = playerData.playerPosition;
                }
                
            }
        }
        drawUIEvent.Raise();
    }

    public void RecieveDamage()
    {
        playerData.lifePoints -= 5;
        if (playerData.lifePoints <= 0) {
            this.Die();
            this.RestoreLifePoints();
            this.respawnPlayerEvent.Raise();
        }
        drawUIEvent.Raise();
    }

    public void TakeItem()
    {
        playerData.itmes++;
        drawUIEvent.Raise();
    }

    public void SetWorld(int index)
    {
        playerData.currentLevel = index;
        drawUIEvent.Raise();
    }

    public void Die() {
        Debug.Log("die....");
        playerData.lifePoints = 0;
        playerData.lifes--;
        drawUIEvent.Raise();
    }

    public void RestoreLifePoints() { 
        playerData.lifePoints = 100;
        drawUIEvent.Raise();
    }

    public void CounterEnemies()
    {
        playerData.counterEnemies++;
        drawUIEvent.Raise();
    }

    public void CounterItems()
    {
        playerData.counterItems++;
        drawUIEvent.Raise();
    }

    public void SetNewGame() {
        Debug.Log("**********************NEW_GAME");
        playerData.newGame = true;
    }


    public void SaveDataCurrent()
    {


        if (playerData != null)
        {

            
            playerData.sceneName = SceneManager.GetActiveScene().name;
            playerData.playerPosition = new Vector3(this.playerTransform.position.x, this.playerTransform.position.y, this.playerTransform.position.z);
            playerData.newGame = false;
            playerData.isBattle = true;

        }
        else
        {
            playerData = new PlayerData();
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 10;
            playerData.sceneName = LoadSceneNames.LEVEL1_1_SCENE;
            playerData.newGame =  true;
            playerData.isBattle = false;

        }



        Debug.Log(Application.persistentDataPath);
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileNameCurrent, false);

        string objString = JsonUtility.ToJson(playerData);
        sw.WriteLine(objString);
        sw.Close();
    }


    public void LoadDataCurrent()
    {

        
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
                populationVO(playerData, playerVO);
            }


            sr.Close();
        }
        else
        {
            Debug.Log("No file with data");
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 10;
            playerData.sceneName = LoadSceneNames.LEVEL1_1_SCENE;

        }
        playerData.isBattle = false;
        SceneManager.LoadScene(playerData.sceneName, LoadSceneMode.Single);

    }

    public void populationVO(PlayerData playerDatToLoad, PlayerDataVO playerVO)
    {
        playerDatToLoad.currentLevel = playerVO.currentLevel;
        playerDatToLoad.lifePoints = playerVO.lifePoints;
        playerDatToLoad.lifes = playerVO.lifes;
        playerDatToLoad.sceneName = playerVO.sceneName;
        playerDatToLoad.playerPosition = playerVO.playerPosition;
        playerDatToLoad.newGame = playerVO.newGame;
    }
}
