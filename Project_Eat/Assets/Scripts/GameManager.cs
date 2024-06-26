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
    public float pointB;
    public float timeA;
    public float timeB;

    public bool isWinning = false;

    public Dictionary<string, Food> FoodDictionary;
    public Dictionary<SideEffectTypeEnum, SideEffect> SideEffectDictionary;

    public TimeManager timeManager;

    public List<string> FoodKeyList;

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
        FoodKeyList = GenerateFoodKeyList(FoodDictionary);
    }

    private void Start()
    {
        //Debug.Log("Hi");
        timeManager.StartCountDown(0f, maxTime);

    }

    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    public List<string> GenerateFoodKeyList(Dictionary<string, Food> dictionary)
    {
        List<string> list = new List<string>();

        foreach (string key in dictionary.Keys)
        {
            list.Add(key);
        }

        return list;
    }
}
