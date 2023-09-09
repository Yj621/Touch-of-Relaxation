using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraMoveSpeed;
    private float cameraZoomSpeed;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    private Camera mainCamera;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        //손가락 하나가 눌렸을 때 -> 화면 이동
        if (Input.touchCount == 1)
        {
            CameraMove();
        }
       //손가락 두개가 눌렸을 때 -> 줌인 줌 아웃
       else if(Input.touchCount == 2)
        {
            CameraZoomInOut();
        }
    }

    //카메라 이동 기능
    private void CameraMove()
    {
        Touch touch = Input.GetTouch(0);
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
        cameraMoveSpeed = 1f;
        cameraZoomSpeed = 1f;
    }
}
