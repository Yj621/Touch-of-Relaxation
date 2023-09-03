using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageControl : MonoBehaviour
{
    public Sprite normalSprite;    // 클릭되지 않은 이미지
    public Sprite selectedSprite;  // 클릭된 이미지
    private Button button;
    public ImageControl[] allButtonHandlers; // 다른 버튼들을 저장할 배열

    private void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = normalSprite; // 초기에는 일반 이미지로 설정

        // 모든 ButtonControl 스크립트를 찾아 배열에 저장
        allButtonHandlers = FindObjectsOfType<ImageControl>();
    }

    public void OnButtonClick()
    {
        // 클릭된 이미지로 변경
        button.image.sprite = selectedSprite;

        // 다른 버튼들을 일반 이미지로 변경
        foreach (ImageControl handler in allButtonHandlers)
        {
            if (handler != this)
            {
                handler.button.image.sprite = handler.normalSprite;
            }
        }
    }
}