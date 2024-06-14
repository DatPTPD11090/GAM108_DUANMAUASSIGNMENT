using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score Instance;
    public TMP_Text coinText;
    public int currentCoins = 0;

    private void Awake()
    {
        // ??m b?o ch? c� m?t instance c?a Score
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // L?y ?i?m ?� l?u t? PlayerPrefs
        currentCoins = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateCoinText();
    }

    // H�m n�y ???c g?i ?? t?ng ?i?m
    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        UpdateCoinText();
        SaveScore();  // L?u ?i?m khi t?ng ?i?m
    }

    // H�m c?p nh?t text hi?n th? ?i?m
    private void UpdateCoinText()
    {
        coinText.text = "Score: " + currentCoins.ToString();
    }

    // H�m l?u ?i?m v�o PlayerPrefs
    public void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", currentCoins);
    }
}
