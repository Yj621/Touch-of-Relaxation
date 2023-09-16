using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    private NavMeshAgent workerNav;

    public bool isHaveGarbage;
    public bool isGorFactory;
    public bool isGoOut;

    private Transform outTarget;
    private Transform arriveTarget;

    // Start is called before the first frame update
    void Start()
    {
        workerNav = GetComponent<NavMeshAgent>();

        isHaveGarbage = false;
        isGoOut = false;
        isGorFactory = false;

        outTarget = GameObject.FindGameObjectWithTag("OutTarget").transform;
        arriveTarget = GameObject.FindGameObjectWithTag("ArriveTarget").transform;

        GoOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy && isHaveGarbage && isGorFactory)
        {
            isGorFactory = false;
            GoFactory();
        }
    }

    public void GoFactory()
    {
        workerNav.SetDestination(arriveTarget.position);
    }
    public void GoOut()
    {
        workerNav.SetDestination(outTarget.position);
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


    private void OnTriggerEnter(Collider collision)
    {
        //쓰레기를 주운 후 건물에 도착함
        if (collision.gameObject.tag == "ArriveTarget" && isHaveGarbage)
        {
            DataManager.instance.player.GoldIncrease();
            isHaveGarbage = false;
            GoOut();
        }

        //나가는 콜라이더에 걸림(나갔다 오면 쓰레기를 가져옴)
        if (collision.gameObject.tag == "OutTarget" && !isHaveGarbage)
        {
            isGorFactory = true;
            isHaveGarbage = true;
            isGoOut = true;
        }
    }
}
