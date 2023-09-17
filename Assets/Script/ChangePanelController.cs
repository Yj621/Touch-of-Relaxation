using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanelController : MonoBehaviour
{
    private GameObject[] panels; // 패널들 배열
    public bool panelOn = false;

    private void Start()
    {
        panels = new GameObject[]
        {
            GameObject.Find("PopUp"),
            GameObject.Find("Gold_Window"),
            GameObject.Find("Dia_Window")
        };

        // 초기에는 모든 패널을 비활성화
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            panelOn = false;
        }
        else
        {
            panel.SetActive(true);
            panelOn = true;
        }
    }

    public void GoldButtonClick()
    {
        TogglePanel(panels[1]); // Gold_Window 패널 토글
        PopUpPanelFalse();
        panelOn = true;
    }

    public void DiaButtonClick()
    {
        TogglePanel(panels[2]); // Dia_Window 패널 토글
        Debug.Log("Dias");
        PopUpPanelFalse();
        panelOn = true;
    }

    public void DiaWindowClose()
    {
        panels[2].SetActive(false); // Dia_Window 패널 닫기
        panelOn = false;
    }

    public void GoldWindowClose()
    {
        panels[1].SetActive(false); // Gold_Window 패널 닫기
        panelOn = false;
    }

    public void PopUpPanelFalse()
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false); // PopUp 패널 닫기
            panelOn = false;
            Debug.Log("panelOn: " +panelOn);
        }
    }

    public void PopUpPanelTrue()
    {
        panels[0].SetActive(true); // PopUp 패널 열기
        panelOn = true;
    }
}
