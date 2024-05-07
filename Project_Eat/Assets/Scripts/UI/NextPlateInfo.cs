using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Food;

public class NextPlateInfo : MonoBehaviour
{
    public TMP_Text textBox;

    public List<string> eatingFoodList = new List<string>();

    bool IsFirst = false;

    public void GenerateNextPlateInfo(List<string> eatingFoodList)
    {
        IsFirst = false;

        string foodNames = null;
        float originalPoint = 0;
        float originalTime = 0;
        List<SideEffectTypeEnum> sideEffectThisPlate = new List<SideEffectTypeEnum>();

        foreach (string foodKey in eatingFoodList)
        {
            Food food = GameManager.Instance.FoodDictionary[foodKey];

            if (!IsFirst)
            {
                foodNames += food.FoodName + ", ";
                IsFirst = true;
            }
            else
            {
                foodNames += food.FoodName;
            }
            
            originalPoint += food.FoodPoint;
            originalTime += food.FoodTime;
            foreach (SideEffectTypeEnum sideEffectTypeEnum in food.SideEffectNameList)
            {
                if (!sideEffectThisPlate.Contains(sideEffectTypeEnum)) 
                { 
                    sideEffectThisPlate.Add(sideEffectTypeEnum); 
                }
            }
        }

        FoodManager foodManager = FindAnyObjectByType<FoodManager>();
        (float realTime, float realPoint) = foodManager.CalculateFoodList(eatingFoodList);

        string pointText = CompareAndDisplay(originalPoint, realPoint, "分数");
        string timeText = CompareAndDisplay(originalTime, realTime, "时间");

        string sideEffects = null;

        int count = sideEffectThisPlate.Count;
        for (int i = 0; i < count; i++)
        {
            SideEffectTypeEnum sideEffectTypeEnum = sideEffectThisPlate[i];

            sideEffects += GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum].SideEffectName;

            if (i < count - 1)
            {
                sideEffects += "，";
            }
        }

        string fullText = null;
        fullText += "食物名称: " + foodNames + "\n" +
            pointText + "\n" +
            timeText + "\n";

        if (count != 0)
        {
            fullText += "副作用: " + sideEffects;
        }

        textBox.text = fullText;

    }

    string CompareAndDisplay(float originalValue, float realValue, string valueType)
    {
        float difference = realValue - originalValue;
        string comparison = difference >= 0 ? "+" : "-";
        difference = Mathf.Abs(difference);

        string displayText = valueType + ": " + originalValue + " " + comparison + " " + difference;
        return displayText;
    }

    public void FinishEatingInfo()
    {
        textBox.text = "暂无";
    }
}
