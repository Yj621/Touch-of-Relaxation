using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    private NavMeshAgent workerNav;

    public bool isHaveGarbage;
    public bool isGoFactory;
    public bool isGoOut;
    public bool special1 = false;
    public bool special2 = false;
    public bool[] specialArry = new bool[12];
    public int increaseAmount;
    public int finalIncreaseAmount;

    private Transform outTarget;
    private Transform arriveTarget;

    // Start is called before the first frame update
    void Start()
    {
        workerNav = GetComponent<NavMeshAgent>();

        isHaveGarbage = false;
        isGoOut = false;
        isGoFactory = false;

        outTarget = GameObject.FindGameObjectWithTag("OutTarget").transform;
        arriveTarget = GameObject.FindGameObjectWithTag("ArriveTarget").transform;

        GoOut();

        for (int i = 0; i < specialArry.Length; i++)
        {
            specialArry[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy && isHaveGarbage && isGoFactory)
        {
            GoFactory();
        }
    }

    public void GoFactory()
    {
        workerNav.SetDestination(arriveTarget.position);
        isGoFactory = false; 
    }
    public void GoOut()
    {
        workerNav.SetDestination(outTarget.position);
        // 0���� 1 ������ ������ ���ڸ� �����մϴ�.
        float randomValue = Random.Range(0f, 1f);

        // Ȯ���� ���� Ư�� �ε����� ���� Ư�� ���� �����մϴ�.
        for (int i = 0; i < specialArry.Length; i++)
        {
            Debug.Log("randomValue : "+randomValue);
            if (randomValue < (i + 1) * 0.01f)
            {
                specialArry[i] = true;
                break; // Ư�� ���� �����Ǹ� ������ ����
            }
        }
    }

    //get set �Լ�
    public bool IsHaveGarbage()
    {
        return isHaveGarbage;
    }
    public void IsHaveGarbage(bool i)
    {
        isHaveGarbage = i;
    }
    public bool IsGoOut()
    {
        return isGoOut;
    }
    public void IsGoOut(bool i)
    {
        isGoOut = i;
    }
    public bool IsGoFactory()
    {
        return isGoFactory;
    }
    public void IsGoFactory(bool i)
    {
        isGoFactory = i;
    }


    private void OnTriggerEnter(Collider collision)
    {
        //�����⸦ �ֿ� �� �ǹ��� ������
        if (collision.gameObject.tag == "ArriveTarget" && isHaveGarbage)
        {
            DataManager.instance.player.SetUnitValue((int)Unit.GARBAGE, 5);
            DataManager.instance.player.IncreaseGage("Main", 5);
            isHaveGarbage = false;
            GoOut();
        }

        //������ �ݶ��̴��� �ɸ�(������ ���� �����⸦ ������)
        if (collision.gameObject.tag == "OutTarget" && !isHaveGarbage)
        {
            isGoFactory = true;
            isHaveGarbage = true;
            isGoOut = true;
        }
    }
}
