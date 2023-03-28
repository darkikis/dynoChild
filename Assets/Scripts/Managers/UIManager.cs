using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HUDPanel;
    public GameObject pausePanel;
    public GameObject exitPanel;
    public GameObject continueExpPanel;

    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI currentLevel;
    public Slider sliderLifePoints;
    public Slider sliderEnergyPoints;
    public GameObject punchIcon;
    public GameObject energyIcon;

    public PlayerData playerData;
    void Start()
    {
        showHUD();
    }


    private void CleanPanel()
    {
        HUDPanel.SetActive(false);
        pausePanel.SetActive(false);
        if (exitPanel != null) {
            exitPanel.SetActive(false);
        }
        if (continueExpPanel != null)
        {
            continueExpPanel.SetActive(false);
        }

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
        //Debug.Log("DrawPlayerStats()");
        //Debug.Log(playerData.lifes);
        lifesText.text = "x" + playerData.lifes.ToString();
        currentLevel.text = "W-" + playerData.currentLevel;
        itemsText.text = playerData.itmes.ToString();
        energyText.text = playerData.energyPoints.ToString();
        sliderLifePoints.value = playerData.lifePoints;
        sliderEnergyPoints.value = playerData.energyPoints;
        if (playerData.canPunch)
        {
            punchIcon.SetActive(true);
            energyIcon.SetActive(false);

        }
        else {
            punchIcon.SetActive(false);
            energyIcon.SetActive(true);
        }

    }

    public void ShowExitPanel() {
        if (this.exitPanel != null) {
            CleanPanel();
            this.exitPanel.SetActive(true);
        }
    }

    public void ShowContinueExpPanel()
    {
        if (this.continueExpPanel != null)
        {
            CleanPanel();
            this.continueExpPanel.SetActive(true);
        }
    }
}
