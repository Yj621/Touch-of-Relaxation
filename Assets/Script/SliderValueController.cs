using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueController : MonoBehaviour
{
    public Slider slider;
    public Button buttonDecrease;
    public Button buttonIncrease;
    public Text textAmountOfGoods;

    private void Start()
    {
        slider.value = 0;
        // if (slider != null && buttonDecrease != null && buttonIncrease != null && textAmountOfGoods != null)
        // {
            // buttonDecrease.onClick.AddListener(DecreaseSliderValue);
            // buttonIncrease.onClick.AddListener(IncreaseSliderValue);
        // }
        // else
        // {
        //     Debug.LogError("UI elements not assigned in the Inspector!");
        // }

        // 초기에도 Slider의 값을 Text에 표시
        // UpdateTextAmountOfGoods();
    }

    private void DecreaseSliderValue()
    {
        // Slider의 현재 값에서 10 감소하되 최소값 제한
        //slider.value = Mathf.Max(slider.value - 10, slider.minValue);
        slider.value -= 10;
        UpdateTextAmountOfGoods();
    }

    private void IncreaseSliderValue()
    {
        // Slider의 현재 값에서 10 증가
        slider.value+=10;
        UpdateTextAmountOfGoods();
    }

    private void UpdateTextAmountOfGoods()
    {
        // Slider의 값을 Text UI에 표시
        textAmountOfGoods.text = slider.value.ToString();
    }
}
