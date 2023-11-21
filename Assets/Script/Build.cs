using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject[] buildings;
    private int currentBuildingIndex = 0;

    void Start()
    {
        foreach (GameObject building in buildings)
        {
            building.SetActive(false);
        }
    }

    public void build()
    {
        if (currentBuildingIndex < buildings.Length)
        {
            buildings[currentBuildingIndex].SetActive(true);
            currentBuildingIndex++;
        }
    }
}
