using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

enum Unit
{
    GOLD = 1,
    GARBAGE,
    ENERGY,
    DIAMOND
};

public class UIController : MonoBehaviour
{
    // CameraSwitcher cameraSwitcher;
    PlayerData playerData;
    WorkerScript workerScript;
    StructerController structerController;
    NoticeUI _notice;

    [Header("메뉴 패널")]
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
    public GameObject bookWindow;

    [Header("골드 변환 패널")]
    private GameObject changeWindowPanel;
    public Text windowTitleText; // Window Title Text UI 요소
    public bool isGoldButtonClicked = false;
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
    public int unitIndex;

    [Header("도감 패널")]
    public Button[] buttons; // 버튼 배열
    private List<SetItem> sets = new List<SetItem>(); // 세트 리스트
    private bool[] isActive = new bool[12];
    public Sprite change_img;

    [Header("진척도 패널")]
    public Image progressGage;

    public List<GameObject> structerList;

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

        changeWindowPanel = GameObject.Find("Change_Window");
        btnMap = GameObject.Find("Button_Map");
        btnBook = GameObject.Find("Button_Book");
        workerScript = FindAnyObjectByType<WorkerScript>();
        structerController = FindAnyObjectByType<StructerController>();
        _notice = FindAnyObjectByType<NoticeUI>();
        // cameraSwitcher = FindAnyObjectByType<CameraSwitcher>();

        //structerController.UIInit();

        slider.value = 0;

        changeWindowPanel.SetActive(false);
        warningWindow.SetActive(false);
    }

    private void Update()
    {
        // Menu 오브젝트를 목표 위치로 천천히 내리기
        menu.transform.localPosition = Vector3.MoveTowards(menu.transform.localPosition, targetPosition, animationSpeed * Time.deltaTime);
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("MAIN");
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("FOREST");
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("CITY");
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("COUNTRY");
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("SEA");

        garbage.text = playerData.MyUnitToString((int)Unit.GARBAGE).ToString();
        energyText.text = playerData.MyUnitToString((int)Unit.ENERGY).ToString();
        goldText.text = playerData.MyUnitToString((int)Unit.GOLD).ToString();
        diaText.text = playerData.MyUnitToString((int)Unit.DIAMOND).ToString();


        //다이아 도감 확률 패널
        for (int i = 0; i < workerScript.specialArry.Length; i++)
        {
            if (workerScript.specialArry[i] && !isActive[i])
            {
                IndexBtnActive(i);
                isActive[i] = true;
            }
        }


        playerData.ConvertUnit((int)Unit.GOLD);
        playerData.ConvertUnit((int)Unit.ENERGY);
        playerData.ConvertUnit((int)Unit.GARBAGE);
        playerData.ConvertUnit((int)Unit.DIAMOND);
        UpdateTextAmountOfGoods();

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

    #region 버튼 관련 함수들
    //메뉴버튼
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

    public void OnBtnHome()
    {
        // 현재 로드된 씬의 이름을 가져옵니다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 만약 현재 씬의 이름이 "HomeStage"이면 동작X
        if (currentSceneName != "HomeStage")
        {
            // "HomeStage" 씬이 로드되어 있지 않은 경우에만 로드
            if (!SceneManager.GetSceneByName("HomeStage").isLoaded)
            {
                SceneManager.LoadScene("HomeStage");
            }
            else
            {
                Debug.Log("이미 홈맵입니다.");
            }
        }
        else
        {
            Debug.Log("이미 홈맵입니다.");
        }
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


    #endregion 버튼 관련 함수들



    #region 재화 변경 관련

    public void ChangeWindowClose()
    {
        changeWindowPanel.SetActive(false);
        Invoke("ChagePanelStateFalse", 0.1f);
        textAmountOfGoods.text = "0";
        slider.value = 0;
        unitIndex = 0;
    }

    public void ChangeWindowFalse()
    {
        if (changeWindowPanel.activeSelf)
        {
            changeWindowPanel.SetActive(false);
            Invoke("ChagePanelStateFalse", 0.1f);
        }
    }

    public void ChangeWindowTrue()
    {
        if (!changeWindowPanel.activeSelf && !isPanelOn)
        {
            changeWindowPanel.SetActive(true);
            Debug.Log("변환패널 켜짐");
            slider.maxValue = playerData.UnitValue((int)Unit.ENERGY, 0);
            isPanelOn = true;
        }
    }

    public void ChagePanelStateFalse()
    {
        isPanelOn = false;
    }

    public void IncreaseSliderValue()
    {
        if (playerData.UnitValue((int)Unit.ENERGY, unitIndex + 1) > 0 && unitIndex < 26)
        {
            slider.value = 0;
            ++unitIndex;
            slider.maxValue = playerData.UnitValue((int)Unit.ENERGY, unitIndex);
        }
    }

    public void DecreaseSliderValue()
    {
        if (unitIndex > 0)
        {
            slider.value = 0;
            --unitIndex;
            slider.maxValue = playerData.UnitValue((int)Unit.ENERGY, unitIndex);
        }
    }

    public void UpdateTextAmountOfGoods()
    {
        int[] unit = { };

        char str = (char)(65 + unitIndex);

        // Slider의 값을 Text UI에 표시
        textAmountOfGoods.text = slider.value.ToString() + str.ToString();
    }

    // 확인 버튼 클릭
    public void ConfirmButtonClick()
    {
        int amount = (int)slider.value; // Slider의 값을 정수로 변환

        if (playerData.UnitValue((int)Unit.ENERGY, unitIndex) >= amount)
        {
            playerData.SetUnitValue((int)Unit.ENERGY, -amount, unitIndex);
            playerData.SetUnitValue((int)Unit.GOLD, amount, unitIndex);
            _notice.SUB("변환되었습니다.");
        }
        else if (playerData.UnitValue((int)Unit.ENERGY, unitIndex) < amount)
        {
            _notice.SUB("에너지가 부족합니다.");
        }
        slider.value = 0;
        unitIndex = 0;
        slider.maxValue = playerData.UnitValue((int)Unit.ENERGY, unitIndex);
    }

    //초기화 버튼 클릭
    public void CancleButtonClick()
    {
        textAmountOfGoods.text = "0";
        slider.value = 0;
        unitIndex = 0;
        slider.maxValue = playerData.UnitValue((int)Unit.ENERGY, unitIndex);
    }

    #endregion 재화 변경 관련


    public void WarningWindowClose()
    {
        warningWindow.SetActive(false);
    }

    // 도감 다이아 얻는 버튼 (버튼 인덱스를 받아 처리)
    public void OnBtnGetDia(int index)
    {
        if (!sets[index].buttonClicked)
        {
            playerData.SetUnitValue((int)Unit.DIAMOND, +1000);
            sets[index].buttonClicked = true;
            sets[index].button.interactable = false;
        }
    }

    public void IndexBtnActive(int index)
    {
        sets[index].button.interactable = true;
    }

    // SetItem 클래스 정의
    private class SetItem
    {
        public Button button;
        public bool buttonClicked = false;
        public int index;
    }
}


