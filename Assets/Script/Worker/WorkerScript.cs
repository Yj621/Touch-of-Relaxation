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
        // 0부터 1 사이의 무작위 숫자를 생성합니다.
        float randomValue = Random.Range(0f, 1f);

        // 50% 확률
        if (randomValue < 0.5f)
        {
            special1 = true;
        }
        else if(randomValue < 0.8f)
        {
            special2 = true;
        }
    }

    //get set 함수
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
        //쓰레기를 주운 후 건물에 도착함
        if (collision.gameObject.tag == "ArriveTarget" && isHaveGarbage)
        {
            DataManager.instance.player.Garbage(DataManager.instance.player.Garbage()+1);
            isHaveGarbage = false;
            GoOut();
        }

        //나가는 콜라이더에 걸림(나갔다 오면 쓰레기를 가져옴)
        if (collision.gameObject.tag == "OutTarget" && !isHaveGarbage)
        {
            isGoFactory = true;
            isHaveGarbage = true;
            isGoOut = true;
        }
    }
}
