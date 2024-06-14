using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
    void Start()
    {
        // Đặt lại điểm khi bắt đầu trò chơi mới
        PlayerPrefs.SetInt("PlayerScore", 0);
    }
}