using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public GameObject endingPanel;
    public Sprite fullStar;
    public Sprite emptyStar;
    public Sprite successLogo;
    public Sprite failLogo;

    public Sprite star1;
    public Sprite star2;
    public Sprite star3;
    public Sprite logo;

    public TimeManager timeManager;

    public void GameSucceeded()
    {   
        timeManager.isCoutingDown = false;
    }

    public void TimeOutFailure()
    {
        timeManager.isCoutingDown = false;
    }
}
