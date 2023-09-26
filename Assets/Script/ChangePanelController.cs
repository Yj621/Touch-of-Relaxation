using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class ChangePanelController : MonoBehaviour
{
    private GameObject popUpPanel; // PopUp 패널
    private GameObject changeWindowPanel;
    public bool PanelOn = false;
    public Text windowTitleText; // Window Title Text UI 요소

    private void Start()
    {
        popUpPanel = GameObject.Find("PopUp");
        changeWindowPanel = GameObject.Find("Change_Window");

        // 초기에는 모든 패널을 비활성화
        popUpPanel.SetActive(false);
        changeWindowPanel.SetActive(false);
    }

    //골드 버튼 클릭
    public void GoldButtonClick()
    {
        if (popUpPanel.activeSelf)
        {
            PopUpPanelFalse();
        }
        changeWindowPanel.SetActive(true);
        PanelOn = true;

        // Window Title Text 변경
        if (windowTitleText != null)
        {
            windowTitleText.text = "골드량 선택";
        }
    }

    //다이아 버튼 클릭
    public void DiaButtonClick()
    {
        if (popUpPanel.activeSelf)
        {
            PopUpPanelFalse();
            Debug.Log("Dias");
        }
        changeWindowPanel.SetActive(true);
        PanelOn = true;

        // Window Title Text 변경
        if (windowTitleText != null)
        {
            windowTitleText.text = "다이아몬드량 선택";
        }
    }

    public void ChangeWindowClose()
    {
        changeWindowPanel.SetActive(false);
        PanelOn = false;
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
}
