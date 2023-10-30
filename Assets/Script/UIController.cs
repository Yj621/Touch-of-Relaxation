using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    CameraSwitcher cameraSwitcher;
    PlayerData playerData;
    WorkerScript workerScript;

    [Header ("메뉴 패널")]
    public GameObject panelConstruction;
    public GameObject panelAbility;
    public GameObject panelTool;
    public GameObject panelStore;
    private GameObject menu;
    private GameObject menuImg;
    private GameObject btnMap;
    private GameObject btnBook;
    private float moveDistance = -918f;
    private float animationSpeed = 2000f;
    private Vector3 targetPosition;
    private Quaternion startRotation;
    public bool isMenuDown = false;
    public Text energyText;
    public GameObject mapWindow;
    public GameObject bookWindow;
    
    [Header("골드, 다이아 변환 패널")]
    private GameObject popUpPanel; // PopUp 패널
    private GameObject changeWindowPanel;
    public Text windowTitleText; // Window Title Text UI 요소
    public bool isGoldButtonClicked = false; //골드, 다이아버튼 어떤걸 클릭했는지 확인하는 변수
    public bool isPanelOn = false;

    [Header("slider 패널")]
    public Slider slider;
    public Button buttonDecrease;
    public Button buttonIncrease;
    public Text textAmountOfGoods;
    public Text goldText; // coin_text UI를 연결해줄 변수
    public Text diaText;
    public Text garbage;
    public GameObject warningWindow;
    public GameObject windowTitle;
    
    [Header("도감 패널")]
    public Button[] buttons; // 버튼 배열
    private List<SetItem> sets = new List<SetItem>(); // 세트 리스트
    private bool isZero = false;
    private bool isFirst = false;

    [Header("진척도 패널")]
    public Image progressGage;


    private void Awake()
    {
    // 모든 버튼을 비활성화합니다.
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        // 세트 초기화
        for (int i = 0; i < buttons.Length; i++)
        {
            SetItem set = new SetItem();
            set.button = buttons[i];
            set.buttonClicked = false;
            set.index = i;
            sets.Add(set);
        }
    }

    private void Start()
    {
        playerData = DataManager.instance.player;

        menu = GameObject.Find("Menu");
        targetPosition = menu.transform.localPosition;
        menuImg = GameObject.Find("Img_Menu");
        startRotation = menuImg.transform.rotation;

        popUpPanel = GameObject.Find("PopUp");
        changeWindowPanel = GameObject.Find("Change_Window");
        btnMap = GameObject.Find("Button_Map");
        btnBook = GameObject.Find("Button_Book");
        workerScript = FindAnyObjectByType<WorkerScript>();
        cameraSwitcher = FindAnyObjectByType<CameraSwitcher>();

        slider.value = 0;

        mapWindow.SetActive(false);
        bookWindow.SetActive(false);
        popUpPanel.SetActive(false);
        changeWindowPanel.SetActive(false);
        warningWindow.SetActive(false);
        
    }

    private void Update()
    {
        // Menu 오브젝트를 목표 위치로 천천히 내리기
        menu.transform.localPosition = Vector3.MoveTowards(menu.transform.localPosition, targetPosition, animationSpeed * Time.deltaTime);
        progressGage.fillAmount = (float)DataManager.instance.player.MainGage() / 100.0f;

        garbage.text = playerData.MyUnitToString("Garbage").ToString();
        energyText.text = playerData.MyUnitToString("Energy").ToString();
        goldText.text = playerData.MyUnitToString("Gold").ToString();
        diaText.text = playerData.MyUnitToString("Diamond").ToString();


        // workerScript.special이 true이고 IndexZero 함수가 아직 실행되지 않았다면 실행합니다.
        if (workerScript.special1 == true && !isZero)
        {
            Debug.Log("0");
            IndexZero();
            isZero = true; // IndexZero 함수를 실행했으므로 플래그를 true로 설정합니다.
        }
        // workerScript.special이 true이고 IndexZero 함수가 아직 실행되지 않았다면 실행합니다.
        if (workerScript.special2 == true && !isFirst)
        {
            Debug.Log("0");
            IndexFirst();
            isFirst = true; // IndexZero 함수를 실행했으므로 플래그를 true로 설정합니다.
        }

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
    public void OnBtnForest()
    {
        SceneManager.LoadScene("StageScene"); 
        cameraSwitcher.ForestCam();
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
    
    public void OnBtnBook()
    {
        bookWindow.SetActive(true);
    }
    
    public void OnBtnBookClose()
    {
        bookWindow.SetActive(false);
    }


    public void MapChange()
    {
        SceneManager.LoadScene("StageScene");
    }

    //골드 버튼 클릭
    public void GoldButtonClick()
    {
        isGoldButtonClicked = true;
        SetCurrentEnergy(int.Parse(energyText.text)); // SetCurrentEnergy 메서드를 호출할 때도 파라미터로 energy 값을 전달


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
        SetCurrentEnergy(int.Parse(energyText.text)); // SetCurrentEnergy 메서드를 호출할 때도 파라미터로 energy 값을 전달

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
        slider.value -= 1;
        UpdateTextAmountOfGoods();
    }

    public void IncreaseSliderValue()
    {
        // Slider의 현재 값에서 10 증가
        slider.value += 1;
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
        //slider.maxValue = playerData.Energy(); // Slider의 maxValue를 현재 energy 값으로 설정
        slider.value = 0; // Slider의 값을 초기화
        UpdateTextAmountOfGoods();
    }

    
    // 확인 버튼 클릭
    public void ConfirmButtonClick()
    {
        int amount = (int)slider.value; // Slider의 값을 정수로 변환
        if (isGoldButtonClicked)
        {
            playerData.SetUnitValue("Energy", -amount);
            playerData.SetUnitValue("Gold", amount);
        }
        else
        {
            playerData.SetUnitValue("Energy", -amount);
            playerData.SetUnitValue("Diamond", amount);
        }
        slider.value = 0;
        UpdateTextAmountOfGoods();
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

    // 도감 다이아 얻는 버튼 (버튼 인덱스를 받아 처리)
    public void OnBtnGetDia(int index)
    {
        if (!sets[index].buttonClicked)
        {
            playerData.SetUnitValue("Diamond", 1000);
            sets[index].buttonClicked = true;
            sets[index].button.interactable = false;
        }
    }

    // 다른 버튼 동작 메서드들 (각 버튼을 별도의 메서드로 처리)
    public void OnBtnGetDia0()
    {
        OnBtnGetDia(0);
    }

    public void OnBtnGetDia1()
    {
        OnBtnGetDia(1);
    }

    public void OnBtnGetDia2()
    {
        OnBtnGetDia(2);
    }

    public void OnBtnGetDia3()
    {
        OnBtnGetDia(3);
    }
    public void OnBtnGetDia4()
    {
        OnBtnGetDia(4);
    }
    public void IndexZero()
    {
        sets[0].button.interactable = true;
    }
    public void IndexFirst()
    {
        sets[1].button.interactable = true;
    }
    // SetItem 클래스 정의
    private class SetItem
    {
        public Button button;
        public bool buttonClicked=false;
        public int index;
    }
}


