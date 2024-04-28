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

    public bool isDragMode;
    
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

            // 启动预制体运动协程
            StartCoroutine(MoveObjectAlongCustomPath(newObject));
            if(isDragMode)
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
                if(isDragMode)
                {
                    // 若在运动状态，则随着传送带路径移动
                    if(obj.GetComponent<DraggableFood>().isMoving)
                    {
                        obj.transform.position = newPosition;
                    }
                    
                }
                else
                {
                    // 若在运动状态，则随着传送带路径移动
                    if(obj.GetComponent<ClickableFood>().isMoving)
                    {
                        obj.transform.position = newPosition;
                    }
                }
                
                
                yield return null;
            }

            startTime = Time.time;
        }

        if(isDragMode)
        {
            // 移动完成后销毁预制体
            if(obj.GetComponent<DraggableFood>().canDestroy)
            {
                Destroy(obj);
                
            }

        }
        else
        {
            // 移动完成后销毁预制体
            if(obj.GetComponent<ClickableFood>().canDestroy)
            {
                
                Destroy(obj);
                
            }
        }
        
        
    }
}
