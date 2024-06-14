using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextSceneName;

    // Hàm x? lý va ch?m
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ki?m tra n?u ??i t??ng va ch?m là ng??i ch?i
        if (other.CompareTag("Player"))
        {
            // L?u ?i?m hi?n t?i
            SavePlayerScore();

            // Chuy?n sang màn ch?i m?i
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // Hàm l?u ?i?m c?a ng??i ch?i
    private void SavePlayerScore()
    {
        int currentScore = Score.Instance.currentCoins;
        PlayerPrefs.SetInt("PlayerScore", currentScore);
    }
}
