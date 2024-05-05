using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChopsInteraction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject main;
    public GameObject characterSelect;
    public GameObject arrows;
    public GameObject hands;
    public Vector3 originPosition;


    private void Start()
    {
        originPosition = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        // 拖拽时，筷子跟随鼠标，且到播放摇摆动画状态
        gameObject.GetComponent<Animator>().SetBool("isGrabbing", true);
        // 拖动过程中调用
        transform.position = eventData.position;
        // 显示箭头动画
        arrows.SetActive(true);


        // 如果在手上，则播放手部动画状态
        // 射线检测，检查拖动物体是否碰撞到了具有标签为“Hands”的对象
        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero);
        // if (hit.collider != null && hit.collider.CompareTag("Hands"))
        // {
        //     hands.GetComponent<Animator>().SetBool("isChopOver", true);
        // }
        // else
        // {
        //     hands.GetComponent<Animator>().SetBool("isChopOver", false);
        // }


    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // 隐藏箭头动画
        arrows.SetActive(false);

        if (hands.GetComponent<Animator>().GetBool("isChopOver"))
        {
            // 显示角色选择，隐藏主选单
            main.SetActive(false);
            characterSelect.SetActive(true);
            
            Initialize();
        }
        else
        {
            // 筷子归位
            transform.position = originPosition;
        }

        // 还原摇摆、手部动画到Idle，切换场景
        gameObject.GetComponent<Animator>().SetBool("isGrabbing", false);
        hands.GetComponent<Animator>().SetBool("isChopOver", false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider: " + other.gameObject.name);
        hands.GetComponent<Animator>().SetBool("isChopOver", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hands.GetComponent<Animator>().SetBool("isChopOver", false);
    }

    void Initialize()
    {
        // 初始化：筷子位置、筷子/手动画机状态
        transform.position = originPosition;
        gameObject.GetComponent<Animator>().SetBool("isGrabbing", false);
        hands.GetComponent<Animator>().SetBool("isChopOver", false);
    }
}
