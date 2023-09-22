using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //메뉴 패널
    public GameObject panelConstruction;
    public GameObject panelAbility;
    public GameObject panelTool;
    public GameObject panelStore;
    private GameObject menu;
    private GameObject menuImg;
    private float moveDistance = -918f;
    private float animationSpeed = 2000f;
    private Vector3 targetPosition;
    private Quaternion startRotation;
    public bool isMenuDown = false;
    public Text textEnergy;
    private SliderValueController sliderValueController;


    //골드 다이어 패널
    private GameObject popUpPanel; // PopUp 패널
    private GameObject changeWindowPanel;
    public Text windowTitleText; // Window Title Text UI 요소

    public bool isPanelOn = false;

    private void Start()
    {
        sliderValueController = FindObjectOfType<SliderValueController>();
        menu = GameObject.Find("Menu");
        targetPosition = menu.transform.localPosition;
        menuImg = GameObject.Find("Img_Menu");
        startRotation = menuImg.transform.rotation;

        popUpPanel = GameObject.Find("PopUp");
        changeWindowPanel = GameObject.Find("Change_Window");

        // 초기에는 모든 패널을 비활성화
        popUpPanel.SetActive(false);
        changeWindowPanel.SetActive(false);
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


    //골드 버튼 클릭
    public void GoldButtonClick()
    {
        sliderValueController.currentEnergy = int.Parse(textEnergy.text); // energy UI Text 값 가져오기
        sliderValueController.SetCurrentEnergy(int.Parse(textEnergy.text)); // SetCurrentEnergy 메서드를 호출할 때도 파라미터로 energy 값을 전달


        if (popUpPanel.activeSelf)
        {
            PopUpPanelFalse();
        }
        changeWindowPanel.SetActive(true);
        isPanelOn = true;

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
        isPanelOn = true;

        windowTitleText.text = "다이아몬드량 선택";
        // Window Title Text 변경
        if (windowTitleText != null)
        {
            Debug.Log("Dia");
            windowTitleText.text = "다이아몬드량 선택";
        }
    }

    public void ChangeWindowClose()
    {
        changeWindowPanel.SetActive(false);
        isPanelOn = false;
    }

    public void PopUpPanelFalse()
    {
        if (popUpPanel.activeSelf)
        {
            popUpPanel.SetActive(false);
            Invoke("ChagePanelStateFalse", 0.1f);
            isPanelOn = false;
        }
    }

    public void PopUpPanelTrue()
    {
        popUpPanel.SetActive(true);
        isPanelOn = true;
    }

    private void ChagePanelStateFalse()
    {
        isPanelOn = false;
    }
    // public void UpdateCoinText(int amount)
    // {
    //     coin_text.text = (int.Parse(coin_text.text) + amount).ToString();
    // }

}
