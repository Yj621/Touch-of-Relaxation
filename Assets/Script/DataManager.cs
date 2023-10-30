using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum StageNum { main = 0, city}

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
    }

    //��ȭ ���� ���� �ڵ�
    public void ConvertUnit(string s)
    {
        int[] unit = { };
        int index = 0;
        if (s == "Gold")
        {
            unit = _gold;
        }
        else if(s == "Garbage")
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

        // �� �ڻ��� ���� ���°��� �� �� �ֵ��� �ϴ� �ڵ�
        for (int i = 0; i < 26; i++)
        {
            if (unit[i] > 0)
            {
                index = i;
            }
        }
        // index�� ��ŭ �� ������ �����ϴ� �ݺ����� ������.
        for (int i = 0; i <= index; i++)
        {
            // ����, i��° �迭�� ���� 10000�̻��̶��
            // �ű⼭ 1000�� ���� �� �迭�� 1�� �����ش�.
            if (unit[i] >= 10000)
            {
                unit[i] -= 1000;
                unit[i + 1] += 1;
            }
            // ����, i��° �迭�� ���� �������
            if (unit[i] < 0)
            {
                // ����, i�� ���� ���� ���� �ڻ��� ������ ������
                // �� �迭���� 1�� ���� ������ i��° �迭�� 1000�� ���Ѵ�.
                if (index > i)
                {
                    unit[i + 1] -= 1;//??
                    unit[i] += 1000;
                }
            }
        }
    }

    //��ȭ�� string���� ��ȯ
    public string MyUnitToString(string s)
    {
        int[] unit = new int[26];
        int index = 0;
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

        for (int i = 0; i < 26; i++)
        {
            if (unit[i] > 0)
            {
                index = i;
            }
        }

        // �迭�� �ִ� ���� �÷��̾ �� �� �ִ� ��ȭ�� ���·� ǥ��
        float a = unit[index];
        // ����, index�� 0���� ũ�ٸ� �Ҽ����� ���´ٴ� ��
        if (index > 0)
        {
            float b = unit[index - 1];
            a += b / 1000;
        }
        // ����, 0�� ���ٸ� �ٷ� ���
        if (index == 0)
        {
            a += 0;
        }
        // �ڷ������� 65���� A�� ǥ���ϱ� ������ ���� �ڵ� 
        char str = (char)(65 + index);
        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + str.ToString();

        return p;

    }

    //��ȭ �� ����
    public void SetUnitValue(string s, int val)
    {
        int[] unit = { };
        int index = 0;
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

        for (int i = 0; i < 26; i++)
        {
            if (unit[i] > 0)
            {
                index = i;
            }
        }

        unit[index] += val;
    }

    //��ȭ�� ������ �ִ���
    public bool IsHaveUnit(string s)
    {
        int[] unit = { };
        int index = 0;
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

    public double ConfirmGage(string s)
    {
        int index = 0;
        if (s == "Main")
        {
            index = (int)StageNum.main;
        }
        return _stageGage[index];
    }
    public void IncreaseGage(string s , int val)
    {
        int index = 0;
        if(s== "Main")
        {
            index = (int)StageNum.main;
        }
        _stageGage[index] += (float)(val / 1000000.0f);
        Debug.Log((float)_stageGage[index]);
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
