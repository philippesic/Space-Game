using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMenu : MonoBehaviour
{

    private GameObject text;

    void Start()
    {
        text = GameObject.Find("MoneyText");
    }

    void Update()
    {
        text.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalData.Singleton.money.ToString();
    }
}