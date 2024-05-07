using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomPathMovement : MonoBehaviour
{
    public GameObject prefabToSpawn; // 预制体
    public List<Transform> pathPoints; // 关键路径点的 Transform
    public float spawnInterval = 1f; // 生成间隔
    public float movementDuration = 5f; // 运动持续时间

    public bool isDragMode;// 是否是拖拽模式（拖拽/点击以移动食物）
    public bool randomColorForFood = false;

    private void Start()
    {
        // 启动生成预制体协程
        StartCoroutine(SpawnPrefabRoutine());
    }

    IEnumerator SpawnPrefabRoutine()
    {
        while (true)
        {
            // 在起点生成预制体
            GameObject newObject = Instantiate(prefabToSpawn, pathPoints[0].position, Quaternion.identity);

            if (randomColorForFood)
            {
                // 为每个食物生成一个随机颜色
                newObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            }

            // 启动预制体运动协程
            StartCoroutine(MoveObjectAlongCustomPath(newObject));
            if (isDragMode)
            {
                newObject.GetComponent<DraggableFood>().isMoving = true;
            }
            else
            {
                newObject.GetComponent<ClickableFood>().isMoving = true;
            }


            // 等待生成间隔
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveObjectAlongCustomPath(GameObject obj)
    {
        float startTime = Time.time;

        for (int i = 1; i < pathPoints.Count; i++)
        {
            Vector3 startPos = pathPoints[i - 1].position;


            Vector3 endPos = pathPoints[i].position;

            while (Time.time - startTime < movementDuration)
            {
                float t = (Time.time - startTime) / movementDuration;
                Vector3 newPosition = Vector3.Lerp(startPos, endPos, t);

                // 拖拽还是点击模式
                if (isDragMode)
                {
                    // 若在运动状态，则随着传送带路径移动
                    if (obj != null && obj.GetComponent<DraggableFood>().isMoving)
                    {
                        obj.transform.position = newPosition;
                    }

                }
                else
                {
                    // 若在运动状态，则随着传送带路径移动
                    if (obj != null && obj.GetComponent<ClickableFood>().isMoving)
                    {
                        obj.transform.position = newPosition;
                    }
                }


                yield return null;
            }

            startTime = Time.time;
        }

        if (isDragMode)
        {
            // 移动完成后销毁预制体
            if (obj != null && obj.GetComponent<DraggableFood>().canDestroy)
            {
                // 如果在拖拽状态，获得虚影对象，销毁
                Debug.Log("destroyed");
                DestroyDraggingGhost();

                // 销毁食物
                Destroy(obj);

            }

        }
        else
        {
            // 移动完成后销毁预制体
            if (obj != null && obj.GetComponent<ClickableFood>().canDestroy)
            {

                Destroy(obj);

            }
        }


    }

    // 检测正在拖拽的是否是ghost，如果是，且满足条件，则摧毁
    void DestroyDraggingGhost()
    {
        // 检查鼠标左键是否被按下
        if (Input.GetMouseButton(0))
        {
            // 获取鼠标位置
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 检查是否有物体被拖拽
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                // 检查标签是否为"ghost"并且 isDragging 是否为 true
                if (hit.collider.gameObject.CompareTag("ghost") && hit.collider.gameObject.GetComponent<DraggableFood>().isDragging)
                {
                    // 摧毁对象
                    Destroy(hit.collider.gameObject);

                    return;
                }
            }
        }
    }
}
