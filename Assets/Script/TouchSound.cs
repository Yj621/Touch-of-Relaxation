using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSound : MonoBehaviour
{
    public AudioClip touchSound; // ��ġ ȿ������ ���� AudioClip ����
    private AudioSource audioSource; // ȿ������ ����� AudioSource ������Ʈ

    void Start()
    {
        // AudioSource ������Ʈ�� ������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource�� ���� ��� ��ũ��Ʈ�� ������ ���ӿ�����Ʈ�� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // ȿ������ �ε��Ͽ� AudioClip�� �Ҵ�
        audioSource.clip = touchSound;
    }

    void Update()
    {
        // ȭ���� ��ġ�Ǿ��� ��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // ȿ������ ���
            if (touchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(touchSound);
            }
        }
    }
}