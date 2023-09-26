// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class SliderValueController : MonoBehaviour
// {
//     public Slider slider;
//     public Button buttonDecrease;
//     public Button buttonIncrease;
//     public Text textAmountOfGoods;
//     public Text coinText; // coin_text UI를 연결해줄 변수
//     public Text diaText; 
//     private UIController uiController;
//     public int currentEnergy = 0;
//     public GameObject warningWindow;
//     public GameObject windowTitle;


//     private void Start()
//     {
//         Debug.Log("currentEnergy: " + currentEnergy);
//         slider.value = 0;
//         uiController = FindObjectOfType<UIController>();
//         warningWindow.SetActive(false);
//     }

//     private void DecreaseSliderValue()
//     {
//         slider.value -= 10;
//         UpdateTextAmountOfGoods();
//     }

//     private void IncreaseSliderValue()
//     {
//         // Slider의 현재 값에서 10 증가
//         slider.value += 10;
//         UpdateTextAmountOfGoods();
//     }

//     private void UpdateTextAmountOfGoods()
//     {
//         // Slider의 값을 Text UI에 표시
//         textAmountOfGoods.text = slider.value.ToString();
//     }

//     // 현재 energy 값을 설정하는 메서드
//     public void SetCurrentEnergy(int energyValue)
//     {
//         currentEnergy = energyValue;
//         slider.maxValue = currentEnergy; // Slider의 maxValue를 현재 energy 값으로 설정
//         slider.value = 0; // Slider의 값을 초기화
//         UpdateTextAmountOfGoods();
//     }

//     //coin_text UI 값을 업데이트
//     public void UpdateCoinText(int amount)
//     {
//         if (currentEnergy >= amount)
//         {
//             // 현재 energy가 충분하면 coin_text UI 값을 업데이트하고 energy 차감
//             coinText.text = (int.Parse(coinText.text) + amount).ToString();
//             currentEnergy -= amount;
//             // energy UI Text를 업데이트
//             uiController.textEnergy.text = currentEnergy.ToString();
//             Debug.Log("골드로 교환 완료");
//         }
//         else
//         {
//             warningWindow.SetActive(true);
//             Debug.Log("Energy가 부족합니다.");
//         }
//     }

//     //dia_text UI 값을 업데이트
//     public void UpdateDiaText(int amount)
//     {
//         if (currentEnergy >= amount)
//         {
//             // 현재 energy가 충분하면 dia_text UI 값을 업데이트하고 energy 차감
//             diaText.text = (int.Parse(diaText.text) + amount).ToString();
//             currentEnergy -= amount;
//             // energy UI Text를 업데이트
//             uiController.textEnergy.text = currentEnergy.ToString();
//             Debug.Log("다이아로로 교환 완료");
//         }
//         else
//         {
//             warningWindow.SetActive(true);
//             Debug.Log("Energy가 부족합니다.");
//         }
//     }
//     // 확인 버튼 클릭
//     public void ConfirmButtonClick()
//     {
//         int amount = (int)slider.value; // Slider의 값을 정수로 변환
//         if(uiController.isGoldButtonClicked == true)
//         {
//             UpdateCoinText(amount);
//         }
//         else
//         {
//             UpdateDiaText(amount);
//         }
//     }
//     //초기화 버튼 클릭
//     public void CancleButtonClick()
//     {
//         textAmountOfGoods.text = "0";
        
//         // 'slider.value'를 0으로 초기화
//         slider.value = 0;
//     }

//     public void WarningWindowClose()
//     {
//         warningWindow.SetActive(false);
//     }

// }
