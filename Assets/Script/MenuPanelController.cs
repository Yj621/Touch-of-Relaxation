using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public GameObject panelConstruction; 
    public GameObject panelAbility;
    public GameObject panelTool;
    public GameObject panelStore;
    private GameObject menu;
    private GameObject menuImg;
    private float moveDistance = -918f; // 이동할 거리를 설정
    private float animationSpeed = 2000f; // 내려가는 애니메이션 속도를 설정
    private bool isMenuDown = false; // 현재 Menu가 아래로 내려갔는지 여부를 저장
    private Vector3 targetPosition; // 목표 위치를 저장
    private Quaternion startRotation; // 메뉴 이미지의 원래의 회전 값을 저장하기 위한 변수
    void Start()
    {
        menu = GameObject.Find("Menu"); 
        targetPosition = menu.transform.localPosition;
        menuImg = GameObject.Find("Img_Menu"); 
        startRotation = menuImg.transform.rotation; // 초기 회전값 저장
    }

    void Update()
    {
        // Menu 오브젝트를 목표 위치로 천천히 내리기
        menu.transform.localPosition = Vector3.MoveTowards(menu.transform.localPosition, targetPosition, animationSpeed * Time.deltaTime);
    }

    //메뉴 버튼 클릭 함수
    public void OnBtnConstruction()
    {
        if (panelConstruction != null)
        {
            panelConstruction.SetActive(true);
            panelAbility.SetActive(false);
            panelTool.SetActive(false);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnAbility()
    {
        if (panelAbility != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(true);
            panelTool.SetActive(false);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnTool()
    {
        if (panelTool != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(false);
            panelTool.SetActive(true);
            panelStore.SetActive(false);
        }
    }

    public void OnBtnStore()
    {
        if (panelStore != null)
        {
            panelConstruction.SetActive(false);
            panelAbility.SetActive(false);
            panelTool.SetActive(false);
            panelStore.SetActive(true);
        }
    }

    public void OnBtnMenuClose()
    {
        // 상태를 토글
        isMenuDown = !isMenuDown;
        // 목표 위치를 설정
        targetPosition = isMenuDown ? new Vector3(targetPosition.x, targetPosition.y + moveDistance, targetPosition.z) : new Vector3(targetPosition.x, targetPosition.y - moveDistance, targetPosition.z);
        // 회전 상태도 토글
        menuImg.transform.rotation = isMenuDown ? Quaternion.Euler(0, 0, 90) : startRotation;
  
    }
}
