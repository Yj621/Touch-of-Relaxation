using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DontDestroyObject : MonoBehaviour
{    
    private void Awake()
    {
        var obj = FindObjectsOfType<DontDestroyObject>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // public GameObject canvas;
    // public GameObject worker;
    
    // private void Awake()
    // {
    //     canvas = GameObject.Find("Canvas");
    //     worker = GameObject.Find("Worker");
        
    //     int canvasCount = GameObject.FindGameObjectsWithTag("Canvas").Length;
    //     int workerCount = GameObject.FindGameObjectsWithTag("Worker").Length;
        
    //     if (canvas != null && canvasCount == 1)
    //     {
    //         DontDestroyOnLoad(canvas);
    //     }
    //     else if (canvas != null)
    //     {
    //         Destroy(canvas);
    //     }
        
    //     if (worker != null && workerCount == 1)
    //     {
    //         DontDestroyOnLoad(worker);
    //     }
    //     else if (worker != null)
    //     {
    //         Destroy(worker);
    //     }
    // }
}
