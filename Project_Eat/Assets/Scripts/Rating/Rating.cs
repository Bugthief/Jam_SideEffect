using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 显示得分相关UI、重置得分
public class Rating : MonoBehaviour
{

    public bool isGameOver;
    public GameObject[] star_imgs;
    public GameObject endingUI;

    public int rating_starNum;


    void Start()
    {
        ResetRating();
    }

    void Update()
    {
        if (isGameOver)
        {
            showEndingUI();
        }
        else
        {
            hideEndingUI();
        }
    }

    public void showEndingUI()
    {
        endingUI.SetActive(true);

        // 获取对应得分，并显示星星
        // if (star_imgs[rating_starNum] != null)
        //     star_imgs[rating_starNum].SetActive(true);


        for (int i = 0; i < star_imgs.Length; i++)
        {
            //GameObject toShowImg = star_imgs[i];

            if (i == rating_starNum)
            {
                star_imgs[i].SetActive(true);
                print("activated: " + star_imgs[rating_starNum].name);
            }
            else
            {
                star_imgs[i].SetActive(false);
                print("DEactivated: " + star_imgs[rating_starNum].name);
            }


        }

    }

    public void hideEndingUI()
    {
        isGameOver = false;
        endingUI.SetActive(false);
        ResetRating();
    }

    public void ResetRating()
    {
        // 重置得分星星的显示
        foreach (GameObject star_img in star_imgs)
        {
            if (star_img != null)
                star_img.SetActive(false);
        }
    }

}
