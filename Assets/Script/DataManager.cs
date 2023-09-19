using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private string _name;
    private int _workerCount;
    private int _energy;
    private int _diamond;

    private double _gold;
    private double _goldIncrease;


    private double _mainGage;
    private double _cityGage;

    public PlayerData()
    {
        _workerCount = 1;
        _goldIncrease = 1f;
        _mainGage = 0;
    }

    //전체 정보 최신화
    public void UpdatePlayerInfo(int w, int e, int g, int d)
    {
        _workerCount = 1;
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

    public double Gold()
    {
        return _gold;
    }
    public void GoldIncrease()
    {
        _gold += _goldIncrease;
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
