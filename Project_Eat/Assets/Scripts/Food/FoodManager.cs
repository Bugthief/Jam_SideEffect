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


    
    // 在吃食物parentbeltParent
    public GameObject parent_Eating;
    // 下一盘要吃食物parent
    public GameObject parent_Waiting;

    public GameObject plate_Eating;
    public GameObject plate_Waiting;

    public float timeByEffect;

    private void Update()
    {
        setFoodParent();
    }


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
        timeByEffect = 0f;

        // Calculate the time and point from those food
        (float timeThisRound, float pointThisRound) = CalculateFoodList(foodKeyList);

        eatingPlate.eatingTime = timeThisRound;

        // Start the SpendingTime coroutine and execute the remaining code after the coroutine completes
        StartCoroutine(SpendingTime(timeThisRound, () =>
        {
            // This code will be executed after the SpendingTime coroutine completes
            GameManager.Instance.currentPoint += pointThisRound;

            if(GameManager.Instance.currentPoint >= GameManager.Instance.maxPoint)
            {
                EndManager endManager = FindObjectOfType<EndManager>();
                if(endManager != null)
                {
                    endManager.GameSucceeded();
                }
            }

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

            GeneralInfo.UpdatePointImage(GameManager.Instance.currentPoint);

            StartCoroutine(SpedingMoreTime(timeByEffect));
        }));
    }

    public IEnumerator SpendingTime(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);

        // Call the onComplete action when the coroutine completes
        onComplete?.Invoke();
    }

    public IEnumerator SpedingMoreTime(float time)
    {
        yield return new WaitForSeconds(time);
        IsEating = false;
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

    void setFoodParent()
    {
        GameObject eatingFood_1 = plate_Eating.GetComponent<EatingPlate>().eating_1;
        GameObject eatingFood_2 = plate_Eating.GetComponent<EatingPlate>().eating_2;

        GameObject waitingFood_1 = plate_Waiting.GetComponent<PlateManager>().food_1;
        GameObject waitingFood_2 = plate_Waiting.GetComponent<PlateManager>().food_2;

        // 食物状态：①在传送带上 ②在吃 ③下一盘要吃
        if (eatingFood_1 != null && eatingFood_2 != null)
        {
            // 在吃的
            eatingFood_1.transform.parent = parent_Eating.transform;
            eatingFood_2.transform.parent = parent_Eating.transform;
            //print("go in Eating!!!!!!");
        }
        
        if (waitingFood_1 != null)
        {
            // 下一盘要吃的
            waitingFood_1.transform.parent = parent_Waiting.transform;
            
            //print("1 go in Waiting!!!!!!");

        }

        if (waitingFood_2 != null)
        {
            // 下一盘要吃的
            waitingFood_2.transform.parent = parent_Waiting.transform;
            
            //print("2 go in Waiting!!!!!!");

        }
    }

}
