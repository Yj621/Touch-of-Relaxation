using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    private float cameraMoveSpeed;
    private float cameraZoomSpeed;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    private Camera mainCamera;

    private UIController uiController; // UIController 스크립트

    private void Start()
    {
        Init();
        uiController = FindObjectOfType<UIController>();
    }

    void Update()
    {
        if (!uiController.isPanelOn && uiController.isMenuDown)
        {
            //손가락 하나가 눌렸을 때 -> 화면 이동
            if (Input.touchCount == 1)
            {
                CameraMove();
            }
            //손가락 두개가 눌렸을 때 -> 줌인 줌 아웃
            else if (Input.touchCount == 2)
            {
                CameraZoomInOut();
            }
        }
    }
    
    //한 손가락 터치
    private void CameraMove()
    {
        Touch touch = Input.GetTouch(0);//터치 상태 받아옴

        if (touch.phase == TouchPhase.Began)
        {
            prePos = touch.position - touch.deltaPosition;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            nowPos = touch.position - touch.deltaPosition;
            movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * cameraMoveSpeed;
            mainCamera.transform.Translate(movePos);
            prePos = touch.position - touch.deltaPosition;
        }


        //터치가 끝나는 순간과 화면이 0.05 밑으로 움직였을 때
        if (Input.GetTouch(0).phase == TouchPhase.Ended && movePos.magnitude < 0.05f)
        {
            Vector2 touchPos = new Vector2(touch.position.x, touch.position.y); //터치 위치 받아옴

            //레이캐스트를 이용해 터치 오브젝트 받아옴
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            Physics.Raycast(ray, out hit);
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider != null && (hit.collider.tag == "TouchPossible" || hit.collider.tag == "ArriveTarget"))
            {
                GameObject CurrentTouch = hit.transform.gameObject;
                // ChangePanelController의 PopUpPanelTrue 함수 호출
                uiController.PopUpPanelTrue();
            }

        }
    }

    //카메라 줌인 줌아웃 기능
    private void CameraZoomInOut()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touchPrevPos0 = touch0.position - touch0.deltaPosition; //touch0의 이전 포지션 -> deltaposition은 이동 방향 추적
        Vector2 touchPrevPos1 = touch1.position - touch1.deltaPosition;  //touch1의 이전 포지션

        float prevTouchDistance = (touchPrevPos0 - touchPrevPos1).magnitude; //이전 터치 사이의 거리
        float currentTouchDistance = (touch0.position - touch1.position).magnitude; // 현재 터치 사이의 거리

        //두 거리의 차이를 구함
        //touchDiff가 -이면 줌인 +이면 줌아웃
        float touchDiff = prevTouchDistance - currentTouchDistance;

        //우리 카메라는 원근법이 적용된 Perspective형식
        mainCamera.fieldOfView += touchDiff * cameraZoomSpeed;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 0.1f, 179.9f); //Mathf.Clamp를 사용한 최소 최대 사이즈 지정
    }

    private void Init()
    {
        mainCamera = this.GetComponent<Camera>();
        cameraMoveSpeed = 3;
        cameraZoomSpeed = 3f;
    }
}
