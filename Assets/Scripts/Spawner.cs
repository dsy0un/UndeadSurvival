using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;

    [SerializeField]
    int spawnCount = 3; // 적이 스폰될 숫자
    [SerializeField]
    float radius = 10; // 원의 반지름
    [SerializeField]
    int level; // 적의 데이터 변경 주기
    [SerializeField]
    float spawnInterval = 4; // 적의 스폰 주기

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        spawnCount = Mathf.Clamp(spawnCount, 3, 30);
        spawnInterval = Mathf.Clamp(spawnInterval, 0.2f, 4f);
        if (GameManager.Instance.GameTime % 10 == 0) level++;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (level >= spawnData.Length)
            {
                spawnInterval -= 0.1f;
                level = 0;
            }

            yield return new WaitForSeconds(spawnInterval); // 한 사이클이 돌면 0.1 감소 (적이 변경되는 시간(주기X)은 1분)

            float angleStep = 360f / spawnCount;
            float angle = 0f;

            for (int i = 0; i < spawnCount; i++)
            {
                float spawnX = transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                float spawnY = transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

                Vector2 enemyPos = new Vector2(spawnX, spawnY);

                GameObject enemy = GameManager.Instance.pool.Get(0);
                enemy.transform.position = enemyPos;
                enemy.GetComponent<EnemyController>().Init(spawnData[level]);

                angle += angleStep;
            }

            level++;
            spawnCount++;
        }
    }
}

[Serializable]
public class SpawnData
{
    public int spriteType;
    public int health;
    public float moveSpeed;
}