using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public List<GameObject> workerList;

    private int workerCount;
    private int currentWorkerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //이런 방식으로 사용
        //DataManager.instance.player

        //저장된 데이터가 없다고 가정
        workerCount = 1;

        for (int i=0; i< workerCount; i++)
        {
            workerList.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //작업자 수 항상 최신화
        if (workerCount < transform.childCount)
            workerCount = transform.childCount;

        //작업자들이 일하러 나갔는지 돌아왔는지 여부를 체크 
        for (currentWorkerIndex = 0; currentWorkerIndex < workerCount; currentWorkerIndex++)
        {
            WorkerScript w = workerList[currentWorkerIndex].GetComponent<WorkerScript>();
            w.gameObject.SetActive(!w.IsGoOut());

            if(w.IsGoOut())
                StartCoroutine(WorkerBack(w));
        }

        
    }

    private IEnumerator WorkerBack(WorkerScript w)
    {
        yield return new WaitForSeconds(1f);
        w.IsGoOut(false);

    }
}
