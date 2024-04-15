using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject menu;
    public GameObject controls;
    public GameObject abortConfirm;
    private List<GameObject> submenus = new List<GameObject>();
    [SerializeField] private InputActionReference menuButton;
    [SerializeField] GameObject back;
    private bool menuButtonState = false;
    void Start()
    {
        menu = GameObject.Find("Pause");
        menu.SetActive(false);
        controls.SetActive(false);
        abortConfirm.SetActive(false);
        back.SetActive(false);

        submenus.Add(controls);
        submenus.Add(abortConfirm);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (menuButton.action.IsPressed() && !menuButtonState))
        {
            ToggleMenu();
        }
        menuButtonState = menuButton.action.IsPressed();
    }

    public void ToggleMenu()
    {
        foreach (GameObject sub in submenus)
        {
            sub.SetActive(false);
        }
        menu.SetActive(!menu.activeSelf);
        back.SetActive(menu.activeSelf);
    }


    public void ToggleControls()
    {
        controls.SetActive(!controls.activeSelf);
        menu.SetActive(!controls.activeSelf);
        back.SetActive(true);
    }

    public void ToggleAbort()
    {
        abortConfirm.SetActive(!abortConfirm.activeSelf);
        menu.SetActive(!abortConfirm);
        back.SetActive(true);
    }
    public void Abort()
    {

        if (TaskMannager.Singleton.tasks.Count > 0)
        {
            GlobalData.Singleton.money -= GlobalData.Singleton.startMoney;
            if (GlobalData.Singleton.money < 0)
            {
                GlobalData.Singleton.money = 0;
            }
        }

        SceneDataController.Singleton.Leave();
    }
}