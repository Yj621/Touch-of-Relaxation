using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private GameObject[] garbageSpawnPoints;
    private GameObject[] garbages;

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
        //���� ������Ʈ ��ġ���� spawnRadius�� �Ÿ��� �������� ����
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
    }


    //�ʱ�ȭ
    private void Init()
    {
        Screen.SetResolution(1440, 3040, true);
        garbageSpawnPoints = GameObject.FindGameObjectsWithTag("GarbageSpawnPoint"); //�����⸦ ���� �� �� ������Ʈ�� ��� ã�ƿ�
        garbages = Resources.LoadAll<GameObject>("GarbagePrefabs"); //����� ������ �����յ��� ���� �ҷ���
        spawnRadius = 10f;
    }


}