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
        //�̷� ������� ���
        //DataManager.instance.player

        //����� �����Ͱ� ���ٰ� ����
        workerCount = 1;

        for (int i=0; i< workerCount; i++)
        {
            workerList.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //�۾��� �� �׻� �ֽ�ȭ
        if (workerCount < transform.childCount)
            workerCount = transform.childCount;

        //�۾��ڵ��� ���Ϸ� �������� ���ƿԴ��� ���θ� üũ 
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
