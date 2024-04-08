using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSound : MonoBehaviour
{
    public AudioClip touchSound; // 터치 효과음을 담을 AudioClip 변수
    private AudioSource audioSource; // 효과음을 재생할 AudioSource 컴포넌트

    void Start()
    {
        // AudioSource 컴포넌트를 가져옴
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource가 없을 경우 스크립트가 부착된 게임오브젝트에 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // 효과음을 로드하여 AudioClip에 할당
        audioSource.clip = touchSound;
    }

    void Update()
    {
        // 화면이 터치되었을 때
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 효과음을 재생
            if (touchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(touchSound);
            }
        }
    }
}