using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ChangePanelController : MonoBehaviour
{
    private GameObject popUpPanel; // PopUp 패널
    private GameObject goldWindowPanel; // Gold_Window 패널
    private GameObject diaWindowPanel; // Dia_Window 패널
    public bool PanelOn = false;

    private void Start()
    {
        // 게임 시작 시 패널들을 이름을 사용하여 찾아서 비활성화
        popUpPanel = GameObject.Find("PopUp");
        goldWindowPanel = GameObject.Find("Gold_Window");
        diaWindowPanel = GameObject.Find("Dia_Window");

        // 초기에는 모든 패널을 비활성화
        popUpPanel.SetActive(false);
        goldWindowPanel.SetActive(false);
        diaWindowPanel.SetActive(false);
    }

    public void GoldButtonClick()
    {
        if (popUpPanel.activeSelf)
        {
            PopUpPanelFalse();
        }
        goldWindowPanel.SetActive(true);
        PanelOn = true;
    }

    public void DiaButtonClick()
    {
        if (popUpPanel.activeSelf)
        {
            PopUpPanelFalse();
            Debug.Log("Dias");
        }
        diaWindowPanel.SetActive(true);
        PanelOn = true;
    }

    public void PopUpPanelFalse()
    {
        if (popUpPanel.activeSelf)
        {
            popUpPanel.SetActive(false);
            PanelOn = false;
            Debug.Log(PanelOn);
        }
    }

    public void PopUpPanelTrue()
    {
        popUpPanel.SetActive(true);
        PanelOn = true;
    }
    public void PopUpPanelClose()
    {
        popUpPanel.SetActive(false);
    }
}
