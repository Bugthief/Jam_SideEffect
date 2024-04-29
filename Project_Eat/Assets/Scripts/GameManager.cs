using System.Collections.Generic;
using UnityEngine;
using static Food;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float currentTime;
    public float maxTime;

    public float currentPoint;
    public float maxPoint;

    public float pointA;
    public float speedA;

    public Dictionary<string, Food> FoodDictionary;
    public Dictionary<SideEffectTypeEnum, SideEffect> SideEffectDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FoodDictionary = FoodLoader.BuildFoodDictionary();
        SideEffectDictionary = SideEffectLoader.BuildSideEffectDictionary();
    }

    private void Start()
    {
        
    }
}
