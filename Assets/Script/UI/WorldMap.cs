using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMap : MonoBehaviour
{
    public GameObject[]            mapWindowChildren;
    public Image[]                  worldImage;


    private void Start()
    {
        WorldImageColor();
        for (int i = 0; i < mapWindowChildren.Length; ++i)
        {
            mapWindowChildren[i].gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        
    }

    void WorldImageColor()
    {
        for (int i = 0; i < worldImage.Length; ++i)
        {
            worldImage[i].color = Color.black;
        }
    }

    public void OnBtnMap()
    {
        for (int i = 0; i < mapWindowChildren.Length; ++i)
        {
            mapWindowChildren[i].SetActive(true);
        }
    }
    public void OnBtnMapClose()
    {
        for (int i = 0; i < mapWindowChildren.Length; ++i)
        {
            mapWindowChildren[i].SetActive(true);
        }
    }

    //¸Ê ¹öÆ°
    public void OnBtnForest()
    {
        OnBtnMapClose();
        SceneManager.LoadScene("ForestStage");
    }

    public void OnBtnCity()
    {
        if (DataManager.instance.player.ConfirmGage("Forest") >= 0.5f)
        {
            OnBtnMapClose();
            SceneManager.LoadScene("CtiyStage");
        }
        else
        {
            Debug.Log("Forest gauge is not equal to or greater than 0.5");
        }

    }
    public void OnBtnCountry()
    {
        if (DataManager.instance.player.ConfirmGage("CITY") >= 0.5f)
        {
            OnBtnMapClose();
            SceneManager.LoadScene("CountrySideStage");
        }
        else
        {
            Debug.Log("CITY gauge is not equal to or greater than 0.5");
        }
    }
    public void OnBtnSea()
    {
        if (DataManager.instance.player.ConfirmGage("COUNTRY") >= 0.5f)
        {
            OnBtnMapClose();
            SceneManager.LoadScene("SeaStage");
        }
        else
        {
            Debug.Log("COUNTRY gauge is not equal to or greater than 0.5");
        }
    }
    public void OnBtnVillage()
    {
        if (DataManager.instance.player.ConfirmGage("SEA") >= 0.5f)
        {
            OnBtnMapClose();
            SceneManager.LoadScene("VillageStage");
        }
        else
        {
            Debug.Log("SEA gauge is not equal to or greater than 0.5");
        }

    }

}
