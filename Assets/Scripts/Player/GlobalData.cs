using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Singleton;

    [HideInInspector]
    public int money = 0;
    public int startMoney = 10;

    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}