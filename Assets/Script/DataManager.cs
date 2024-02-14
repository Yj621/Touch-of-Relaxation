using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

enum StageNum { MAIN = 0, FOREST, CITY,}


public class PlayerData
{
    StageNum stageNum;
    private string _name;   //�÷��̾� �г���



    //worker ���� 
    private int _workerCount;
    private double _workerPower;
    private double _workerHealth;
    private double _workerSpeed;
    private double _workerSight;

    //��ȭ ����
    private int[] _garbage;
    private int[] _energy;
    private int[] _gold;
    private int[] _diamond;

    //�������� ���൵ ����
    private float[] _stageGage;

    public PlayerData()
    {
        _workerCount = 1;

        _garbage = new int[26];
        _energy = new int[26];
        _gold = new int[26];
        _diamond = new int[26];

        _stageGage = new float[6];

        for(int i=0; i<26; i++)
        {
            _garbage[i] = 0;
            _energy[i] = 0;
            _gold[i] = 0;
            _diamond[i] = 0;
        }
        _energy[0] = 10100;
        _garbage[0] = 10100;
        _gold[0] = 10100;
    }

    public int[] Gold()
    {
        return _gold;
    }

    public int[] Garbage()
    {
        return _garbage;
    }

    #region ��ȭ ���� �Լ���

    //��ȭ ���� ���� �ڵ�
    public void ConvertUnit(int _unitNum)
    {
        int[] unit = { };
        int index = 0;

        index = UintCurrentIndex(_unitNum, ref unit);

        // index�� ��ŭ �� ������ �����ϴ� �ݺ����� ������.
        for (int i = 0; i <= index; i++)
        {
            // ����, i��° �迭�� ���� 10000�̻��̶��
            // �ű⼭ 10000�� ���� �� �迭�� 1�� �����ش�.
            if (unit[i] >= 10000)
            {
                unit[i] -= 10000;
                unit[i + 1] += 1;
            }
            // ����, i��° �迭�� ���� �������
            if (unit[i] < 0)
            {
                // ����, i�� ���� ���� ���� �ڻ��� ������ ������
                // �� �迭���� 1�� ���� ������ i��° �迭�� 10000�� ���Ѵ�.
                if (index > i)
                {
                    unit[i + 1] -= 1;
                    unit[i] += 10000;
                }
            }
        }
    }

    //��ȭ�� string���� ��ȯ
    public string MyUnitToString(int _unitNum)
    {
        int[] unit = new int[26];
        int index = 0;
        index = UintCurrentIndex(_unitNum, ref unit);
        // �迭�� �ִ� ���� �÷��̾ �� �� �ִ� ��ȭ�� ���·� ǥ��
        float a = unit[index];
        if (index > 0)
        {
            float b = unit[index - 1];
            a += b / 10000;
        }
        if (index == 0)
        {
            a += 0;
        }

        char str = (char)(65 + index);
        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + str.ToString();

        return p;

    }

    //��ȭ �� ����

    public void SetUnitValue(int _unitNum, int[] _minusUnit)
    {
        int[] unit = new int[26];

        if (_unitNum == (int)Unit.GOLD)
            unit = _gold;
        else
            unit = _garbage;

        for (int i=0; i<26; ++i)
        {
            unit[i] -= _minusUnit[i];
        }

    }
    public void SetUnitValue(int _unitNum, int _amount, int _index = 0)
    {
        int[] unit = new int[26];
        UintCurrentIndex(_unitNum, ref unit);
        unit[_index] += _amount;
    }

    //��ȭ�� ������ �ִ���
    public bool IsHaveUnit(string s)
    {
        int[] unit = { };

        if (s == "Gold")
        {
            unit = _gold;
        }
        else if (s == "Garbage")
        {
            unit = _garbage;
        }
        else if (s == "Energy")
        {
            unit = _energy;
        }
        else if (s == "Diamon")
        {
            unit = _diamond;
        }

        if (unit[0] > 0)
            return true;
        return false;

    }

    //��ȭ �� ��ȯ
    public int UnitValue(int _unitNum, int _index = 0)    
    {
        int[] unit = { };

        switch (_unitNum)
        {
            case (int)Unit.GOLD:
                return _gold[_index];

            case (int)Unit.GARBAGE:
                return _garbage[_index];
                break;

            case (int)Unit.ENERGY:
                return _energy[_index];
                break;

            case (int)Unit.DIAMOND:
                return _diamond[_index];
                break;

            default:
                break;
        }

        return 0;
    }

    //���� ��ȭ �ε��� ���ϱ�
    public int UintCurrentIndex(int _unitNum, ref int[] _unit)
    {
        int index = 0;

        switch (_unitNum)
        {
            case (int)Unit.GOLD:
                _unit = _gold;
                break;

            case (int)Unit.GARBAGE:
                _unit = _garbage;
                break;

            case (int)Unit.ENERGY:
                _unit = _energy;
                break;

            case (int)Unit.DIAMOND:
                _unit = _diamond;
                break;

            default:
                break;
        }

        for (int i = 0; i < 26; i++)
        {
            if (_unit[i] > 0)
            {
                index = i;
            }
        }
        return index;
    }


    #endregion ��ȭ ���� �Լ���

    public double ConfirmGage(string s)
    {
        int index = 0;
        if (s == "Main")
        {
            index = (int)StageNum.MAIN;
        }
        if(s=="Forest")
        {
            index = (int)StageNum.FOREST;
        }
        return _stageGage[index];
    }
    public void IncreaseGage(string s , int val)
    {
        int index = 0;
        if(s== "Main")
        {
            index = (int)StageNum.MAIN;
        }
        _stageGage[index] += (float)(val / 100000.0f);
    }

    //��ü ���� �ֽ�ȭ
    public void UpdatePlayerInfo(int w=1, int e=0, int g=0, int d=0)
    {
        _workerCount = 1;
    }

    public void WorkerCount(int w)
    {
        _workerCount = w;
    }
    public int WorkerCount()
    {
        return _workerCount;
    }

}



public class DataManager : MonoBehaviour
{
    //�̱��� ����
    public static DataManager instance;

    public PlayerData player = new PlayerData();
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private void Update()
    {
        //Debug.Log(player.Gold());
    }

}
