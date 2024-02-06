using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject[] buildings;
    private int currentBuildingIndex = 0;
    private UIController uIController;

    void Start()
    {
        uIController = FindAnyObjectByType<UIController>();
        foreach (GameObject building in buildings)
        {
            building.SetActive(false);
        }
    }

    public void build()
    {
        if (uIController.b_level == 1)
        {
            ActivateBuilding(1);
        }
        else if(uIController.b_level == 200)
        {
            ActivateBuilding(2);            
        }
        else if (uIController.b_level == 500)
        {
            ActivateBuilding(3);
        }
        else if (uIController.b_level == 700)
        {
            ActivateBuilding(4);
        }
        // else
        // {
            
        // }
    }

    private void ActivateBuilding(int index)
    {
        if (currentBuildingIndex <= index && index < buildings.Length)
        {
            buildings[index].SetActive(true);
            currentBuildingIndex = index + 1;
        }
    }

}
