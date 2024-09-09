using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.pool.Get(0);
            }
        }
    }
}
