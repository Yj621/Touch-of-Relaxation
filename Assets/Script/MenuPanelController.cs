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
    
    private GameObject menu;
    private GameObject menuImg;
    private float moveDistance = -918f;
    private float animationSpeed = 2000f;
    private bool isMenuDown = false;
    private Vector3 targetPosition;
    private Quaternion startRotation;

    private void Start()
    {
        menu = GameObject.Find("Menu");
        targetPosition = menu.transform.localPosition;
        menuImg = GameObject.Find("Img_Menu");
        startRotation = menuImg.transform.rotation;
    }

    private void Update()
    {
        // Menu 오브젝트를 목표 위치로 천천히 내리기
        menu.transform.localPosition = Vector3.MoveTowards(menu.transform.localPosition, targetPosition, animationSpeed * Time.deltaTime);
    }

    private void SetPanelActive(GameObject panel)
    {
        if (panel != null)
        {
            panelConstruction.SetActive(panel == panelConstruction);
            panelAbility.SetActive(panel == panelAbility);
            panelTool.SetActive(panel == panelTool);
            panelStore.SetActive(panel == panelStore);
        }
    }

    public void OnBtnConstruction()
    {
        SetPanelActive(panelConstruction);
    }

    public void OnBtnAbility()
    {
        SetPanelActive(panelAbility);
    }

    public void OnBtnTool()
    {
        SetPanelActive(panelTool);
    }

    public void OnBtnStore()
    {
        SetPanelActive(panelStore);
    }

    public void OnBtnMenuClose()
    {
        isMenuDown = !isMenuDown;
        targetPosition = isMenuDown ? targetPosition + Vector3.up * moveDistance : targetPosition - Vector3.up * moveDistance;
        menuImg.transform.rotation = isMenuDown ? Quaternion.Euler(0, 0, 90) : startRotation;
    }
}
