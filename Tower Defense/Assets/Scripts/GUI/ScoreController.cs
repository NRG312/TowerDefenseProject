using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyTxt;

    private void Start()
    {
        moneyTxt.text = "$0";
    }

    public void ManageMoneyUI(float money)
    {
        moneyTxt.text = "$" + money;
    }
}
