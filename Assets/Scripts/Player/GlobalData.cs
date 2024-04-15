using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Singleton;

    [HideInInspector]
    public int money = 0;
    public int startMoney = 100;

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }
}