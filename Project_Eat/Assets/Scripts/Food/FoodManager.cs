using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Food;

public class FoodManager : MonoBehaviour
{
    public SideEffectManager SideEffectManager;
    public bool isEating;

    public void EatFoodList(List<string> foodKeyList)
    {
        isEating = true;

        //calculate the time and point from those food
        (float timeThisRound, float pointThisRound) = CalculateFoodList(foodKeyList);

        //wait for finishing the food
        StartCoroutine(SpendingTime(timeThisRound));

        GameManager.Instance.currentPoint += pointThisRound;

        List<SideEffectTypeEnum> effectThisRound = new List<SideEffectTypeEnum>();

        foreach (string foodKey in foodKeyList)
        {
            Food thisFood = GameManager.Instance.FoodDictionary[foodKey];

            foreach (SideEffectTypeEnum sideEffectTypeEnum in thisFood.SideEffectNameList)
            {
                effectThisRound.Add(sideEffectTypeEnum);
            }
        }

        foreach (SideEffectTypeEnum sideEffectTypeEnum in effectThisRound)
        {
            SideEffectManager.BuffEffect(sideEffectTypeEnum);
        }

        isEating = false;
    }

    public IEnumerator SpendingTime(float time)
    {
        yield return new WaitForSeconds(time);
    }


    //calculate the point and time with effect and speed
    public (float, float) CalculateFoodList(List<string> foodKeyList)
    {
        float timeThisRound = 0f;
        float pointThisRound = 0f;

        foreach (string foodKey in foodKeyList)
        {
            Food thisFood = GameManager.Instance.FoodDictionary[foodKey];

            (float foodTime, float foodPoint) = SideEffectManager.CalculateUnderEffect(thisFood);

            pointThisRound += thisFood.FoodPoint;
            timeThisRound += thisFood.FoodTime;
        }

        return (timeThisRound, pointThisRound);
    }

}
