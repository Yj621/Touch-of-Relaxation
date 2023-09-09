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
        //�հ��� �ϳ��� ������ �� -> ȭ�� �̵�
        if (Input.touchCount == 1)
        {
            CameraMove();
        }
       //�հ��� �ΰ��� ������ �� -> ���� �� �ƿ�
       else if(Input.touchCount == 2)
        {
            CameraZoomInOut();
        }
    }

    //ī�޶� �̵� ���
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

    //ī�޶� ���� �ܾƿ� ���
    private void CameraZoomInOut()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touchPrevPos0 = touch0.position - touch0.deltaPosition; //touch0�� ���� ������ -> deltaposition�� �̵� ���� ����
        Vector2 touchPrevPos1 = touch1.position - touch1.deltaPosition;  //touch1�� ���� ������

        float prevTouchDistance = (touchPrevPos0 - touchPrevPos1).magnitude; //���� ��ġ ������ �Ÿ�
        float currentTouchDistance = (touch0.position - touch1.position).magnitude; // ���� ��ġ ������ �Ÿ�

        //�� �Ÿ��� ���̸� ����
        //touchDiff�� -�̸� ���� +�̸� �ܾƿ�
        float touchDiff = prevTouchDistance - currentTouchDistance;

        //�츮 ī�޶�� ���ٹ��� ����� Perspective����
        mainCamera.fieldOfView += touchDiff * cameraZoomSpeed;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 0.1f, 179.9f); //Mathf.Clamp�� ����� �ּ� �ִ� ������ ����
    }

    private void Init()
    {
        mainCamera = this.GetComponent<Camera>();
        cameraMoveSpeed = 1f;
        cameraZoomSpeed = 1f;
    }
}
