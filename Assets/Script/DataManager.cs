using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private string _name;   //플레이어 닉네임

    //worker 관련 
    private int _workerCount;
    private double _workerPower;
    private double _workerHealth;
    private double _workerSpeed;
    private double _workerSight;

    //재화 관련
    private int _garbage;
    private int _energy;
    private double _gold;
    private double _diamond;

    //스테이지 진행도 관련
    private double _mainGage;
    private double _cityGage;

    public PlayerData()
    {
        _workerCount = 1;
        _mainGage = 0;
    }

    //전체 정보 최신화
    public void UpdatePlayerInfo(int w=1, int e=0, int g=0, int d=0)
    {
        _workerCount = 1;
        _garbage = 0;
        _energy = 0;
        _gold = 0;
        _diamond = 0;
    }

    public void WorkerCount(int w)
    {
        _workerCount = w;
    }
    public int WorkerCount()
    {
        return _workerCount;
    }

    public void Garbage(int g)
    {
        _garbage = g;
    }
    public int Garbage()
    {
        return _garbage;
    }

    public void Energy(int e)
    {
        _energy = e;
    }
    public int Energy()
    {
        return _energy;
    }

    public void Gold(double g)
    {
        _gold = g;
    }
    public double Gold()
    {
        return _gold;
    }

    public void Dia(double d)
    {
        _diamond = d;
    }
    public double Dia()
    {
        return _diamond;
    }

    public void MainGage(double m)
    {
        _mainGage = m;
    }
    public double MainGage()
    {
        return _mainGage;
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
