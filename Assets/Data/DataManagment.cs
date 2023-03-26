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
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.ToUpper().Contains("LEVEL"))
        {
            playerData.isBattle = false;
        }

        Debug.Log("----------------------------------------------Awake");
        if (playerData.newGame)
        {
            Debug.Log("----------------------------------------------newGame");
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.lifes = 13;
            playerData.sceneName = LoadSceneNames.LEVEL1_1_SCENE;
            playerData.playerPosition = new Vector3(0f, 0f, 0f);
            playerData.counterEnemies = 0;
            playerData.energyPoints = 100;
            drawUIEvent.Raise();
        }
        else {
            Debug.Log("----------------------------------------------else");
            if (!playerData.isBattle) {
                if (playerData.playerPosition != null
                && playerData.playerPosition.x != 0
                && playerData.playerPosition.y != 0
                && playerData.playerPosition.z != 0)
                {

                    Debug.Log("----------------------------------------------else if");
                    if (this.playerTransform != null)
                    {
                        Debug.Log("----------------------------------------------else if if");
                        this.playerTransform.position = playerData.playerPosition;
                    }
                    drawUIEvent.Raise();
                }
            }
            
            drawUIEvent.Raise();
        }
        
        drawUIEvent.Raise();
    }

    public void RecieveDamage()
    {
        playerData.lifePoints--;
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

    public void DiscountEnergy() {
        playerData.energyPoints--;
        if (playerData.energyPoints < 0) {
            playerData.energyPoints = 0;
        }
        drawUIEvent.Raise();
    }

    public void UpdateEnergy() {

        playerData.energyPoints = playerData.energyPoints + 5;
        if (playerData.energyPoints > 100) {
            playerData.energyPoints = 100;
        }
        drawUIEvent.Raise();
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
            playerData.energyPoints = 100;
            playerData.counterEnemies = 0;

        }



        //Debug.Log(Application.persistentDataPath);
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileNameCurrent, false);

        string objString = JsonUtility.ToJson(playerData);
        sw.WriteLine(objString);
        sw.Close();
    }


    public void LoadDataCurrent()
    {
        SaveStatsCurrent();


        //Debug.Log(Application.persistentDataPath);

        if (File.Exists(Application.persistentDataPath + "/" + fileNameCurrent))
        {
            //Debug.Log("File with data");
            sr = new StreamReader(Application.persistentDataPath + "/" + fileNameCurrent);
            string objString = sr.ReadToEnd();
            //Debug.Log(objString);
            PlayerDataVO playerVO = JsonUtility.FromJson<PlayerDataVO>(objString);
            if (playerVO != null)
            {
                if (playerData != null && playerData.energyPoints > 0) { 
                
                }
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
            playerData.energyPoints = 100;
            playerData.counterEnemies = 0;

        }
        playerData.isBattle = false;

        if (SceneManager.GetActiveScene().name.ToUpper().Contains("BATTLE")) {
            SceneManager.LoadScene(playerData.sceneName, LoadSceneMode.Single);
        }
            

    }

    public void populationVO(PlayerData playerDatToLoad, PlayerDataVO playerVO)
    {
        playerDatToLoad.currentLevel = playerVO.currentLevel;
        playerDatToLoad.lifePoints = playerVO.lifePoints;
        playerDatToLoad.lifes = playerVO.lifes;
        playerDatToLoad.sceneName = playerVO.sceneName;
        playerDatToLoad.playerPosition = playerVO.playerPosition;
        playerDatToLoad.newGame = playerVO.newGame;
        playerDatToLoad.energyPoints = playerVO.energyPoints;
        playerDatToLoad.counterEnemies = playerVO.counterEnemies;
    }


    public void SaveStatsCurrent()
    {
        
        //Debug.Log(Application.persistentDataPath);
        PlayerData loadPlayerDataCurrent = new PlayerData();
        if (File.Exists(Application.persistentDataPath + "/" + fileNameCurrent))
        {
            //Debug.Log("File with data");
            sr = new StreamReader(Application.persistentDataPath + "/" + fileNameCurrent);
            string objStringLoad = sr.ReadToEnd();
            //Debug.Log(objStringLoad);
            PlayerDataVO playerVO = JsonUtility.FromJson<PlayerDataVO>(objStringLoad);
            if (playerVO != null)
            {
                
                populationVO(loadPlayerDataCurrent, playerVO);
            }


            sr.Close();
        }

        loadPlayerDataCurrent.isBattle = false;

        if (loadPlayerDataCurrent != null)
        {

            //Debug.Log(Application.persistentDataPath);
            sw = new StreamWriter(Application.persistentDataPath + "/" + fileNameCurrent, false);
            loadPlayerDataCurrent.lifePoints = playerData.lifePoints;
            loadPlayerDataCurrent.energyPoints = playerData.energyPoints;
            loadPlayerDataCurrent.canPunch = playerData.canPunch;
            loadPlayerDataCurrent.itmes = playerData.itmes;
            loadPlayerDataCurrent.counterItems = playerData.counterItems;
            loadPlayerDataCurrent.isBattle = false;

            string objString = JsonUtility.ToJson(loadPlayerDataCurrent);
            //Debug.Log(objString);
            sw.WriteLine(objString);
            sw.Close();

        }



       
    }

}
