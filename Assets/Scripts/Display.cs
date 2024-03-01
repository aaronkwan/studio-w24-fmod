using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject gameOverMenu;
    [SerializeField] private TMP_Text gameOverMessage;
    [SerializeField] private Image overlayLife;

    public void DisplayPause(bool pause)
    {
        pauseMenu.SetActive(pause);
    }
    public void DisplayGameOver(bool gameOver)
    {
        gameOverMenu.SetActive(gameOver);
        gameOverMessage.text = "Time survived: " + Manager.Instance.timer;
    }
    void FixedUpdate()
    {
        overlayLife.fillAmount = (Manager.Instance.life / 30f);
    }
}
