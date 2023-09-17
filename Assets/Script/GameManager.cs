using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    private GameObject[] garbageSpawnPoints;
    private GameObject[] garbages;

    private float spawnRadius;
    public GameObject popUpPanel; // PopUp �г�
    private ChangePanelController changePanelController; // ChangePanelController ��ũ��Ʈ�� �ν��Ͻ�


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SpawnGarbage();
        // ChangePanelController ��ũ��Ʈ�� �ν��Ͻ� ��������
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

    //�ǹ� ���� �ڵ�
    private void ObjectSelect()
    {
        if (changePanelController.PanelOn==false && Input.touchCount > 0)
        {
            //��ġ�� �߻��Ǹ�
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); //��ġ ���� �޾ƿ�

                //��ġ�� ������ ����
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Vector2 touchPos = new Vector2(touch.position.x, touch.position.y); //��ġ ��ġ �޾ƿ�

                    //����ĳ��Ʈ�� �̿��� ��ġ ������Ʈ �޾ƿ�
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touchPos);
                    Physics.Raycast(ray, out hit);

                    if (hit.collider != null && (hit.collider.tag == "TouchPossible" || hit.collider.tag == "ArriveTarget")) 
                    {
                        GameObject CurrentTouch = hit.transform.gameObject;
                        Debug.Log(CurrentTouch.gameObject.name);
                        // ChangePanelController�� PopUpPanelTrue �Լ� ȣ��
                        changePanelController.PopUpPanelTrue();
                        Debug.Log("ObjectSelect: " + changePanelController.PanelOn);
                    }
                }
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