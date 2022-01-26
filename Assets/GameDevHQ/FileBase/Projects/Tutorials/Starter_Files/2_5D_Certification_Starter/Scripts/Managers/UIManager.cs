using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("UIManager is null!");

            return _instance;

        }
    }

    [SerializeField]
    private TMP_Text _coins;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateCoinText(int coins)
    {
        _coins.SetText($"Coins: {coins}");
    }

}
