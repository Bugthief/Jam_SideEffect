using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Food;

public class FoodManager : MonoBehaviour
{
    public SideEffectManager SideEffectManager;
    public GeneralInfo GeneralInfo;

    public event Action<bool> OnEatingStatusChanged;
    private bool isEating;

    public EatingPlate eatingPlate;

    public bool IsEating
    {
        get { return isEating; }
        set
        {
            isEating = value;

            OnEatingStatusChanged?.Invoke(isEating);
        }
    }

    public void EatFoodList(List<string> foodKeyList)
    {
        IsEating = true;

        // Calculate the time and point from those food
        (float timeThisRound, float pointThisRound) = CalculateFoodList(foodKeyList);

        eatingPlate.eatingTime = timeThisRound;

        // Start the SpendingTime coroutine and execute the remaining code after the coroutine completes
        StartCoroutine(SpendingTime(timeThisRound, () =>
        {
            // This code will be executed after the SpendingTime coroutine completes
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
                Debug.Log(sideEffectTypeEnum.ToString());
            }

            GeneralInfo.UpdatePointText(GameManager.Instance.currentPoint);

            IsEating = false;
        }));
    }

    public IEnumerator SpendingTime(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);

        // Call the onComplete action when the coroutine completes
        onComplete?.Invoke();
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

            pointThisRound += foodPoint;
            timeThisRound += foodTime;
        }

        return (timeThisRound, pointThisRound);
    }

}
