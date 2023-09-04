using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public GameObject panelConstruction; 
    public GameObject panelAbility;
    public GameObject panelTool;
    public GameObject panelStore;

    // 버튼 클릭 시 호출되는 함수
    public void OnBtnConstruction()
    {
        if (panelConstruction != null)
        {
            panelConstruction.SetActive(true);
            panelAbility.SetActive(false);
            panelTool.SetActive(false);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnAbility()
    {
        if (panelAbility != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(true);
            panelTool.SetActive(false);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnTool()
    {
        if (panelTool != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(false);
            panelTool.SetActive(true);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnStore()
    {
        if (panelStore != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(false);
            panelTool.SetActive(false);
            panelStore.SetActive(true);
        }
    }
}
