using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //메뉴 패널
    public GameObject panelConstruction;
    public GameObject panelAbility;
    public GameObject panelTool;
    public GameObject panelStore;
    private GameObject menu;
    private GameObject menuImg;
    private GameObject btnMap;
    private float moveDistance = -918f;
    private float animationSpeed = 2000f;
    private Vector3 targetPosition;
    private Quaternion startRotation;
    public bool isMenuDown = false;
    public Text textEnergy;
    public GameObject mapWindow;

    //골드 다이어 패널
    private GameObject popUpPanel; // PopUp 패널
    private GameObject changeWindowPanel;
    public Text windowTitleText; // Window Title Text UI 요소
    public bool isGoldButtonClicked = false; //골드, 다이아버튼 어떤걸 클릭했는지 확인하는 변수
    public bool isPanelOn = false;


    //slider 패널
    public Slider slider;
    public Button buttonDecrease;
    public Button buttonIncrease;
    public Text textAmountOfGoods;
    public Text coinText; // coin_text UI를 연결해줄 변수
    public Text diaText;
    private UIController uiController;
    public int currentEnergy = 0;
    public GameObject warningWindow;
    public GameObject windowTitle;

    private void Start()
    {
        menu = GameObject.Find("Menu");
        targetPosition = menu.transform.localPosition;
        menuImg = GameObject.Find("Img_Menu");
        startRotation = menuImg.transform.rotation;

        popUpPanel = GameObject.Find("PopUp");
        changeWindowPanel = GameObject.Find("Change_Window");
        btnMap = GameObject.Find("Button_Map");

        slider.value = 0;
        uiController = FindObjectOfType<UIController>();

        mapWindow.SetActive(false);
        popUpPanel.SetActive(false);
        changeWindowPanel.SetActive(false);
        warningWindow.SetActive(false);

        Debug.Log("currentEnergy: " + currentEnergy);
        
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

    public void OnBtnMap()
    {
        mapWindow.SetActive(true);
    }

    public void OnBtnMapClose()
    {
        mapWindow.SetActive(false);
    }

    public void MapChange()
    {
        SceneManager.LoadScene("StageScene");
    }

    //골드 버튼 클릭
    public void GoldButtonClick()
    {
        isGoldButtonClicked = true;
        currentEnergy = int.Parse(textEnergy.text); // energy UI Text 값 가져오기
        SetCurrentEnergy(int.Parse(textEnergy.text)); // SetCurrentEnergy 메서드를 호출할 때도 파라미터로 energy 값을 전달


        if (popUpPanel.activeSelf)
        {
            popUpPanel.SetActive(false);
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
        isGoldButtonClicked = false;
        currentEnergy = int.Parse(textEnergy.text); // energy UI Text 값 가져오기
        SetCurrentEnergy(int.Parse(textEnergy.text)); // SetCurrentEnergy 메서드를 호출할 때도 파라미터로 energy 값을 전달

        if (popUpPanel.activeSelf)
        {
            popUpPanel.SetActive(false);
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
        popUpPanel.SetActive(false);
        Invoke("ChagePanelStateFalse", 0.1f);
    }

    public void PopUpPanelFalse()
    {
        if (popUpPanel.activeSelf)
        {
            popUpPanel.SetActive(false);
            Invoke("ChagePanelStateFalse", 0.1f);
        }
    }

    public void PopUpPanelTrue()
    {
        if (!changeWindowPanel.activeSelf && !isPanelOn)
        {
            popUpPanel.SetActive(true);
            isPanelOn = true;
        }
    }

    public void ChagePanelStateFalse()
    {
        isPanelOn = false;
    }



    public void DecreaseSliderValue()
    {
        slider.value -= 10;
        UpdateTextAmountOfGoods();
    }

    public void IncreaseSliderValue()
    {
        // Slider의 현재 값에서 10 증가
        slider.value += 10;
        UpdateTextAmountOfGoods();
    }

    public void UpdateTextAmountOfGoods()
    {
        // Slider의 값을 Text UI에 표시
        textAmountOfGoods.text = slider.value.ToString();
    }

    // 현재 energy 값을 설정하는 메서드
    public void SetCurrentEnergy(int energyValue)
    {
        currentEnergy = energyValue;
        slider.maxValue = currentEnergy; // Slider의 maxValue를 현재 energy 값으로 설정
        slider.value = 0; // Slider의 값을 초기화
        UpdateTextAmountOfGoods();
    }

    //coin_text UI 값을 업데이트
    public void UpdateCoinText(int amount)
    {
        if (currentEnergy >= amount)
        {
            // 현재 energy가 충분하면 coin_text UI 값을 업데이트하고 energy 차감
            coinText.text = (int.Parse(coinText.text) + amount).ToString();
            currentEnergy -= amount;
            // energy UI Text를 업데이트
            uiController.textEnergy.text = currentEnergy.ToString();
            Debug.Log("골드로 교환 완료");
        }
        else
        {
            warningWindow.SetActive(true);
        }
    }

    //dia_text UI 값을 업데이트
    public void UpdateDiaText(int amount)
    {
        if (currentEnergy >= amount)
        {
            // 현재 energy가 충분하면 dia_text UI 값을 업데이트하고 energy 차감
            diaText.text = (int.Parse(diaText.text) + amount).ToString();
            currentEnergy -= amount;
            // energy UI Text를 업데이트
            uiController.textEnergy.text = currentEnergy.ToString();
            Debug.Log("다이아로로 교환 완료");
        }
        else
        {
            warningWindow.SetActive(true);
            Debug.Log("Energy가 부족합니다.");
        }
    }
    // 확인 버튼 클릭
    public void ConfirmButtonClick()
    {
        int amount = (int)slider.value; // Slider의 값을 정수로 변환
        if (uiController.isGoldButtonClicked == true)
        {
            UpdateCoinText(amount);
        }
        else
        {
            UpdateDiaText(amount);
        }
    }
    //초기화 버튼 클릭
    public void CancleButtonClick()
    {
        textAmountOfGoods.text = "0";

        // 'slider.value'를 0으로 초기화
        slider.value = 0;
    }

    public void WarningWindowClose()
    {
        warningWindow.SetActive(false);
    }
}
