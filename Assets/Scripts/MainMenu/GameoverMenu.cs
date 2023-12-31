﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    public string nameGameplayScene;
    public float remainingGameOver;

    bool activeLevelUp;

    public void GameOver()
    {
        activeLevelUp = levelUpPanel.GetComponent<ActivePanel>().isSetActive;
        if (activeLevelUp)
        {
            // Đang bật Panel Nâng cấp thì tắt nó rồi game over
            levelUpPanel.SetActive(false);
        }
        gameOverPanel.SetActive(true);

        StartCoroutine(RemainingGameOver());
    }

    public void RetryGame()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(nameGameplayScene);
        SoundManager.Instance.PlayMusic(nameGameplayScene);

        Time.timeScale = 1.0f;
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.PlayMusic("MainMenu");

        Time.timeScale = 1.0f;
    }


    // Đếm ngược vài giây để dừng hẳn game lại
    IEnumerator RemainingGameOver()
    {
        yield return new WaitForSeconds(remainingGameOver);
        // Sau khi đã đếm ngược vài giây thì bắt đầu dừng game tránh game bị tràn dữ liệu
        Time.timeScale = 0f;
    }
}
