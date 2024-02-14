using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructerController : MonoBehaviour
{
    PlayerData playerData;
    private UIController uIController;
    NoticeUI _notice;
    public List<GameObject> structerList;

    int[] needGold;
    int[] needGarbage;

    private int currentBuildingIndex = 0;

    void Start()
    {
        playerData = DataManager.instance.player;
        uIController = FindAnyObjectByType<UIController>();
        _notice = FindAnyObjectByType<NoticeUI>();
        foreach (GameObject building in structerList)
        {
            building.SetActive(false);
        }

        //UIInit();
    }

    public void build()
    {
        if (uIController.b_level == 1)
        {
            ActivateBuilding(1);
        }
        else if(uIController.b_level == 200)
        {
            ActivateBuilding(2);            
        }
        else if (uIController.b_level == 500)
        {
            ActivateBuilding(3);
        }
        else if (uIController.b_level == 700)
        {
            ActivateBuilding(4);
        }
        // else
        // {
            
        // }
    }

    public void OnBtnBuild()
    {
        int clickNum = EventSystem.current.currentSelectedGameObject.GetComponent<ButtonNum>().GetButtonNuum();
        Structer nowStructer = structerList[clickNum].GetComponent<Structer>();
        needGold = nowStructer.GetNeedGold();
        needGarbage = nowStructer.GetNeedGarbage();
        int needGoldIndex = nowStructer.UintCurrentIndex(needGold);
        int needGarbageIndex = nowStructer.UintCurrentIndex(needGarbage);

        //골드랑 쓰레기 소지 양이 더 많다면
        if (IsHaveUnit(needGoldIndex, needGarbageIndex))
        {
            // 돈 소비        
            playerData.SetUnitValue((int)Unit.GOLD, needGold);
            //쓰레기 소비
            playerData.SetUnitValue((int)Unit.GARBAGE, needGarbage);

            nowStructer.SetLevel(nowStructer.GetLevel() + 1);

            //레벨 표시 증가
            UIController uIController = FindAnyObjectByType<UIController>();
            uIController.lvTextBuild.text = "Lv." + nowStructer.GetLevel().ToString("D3");

            needGold[0] += 50;
            needGarbage[0] += 50;
            nowStructer.SetNeedGold(needGold);
            nowStructer.SetNeedGarbage(needGarbage);

            uIController.moneyTextBuild.text = nowStructer.NeedGoldToString();
            uIController.garbageTextBuild.text = nowStructer.NeedGarbageToString();

            build();
            _notice.SUB("건설!");
        }
        else
        {
            _notice.SUB("재화가 부족합니다!");
        }
        
    }

    public void UIInit()
    {
        for (int i = 0; i < structerList.Count; ++i)
        {
            Structer nowStructer = structerList[i].GetComponent<Structer>();

            //레벨 표시 증가
            UIController uIController = FindAnyObjectByType<UIController>();
            uIController.lvTextBuild.text = "Lv." + nowStructer.GetLevel().ToString("D3");

            nowStructer.SetNeedGold(needGold);
            nowStructer.SetNeedGarbage(needGarbage);

            uIController.moneyTextBuild.text = nowStructer.NeedGoldToString();
            uIController.garbageTextBuild.text = nowStructer.NeedGarbageToString();
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
        if (currentBuildingIndex <= index && index < structerList.Count)
        {
            structerList[index].SetActive(true);
            currentBuildingIndex = index + 1;
        }
    }

}
