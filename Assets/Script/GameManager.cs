using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private GameObject[] garbageSpawnPoints;
    private GameObject[] garbages;

    public GameObject[] garbageDelete;

    private float spawnRadius;
    private PlayerData playerData;

    private bool isChanging;
    public UnityEngine.UI.Slider slider;
    public Text textAmountOfGoods;


    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.player;
        isChanging = false;
        Init();
        SpawnGarbage();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.IsHaveUnit("Garbage") && !isChanging)
        {
            isChanging = true;
            Invoke("ChangeGarbageToEnergy", 1f);
        }

    }

    private void ChangeGarbageToEnergy()
    {
        playerData.SetUnitValue((int)Unit.GARBAGE, -1);
        playerData.SetUnitValue((int)Unit.ENERGY, 1);
        isChanging = false;
    }

    private void SpawnGarbage()
    {
        //스폰 오브젝트 위치에서 spawnRadius의 거리를 랜덤으로 스폰
        for (int i = 0; i < garbageSpawnPoints.Length; i++)
        {
            int garbageCount = Random.Range(5, 10);

            for (int j = 0; j < garbageCount; j++)
            {
                int garbageNum = Random.Range(0, garbages.Length);
                float x = Random.Range(-spawnRadius + garbageSpawnPoints[i].transform.position.x, spawnRadius + garbageSpawnPoints[i].transform.position.x);
                float z = Random.Range(-spawnRadius + garbageSpawnPoints[i].transform.position.z, spawnRadius + garbageSpawnPoints[i].transform.position.z);

                Vector3 sapwnPoint = new Vector3(x, garbageSpawnPoints[i].transform.position.y + 5, z);
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
                GameObject spawnedObject = Instantiate(garbages[garbageNum], sapwnPoint, spawnRotation);
                spawnedObject.transform.SetParent(garbageSpawnPoints[i].transform);
                spawnedObject.transform.localScale = new Vector3(5f, 5f, 5f);
            }
        }

        List<int> indexesToDelete = new List<int>();
        double gage = 0.0f;

        switch (SceneManager.GetActiveScene().name)
        {
            case "ForestStage":
                gage = DataManager.instance.player.ConfirmGage("Forest");
                break;
            case "CityStage":
                gage = DataManager.instance.player.ConfirmGage("CITY");
                break;
            case "CountrySideStage":
                gage = DataManager.instance.player.ConfirmGage("COUNTRY");
                break;
            case "SeaStage":
                gage = DataManager.instance.player.ConfirmGage("SEA");
                break;
            case "VillageStage":
                gage = DataManager.instance.player.ConfirmGage("VILLAGE");
                break;
        }

        if (gage >= 0.3f)
        {
            indexesToDelete.AddRange(new int[] { 0, 2, 4 });
        }
        if (gage >= 0.5f)
        {
            indexesToDelete.AddRange(new int[] { 1, 3, 5, 7 });
        }
        if (gage >= 1.0f)
        {
            indexesToDelete.AddRange(new int[] { 6, 8, 9, 10 });
        }

        foreach (int index in indexesToDelete)
        {
            if (index < garbageDelete.Length)
            {
                garbageDelete[index].gameObject.SetActive(false);
            }
        }



    }


    //초기화
    private void Init()
    {
        Screen.SetResolution(1440, 3040, true);
        garbageSpawnPoints = GameObject.FindGameObjectsWithTag("GarbageSpawnPoint"); //쓰레기를 스폰 할 빈 오브젝트를 모두 찾아옴
        garbages = Resources.LoadAll<GameObject>("GarbagePrefabs"); //사용할 쓰레기 프리팹들을 전부 불러옴
        spawnRadius = 10f;
    }


}