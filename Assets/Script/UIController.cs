using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

enum Unit
{
    GOLD=1,
    GARBAGE,
    ENERGY,
    DIAMOND
};

public class UIController : MonoBehaviour
{
    // CameraSwitcher cameraSwitcher;
    PlayerData playerData;
    WorkerScript workerScript;
    Build build;
    NoticeUI _notice;

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


    [Header ("건설 버튼")]
    public Text lvTextBuild;
    public Text moneyTextBuild;
    public Text garbageTextBuild;
    public int b_level = 0;
    private int b_garbage = 50;

    private int b_money = 50;
    
    [Header ("골드 변환 패널")]
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

        changeWindowPanel = GameObject.Find("Change_Window");
        btnMap = GameObject.Find("Button_Map");
        btnBook = GameObject.Find("Button_Book");
        workerScript = FindAnyObjectByType<WorkerScript>();
        build = FindAnyObjectByType<Build>();
        _notice = FindAnyObjectByType<NoticeUI>();
        // cameraSwitcher = FindAnyObjectByType<CameraSwitcher>();


        slider.value = 0;

        mapWindow.SetActive(false);
        changeWindowPanel.SetActive(false);
        warningWindow.SetActive(false);
    }
    // void OnEnable()
    // {
    // 	  // 씬 매니저의 sceneLoaded에 체인을 건다.
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("OnSceneLoaded: " + scene.name);
    //     Debug.Log(mode);
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    private void Update()
    {
        // Menu 오브젝트를 목표 위치로 천천히 내리기
        menu.transform.localPosition = Vector3.MoveTowards(menu.transform.localPosition, targetPosition, animationSpeed * Time.deltaTime);
        progressGage.fillAmount = (float)DataManager.instance.player.ConfirmGage("Main");

        garbage.text = playerData.MyUnitToString((int)Unit.GARBAGE).ToString();
        energyText.text = playerData.MyUnitToString((int)Unit.ENERGY).ToString();
        goldText.text = playerData.MyUnitToString((int)Unit.GOLD).ToString();
        diaText.text = playerData.MyUnitToString((int)Unit.DIAMOND).ToString();


        // workerScript.special이 true이고 IndexZero 함수가 아직 실행되지 않았다면 실행합니다.
        if (workerScript.special1 == true && !isZero)
        {
            IndexZero();
            isZero = true; // IndexZero 함수를 실행했으므로 플래그를 true로 설정합니다.
        }
        // workerScript.special이 true이고 IndexZero 함수가 아직 실행되지 않았다면 실행합니다.
        if (workerScript.special2 == true && !isFirst)
        {
            IndexFirst();
            isFirst = true; // IndexZero 함수를 실행했으므로 플래그를 true로 설정합니다.
        }

        playerData.ConvertUnit((int)Unit.GOLD);
        playerData.ConvertUnit((int)Unit.ENERGY);
        playerData.ConvertUnit((int)Unit.GARBAGE);
        playerData.ConvertUnit((int)Unit.DIAMOND);
        UpdateTextAmountOfGoods();

        Debug.Log("초기 돈 : " + playerData.UnitValue((int)Unit.GOLD, 0));
        Debug.Log("초기 쓰레기 : " + playerData.UnitValue((int)Unit.GARBAGE, 0));
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

    //맵 버튼
    public void OnBtnForest()
    {
        SceneManager.LoadScene("ForestStage");
        mapWindow.SetActive(false);
    }

    public void OnBtnCity()
    {
        SceneManager.LoadScene("CtiyStage");
        mapWindow.SetActive(false);
    }
    public void OnBtnCountry()
    {
        SceneManager.LoadScene("CountrySideStage");
        mapWindow.SetActive(false);
    }
    public void OnBtnSea()
    {
        SceneManager.LoadScene("SeaStage");
        mapWindow.SetActive(false);
    }
    public void OnBtnVillage()
    {
        SceneManager.LoadScene("VillageStage");
        mapWindow.SetActive(false);
    }

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

    public void OnBtnMap()
    {
        mapWindow.SetActive(true);
    }
    public void OnBtnMapClose()
    {
        mapWindow.SetActive(false);
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
        else if(playerData.UnitValue((int)Unit.ENERGY, unitIndex) < amount)
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

    //물어보기
    public void OnBtnBuild()
    {
        if (playerData.UnitValue((int)Unit.GOLD, 0) >= b_money && playerData.UnitValue((int)Unit.GARBAGE, 0) >= b_garbage )
        {
            // 돈 소비        
            playerData.SetUnitValue((int)Unit.GOLD, -b_money);
            //쓰레기 소비
            playerData.SetUnitValue((int)Unit.GARBAGE, -b_garbage);
            
            b_level++;
            lvTextBuild.text = "Lv." + b_level.ToString("D3");
            build.build();
            


            // 쓰여있는 돈 증가
            b_money += 50;
            // 쓰여있는 쓰레기 증가
            b_garbage += 50;
            moneyTextBuild.text = b_money.ToString();
            garbageTextBuild.text = b_garbage.ToString();

            _notice.SUB("건설!");
            Debug.Log("b_level : " + b_level);
            Debug.Log("------돈 : " + playerData.UnitValue((int)Unit.GOLD, 0));
            Debug.Log("------쓰레기 : " + playerData.UnitValue((int)Unit.GARBAGE, 0));


        }
        else if(playerData.UnitValue((int)Unit.GOLD, 0) <= b_money)
        {
            // 돈이 부족한 경우에 대한 처리
            _notice.SUB("돈이 부족합니다!");
            Debug.Log("------돈 : " + playerData.UnitValue((int)Unit.GOLD, 0));

        }
        else if(playerData.UnitValue((int)Unit.GARBAGE, 0) <= b_garbage)
        {
            _notice.SUB("쓰레기가 부족합니다!");
            Debug.Log("------쓰레기 : " + playerData.UnitValue((int)Unit.GARBAGE, 0));

        }
    }
  //  테스트 버튼
    public void test()
    {
        lvTextBuild.text = "Lv." + b_level.ToString("D3");
        b_level+=99;
    }
}


