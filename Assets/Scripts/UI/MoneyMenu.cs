using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMenu : MonoBehaviour
{

    [SerializeField] private GameObject text;

    void Update()
    {
        if (GlobalData.Singleton == null)
        {
            text.GetComponent<TMPro.TextMeshProUGUI>().text = "GlobalData not saving?????";
        }
        else
        {
            text.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalData.Singleton.money.ToString();
        }
    }
}