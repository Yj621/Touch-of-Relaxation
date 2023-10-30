using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum StageNum { main = 0, city}

public class PlayerData
{
    StageNum stageNum;
    private string _name;   //플레이어 닉네임

    //worker 관련 
    private int _workerCount;
    private double _workerPower;
    private double _workerHealth;
    private double _workerSpeed;
    private double _workerSight;

    //재화 관련
    private int[] _garbage;
    private int[] _energy;
    private int[] _gold;
    private int[] _diamond;

    //스테이지 진행도 관련
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

    //재화 단위 변경 코드
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

        // 내 자산의 현재 상태값을 알 수 있도록 하는 코드
        for (int i = 0; i < 26; i++)
        {
            if (unit[i] > 0)
            {
                index = i;
            }
        }
        // index값 만큼 돈 단위를 정리하는 반복문을 돌린다.
        for (int i = 0; i <= index; i++)
        {
            // 만약, i번째 배열에 돈이 10000이상이라면
            // 거기서 1000을 빼고 윗 배열에 1을 더해준다.
            if (unit[i] >= 10000)
            {
                unit[i] -= 1000;
                unit[i + 1] += 1;
            }
            // 만약, i번째 배열의 값이 음수라면
            if (unit[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번째 배열에 1000을 더한다.
                if (index > i)
                {
                    unit[i + 1] -= 1;//??
                    unit[i] += 1000;
                }
            }
        }
    }

    //재화를 string으로 변환
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

        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현
        float a = unit[index];
        // 만약, index가 0보다 크다면 소수점이 나온다는 것
        if (index > 0)
        {
            float b = unit[index - 1];
            a += b / 1000;
        }
        // 만약, 0과 같다면 바로 출력
        if (index == 0)
        {
            a += 0;
        }
        // 자료형에서 65부터 A를 표현하기 때문에 쓰는 코드 
        char str = (char)(65 + index);
        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + str.ToString();

        return p;

    }

    //재화 양 변경
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

    //재화를 가지고 있는지
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

    //전체 정보 최신화
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
    //싱글톤 구조
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
