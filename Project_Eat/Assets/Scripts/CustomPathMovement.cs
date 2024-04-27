using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPathMovement : MonoBehaviour
{
    public GameObject prefabToSpawn; // 预制体
    public List<Transform> pathPoints; // 关键路径点的 Transform
    public float spawnInterval = 1f; // 生成间隔
    public float movementDuration = 5f; // 运动持续时间
    
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
                obj.transform.position = newPosition;
                yield return null;
            }

            startTime = Time.time;
        }

        // 移动完成后销毁预制体
        Destroy(obj);
    }
}
