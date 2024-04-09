using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject menu;
    public GameObject controls;
    public GameObject abortConfirm;
    [SerializeField] private GameObject player;
    private List<GameObject> submenus = new List<GameObject>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menu = GameObject.Find("Pause");
        menu.SetActive(false);
        controls.SetActive(false);
        abortConfirm.SetActive(false);

        submenus.Add(controls);
        submenus.Add(abortConfirm);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        foreach (GameObject sub in submenus)
        {
            sub.SetActive(false);
        }

        menu.SetActive(!menu.activeSelf);
        foreach (Transform child in player.GetComponentsInChildren<Transform>())
        {
            foreach (MonoBehaviour script in child.gameObject.GetComponents<MonoBehaviour>())
            {
                if ((script is ArmMovement) || (script is HandController) || (script is PlayerCam))
                {
                    script.enabled = (!menu.activeSelf && !controls.activeSelf);
                }

            }
        }

        if (menu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void ToggleControls()
    {
        controls.SetActive(!controls.activeSelf);
        menu.SetActive(!menu.activeSelf);
    }

    public void ToggleAbort()
    {
        menu.SetActive(!menu.activeSelf);
        abortConfirm.SetActive(!abortConfirm.activeSelf);
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