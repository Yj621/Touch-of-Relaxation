using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject[] cameras;
    private int activeCameraIndex = -1; // 현재 활성화된 카메라 인덱스

    void Awake()
    {
        // 모든 카메라를 비활성화
        foreach (GameObject camera in cameras)
        {
            camera.SetActive(false);
        }
    }

    public void ForestCam()
    {
        Debug.Log("Forest");
        // 이미 활성화된 카메라를 비활성화
        DeactivateCurrentCamera();

        // 원하는 카메라를 활성화
        ActivateCamera(0);
    }

    public void VactionCam()
    {
        Debug.Log("VactionCam");
        // 이미 활성화된 카메라를 비활성화
        DeactivateCurrentCamera();

        ActivateCamera(1);
    }

    public void CityCam()
    {
        Debug.Log("CityCam");
        // 이미 활성화된 카메라를 비활성화
        DeactivateCurrentCamera();

        ActivateCamera(2);
    }

    public void VillageCam()
    {
        Debug.Log("VillageCam");
        // 이미 활성화된 카메라를 비활성화
        DeactivateCurrentCamera();

        ActivateCamera(3);
    }

    // 카메라를 활성화하는 함수
    private void ActivateCamera(int index)
    {
        // 해당 인덱스의 카메라를 활성화
        if (index >= 0 && index < cameras.Length)
        {
            cameras[index].SetActive(true);
            activeCameraIndex = index; // 활성화된 카메라 인덱스 업데이트
        }
    }

    // 현재 활성화된 카메라를 비활성화하는 함수
    private void DeactivateCurrentCamera()
    {
        if (activeCameraIndex >= 0 && activeCameraIndex < cameras.Length)
        {
            cameras[activeCameraIndex].SetActive(false);
        }
    }
}
