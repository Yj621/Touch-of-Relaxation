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

    private UIController uiController; // UIController ��ũ��Ʈ

    private void Start()
    {
        Init();
        uiController = FindObjectOfType<UIController>();
    }

    void Update()
    {
        if (!uiController.isPanelOn && uiController.isMenuDown)
        {
            //�հ��� �ϳ��� ������ �� -> ȭ�� �̵�
            if (Input.touchCount == 1)
            {
                CameraMove();
            }
            //�հ��� �ΰ��� ������ �� -> ���� �� �ƿ�
            else if (Input.touchCount == 2)
            {
                CameraZoomInOut();
            }
        }
    }
    
    //�� �հ��� ��ġ
    private void CameraMove()
    {
        Touch touch = Input.GetTouch(0);//��ġ ���� �޾ƿ�

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


        //��ġ�� ������ ������ ȭ���� 0.05 ������ �������� ��
        if (Input.GetTouch(0).phase == TouchPhase.Ended && movePos.magnitude < 0.05f)
        {
            Vector2 touchPos = new Vector2(touch.position.x, touch.position.y); //��ġ ��ġ �޾ƿ�

            //����ĳ��Ʈ�� �̿��� ��ġ ������Ʈ �޾ƿ�
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            Physics.Raycast(ray, out hit);
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider != null && (hit.collider.tag == "TouchPossible" || hit.collider.tag == "ArriveTarget"))
            {
                GameObject CurrentTouch = hit.transform.gameObject;
                // ChangePanelController�� PopUpPanelTrue �Լ� ȣ��
                uiController.PopUpPanelTrue();
            }

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
        cameraMoveSpeed = 3;
        cameraZoomSpeed = 3f;
    }
}
