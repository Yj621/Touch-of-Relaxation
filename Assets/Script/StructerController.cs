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

        //���� ������ ���� ���� �� ���ٸ�
        if (IsHaveUnit(needGoldIndex, needGarbageIndex))
        {
            // �� �Һ�        
            playerData.SetUnitValue((int)Unit.GOLD, needGold);
            //������ �Һ�
            playerData.SetUnitValue((int)Unit.GARBAGE, needGarbage);

            nowStructer.SetLevel(nowStructer.GetLevel() + 1);

            //���� ǥ�� ����
            UIController uIController = FindAnyObjectByType<UIController>();
            uIController.lvTextBuild.text = "Lv." + nowStructer.GetLevel().ToString("D3");

            needGold[0] += 50;
            needGarbage[0] += 50;
            nowStructer.SetNeedGold(needGold);
            nowStructer.SetNeedGarbage(needGarbage);

            uIController.moneyTextBuild.text = nowStructer.NeedGoldToString();
            uIController.garbageTextBuild.text = nowStructer.NeedGarbageToString();

            build();
            _notice.SUB("�Ǽ�!");
        }
        else
        {
            _notice.SUB("��ȭ�� �����մϴ�!");
        }
        
    }

    public void UIInit()
    {
        for (int i = 0; i < structerList.Count; ++i)
        {
            Structer nowStructer = structerList[i].GetComponent<Structer>();

            //���� ǥ�� ����
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
