using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HUDPanel;
    public GameObject pausePanel;

    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI currentLevel;

    public PlayerData playerData;
    void Start()
    {
        showHUD();
    }


    private void CleanPanel()
    {
        HUDPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void showHUD()
    {
        CleanPanel();
        HUDPanel.SetActive(true);
    }

    public void showPause()
    {
        CleanPanel();
        pausePanel.SetActive(true);
    }

    public void DrawPlayerStats()
    {
        itemsText.text = playerData.itmes.ToString();

        lifesText.text = playerData.lifes.ToString();
        currentLevel.text = "W-" + playerData.currentLevel;
    }
}
