using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structer : MonoBehaviour
{
    private string name;
    private int level;
    private int[] needGold;
    private int[] needGarbage;

    private void Awake()
    {
        name = "";
        level = 0;
        needGold = new int[26];
        needGarbage = new int[26];
        for(int i=0; i<26; ++i)
        {
            needGold[i] = 0;
            needGarbage[i] = 0;
        }
        needGold[0] = 50;
        needGarbage[0] = 50;
    }

    private void Update()
    {
        ConvertUnit();
    }
    //재화 단위 변경 코드
    public void ConvertUnit()
    {
        //골드 부분
        int index = UintCurrentIndex(needGold);
        for (int i = 0; i <= index; i++)
        {
            // 만약, i번째 배열에 돈이 10000이상이라면
            // 거기서 10000을 빼고 윗 배열에 1을 더해준다.
            if (needGold[i] >= 10000)
            {
                needGold[i] -= 10000;
                needGold[i + 1] += 1;
            }
            // 만약, i번째 배열의 값이 음수라면
            if (needGold[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번째 배열에 10000을 더한다.
                if (26 > i)
                {
                    needGold[i + 1] -= 1;
                    needGold[i] += 10000;
                }
            }

        }

        //쓰레기 부분
        index = UintCurrentIndex(needGold);
        for (int i = 0; i <= index; ++i) {
            // 만약, i번째 배열에 돈이 10000이상이라면
            // 거기서 10000을 빼고 윗 배열에 1을 더해준다.
            if (needGarbage[i] >= 10000)
            {
                needGarbage[i] -= 10000;
                needGarbage[i + 1] += 1;
            }
            // 만약, i번째 배열의 값이 음수라면
            if (needGarbage[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번째 배열에 10000을 더한다.
                if (26 > i)
                {
                    needGarbage[i + 1] -= 1;
                    needGarbage[i] += 10000;
                }
            }
        }
    }

    //재화를 string으로 변환
    public string NeedGoldToString()
    {
        int index = UintCurrentIndex(needGold);
        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현
        float a = needGold[index];
        if (index > 0)
        {
            float b = needGold[index - 1];
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
    //재화를 string으로 변환
    public string NeedGarbageToString()
    {
        int index = UintCurrentIndex(needGarbage);

        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현
        float a = needGarbage[index];
        if (index > 0)
        {
            float b = needGarbage[index - 1];
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

    public int UintCurrentIndex(int[] _unit)
    {
        int index = 0;
        for (int i = 0; i < 26; i++)
        {
            if (_unit[i] > 0)
            {
                index = i;
            }
        }
        return index;
    }

    public string GetName()
    {
        return name;
    }
    public void SetName(string _name)
    {
        name = _name;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }

    public int[] GetNeedGold()
    {
        return needGold;
    }

    public void SetNeedGold(int[] _needGold)
    {
        needGold = _needGold;
    }

    public int[] GetNeedGarbage()
    {
        return needGarbage;
    }

    public void SetNeedGarbage(int[] _needGarbage)
    {
        needGarbage = _needGarbage;
    }

}
