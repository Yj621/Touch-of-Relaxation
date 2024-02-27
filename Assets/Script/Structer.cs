using System;
using UnityEngine;
using UnityEditor.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

enum PARTICLEGRADIENT { GREEN,BLUE,PURPLE,GOLD,RED }

public class Structer : MonoBehaviour
{
    private string          name;
    private int             level;
    public int              initGold;
    public int              initGarbage;
    private int[]           needGold;
    private int[]           needGarbage;
    public GameObject[]     particles;

    private Gradient         colorGradient;
    private Gradient[]      chageGradientArray;

public Text             lvTextBuild;
    public Text             goldTextBuild;
    public Text             garbageTextBuild;
    public Button           buildButton;

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

        level = 0;
        needGold[0] = initGold;
        needGarbage[0] = initGarbage;

        if (lvTextBuild != null)
        {
            lvTextBuild.text = "Lv." + level.ToString("D3");
            goldTextBuild.text = NeedGoldToString();
            garbageTextBuild.text = NeedGarbageToString();
        }
    }

    void Start()
    {
        InitParticleColor();
    }

    public void LevelUP()
    {
        // 레벨 및 소요 재화 재설정
        ++level;

        needGold[0] += 50;
        needGarbage[0] += 50;

        ConvertUnit();
        lvTextBuild.text = "Lv." + level.ToString("D3");
        goldTextBuild.text = NeedGoldToString();
        garbageTextBuild.text = NeedGarbageToString();
        ChangeParticle();

        if (0 < level && level < 1000)
        {
            buildButton.GetComponentInChildren<Text>().text = "레벨 업";
        }
        else if (level > 1000)
        {
            buildButton.GetComponentInChildren<Text>().text = "완성";
        }
        else
        {
            buildButton.GetComponentInChildren<Text>().text = "건설";
        }
    }

    // 파티클 색을 바꾸는 함수
    public void ChangeParticle()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        var colorOverLifetime = particleSystem.colorOverLifetime;

        if (level >= 1 && level < 300)
        {
            colorGradient = chageGradientArray[(int)PARTICLEGRADIENT.GREEN];
        }
        else if (level >= 300 && level < 500)
        {
            colorGradient = chageGradientArray[(int)PARTICLEGRADIENT.BLUE];
        }
        else if (level >= 501 && level < 700)
        {
            colorGradient = chageGradientArray[(int)PARTICLEGRADIENT.PURPLE];
        }

        colorOverLifetime.enabled = true;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(colorGradient);
    }

    //재화를 텍스트로 변환
    public void ConvertUnit()
    {
        int index = UintCurrentIndex(needGold);
        for (int i = 0; i <= index; i++)
        {
            if (needGold[i] >= 10000)
            {
                needGold[i] -= 10000;
                needGold[i + 1] += 1;
            }
            if (needGold[i] < 0)
            {
                if (26 > i)
                {
                    needGold[i + 1] -= 1;
                    needGold[i] += 10000;
                }
            }

        }
        //������ �κ�
        index = UintCurrentIndex(needGold);
        for (int i = 0; i <= index; ++i) {
            // ����, i��° �迭�� ���� 10000�̻��̶��
            // �ű⼭ 10000�� ���� �� �迭�� 1�� �����ش�.
            if (needGarbage[i] >= 10000)
            {
                needGarbage[i] -= 10000;
                needGarbage[i + 1] += 1;
            }
            // ����, i��° �迭�� ���� �������
            if (needGarbage[i] < 0)
            {
                // ����, i�� ���� ���� ���� �ڻ��� ������ ������
                // �� �迭���� 1�� ���� ������ i��° �迭�� 10000�� ���Ѵ�.
                if (26 > i)
                {
                    needGarbage[i + 1] -= 1;
                    needGarbage[i] += 10000;
                }
            }
        }
    }

    //��ȭ�� string���� ��ȯ
    public string NeedGoldToString()
    {
        int index = UintCurrentIndex(needGold);
        // �迭�� �ִ� ���� �÷��̾ �� �� �ִ� ��ȭ�� ���·� ǥ��
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
    //��ȭ�� string���� ��ȯ
    public string NeedGarbageToString()
    {
        int index = UintCurrentIndex(needGarbage);

        // �迭�� �ִ� ���� �÷��̾ �� �� �ִ� ��ȭ�� ���·� ǥ��
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

    private void InitParticleColor()
    {
        chageGradientArray = new Gradient[5];
        //초록
        chageGradientArray[(int)PARTICLEGRADIENT.GREEN] = new Gradient();
        chageGradientArray[(int)PARTICLEGRADIENT.GREEN].SetKeys(new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });

        //파랑
        chageGradientArray[(int)PARTICLEGRADIENT.BLUE] = new Gradient();
        chageGradientArray[(int)PARTICLEGRADIENT.BLUE].SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });

        //보라
        chageGradientArray[(int)PARTICLEGRADIENT.PURPLE] = new Gradient();
        chageGradientArray[(int)PARTICLEGRADIENT.PURPLE].SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.5f, 0.0f, 0.5f), 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f) });

        //황금
        chageGradientArray[(int)PARTICLEGRADIENT.GOLD] = new Gradient();
        chageGradientArray[(int)PARTICLEGRADIENT.GOLD].SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1.0f, 0.84f, 0.0f), 0.75f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f) });

        //빨강
        chageGradientArray[(int)PARTICLEGRADIENT.GOLD] = new Gradient();
        chageGradientArray[(int)PARTICLEGRADIENT.GOLD].SetKeys(new GradientColorKey[] { new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f) });
    }
}
