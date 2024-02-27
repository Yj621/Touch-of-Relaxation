using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructerController : MonoBehaviour
{
    const int BUILDINGCOUNT = 3;

    PlayerData                  playerData;
    NoticeUI                    _notice;
    UIController                uIController;
    public List<GameObject>     structerGameObjectList;     // 건물 건설용도로 선언한 게임 오브젝트 리스트
    private List<Structer>      structerScriptList;         // 건물 3개의 정보만 관리하기 윈한 스크립트 변수
    int[]                       needGold;
    int[]                       needGarbage;

    private int                 currentBuildingIndex = 0;


    void Start()
    {
        playerData = DataManager.instance.player;
        uIController = FindAnyObjectByType<UIController>();
        _notice = FindAnyObjectByType<NoticeUI>();
        structerScriptList = new List<Structer>();

        foreach (GameObject building in structerGameObjectList)
        {
            building.SetActive(false);
        }

        for (int i = 0; i < (structerGameObjectList.Count / BUILDINGCOUNT); i++)
        {
            structerScriptList.Add(structerGameObjectList[i].GetComponent<Structer>());
        }
    }
    private void Update()
    {
    }
    public void OnBtnBuild()
    {
        // 버튼 스크립트에서 넘버를 불러옴
        int clickNum = EventSystem.current.currentSelectedGameObject.GetComponent<ButtonNum>().GetButtonNuum();
        Structer nowStructer = structerScriptList[clickNum % BUILDINGCOUNT].GetComponent<Structer>();   //불러온 넘버에 맞는 structer 스크립트 호출
        needGold = nowStructer.GetNeedGold();   //해당 건물의 소요 골드를 저장할 임시 변수
        needGarbage = nowStructer.GetNeedGarbage();  //해당 건물의 소요 쓰레기를 저장할 임시 변수
        int needGoldIndex = nowStructer.UintCurrentIndex(needGold);     //소요 골드의 자릿수를 정해주는 index변수
        int needGarbageIndex = nowStructer.UintCurrentIndex(needGarbage); // 소요 쓰레기를 자릿수를 정해주는 index변수

        if (IsHaveUnit(needGoldIndex, needGarbageIndex))
        {
            if (structerGameObjectList[clickNum].activeInHierarchy == true)
            {
                _notice.SUB("레벨 업!");
            }
            else
            {
                _notice.SUB("건설!");
            }
            // 소지 재화 감소
            playerData.SetUnitValue((int)Unit.GOLD, needGold);
            playerData.SetUnitValue((int)Unit.GARBAGE, needGarbage);

            //건물 레벨 업
            nowStructer.LevelUP();

            // 건물 활성화
            ActivateBuilding(nowStructer.GetLevel(), clickNum);
        }
        else
        {
            _notice.SUB("재화가 부족합니다.");
        }

    }

    private void ActivateBuilding(int currentLevel, int clickNum)
    {
            switch (currentLevel)
            {
                case 1:
                    ActivateBuilding(0 + clickNum);
                    break;
                case 200:
                    ActivateBuilding(3+ clickNum);
                    break;
                case 500:
                    ActivateBuilding(6 + clickNum);
                    break;
            }
        
    }

    public bool IsHaveUnit(int _currentGoldIndex, int _currentGarbageIndex)
    {
        int[] hasGold = playerData.Gold();
        int[] hasGarbage = playerData.Garbage();

        if (hasGold[_currentGoldIndex] >= needGold[_currentGoldIndex] || hasGold[_currentGoldIndex + 1] * 10000 >= needGold[_currentGoldIndex])
        {
            if (hasGarbage[_currentGarbageIndex] >= needGarbage[_currentGarbageIndex] || hasGarbage[_currentGarbageIndex + 1] * 10000 >= needGarbage[_currentGarbageIndex])
            {
                return true;
            }
        }

        return false;
    }

    private void ActivateBuilding(int index)
    {
        if (currentBuildingIndex <= index && index < structerGameObjectList.Count)
        {
            structerGameObjectList[index].SetActive(true);
            currentBuildingIndex = index + 1;
        }
    }

}
