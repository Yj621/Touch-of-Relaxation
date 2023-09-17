using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    private GameObject[] garbageSpawnPoints;
    private GameObject[] garbages;

    private float spawnRadius;
    public GameObject popUpPanel; // PopUp 패널
    private ChangePanelController changePanelController; // ChangePanelController 스크립트의 인스턴스


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SpawnGarbage();
        // ChangePanelController 스크립트의 인스턴스 가져오기
        changePanelController = FindObjectOfType<ChangePanelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ObjectSelect();
        }
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
    }

    //건물 선택 코드
    private void ObjectSelect()
    {
        if (changePanelController.PanelOn==false && Input.touchCount > 0)
        {
            //터치가 발생되면
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); //터치 상태 받아옴

                //터치가 끝나는 순간
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Vector2 touchPos = new Vector2(touch.position.x, touch.position.y); //터치 위치 받아옴

                    //레이캐스트를 이용해 터치 오브젝트 받아옴
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touchPos);
                    Physics.Raycast(ray, out hit);

                    if (hit.collider != null && (hit.collider.tag == "TouchPossible" || hit.collider.tag == "ArriveTarget")) 
                    {
                        GameObject CurrentTouch = hit.transform.gameObject;
                        Debug.Log(CurrentTouch.gameObject.name);
                        // ChangePanelController의 PopUpPanelTrue 함수 호출
                        changePanelController.PopUpPanelTrue();
                        Debug.Log("ObjectSelect: " + changePanelController.PanelOn);
                    }
                }
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