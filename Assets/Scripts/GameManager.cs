using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public PoolManager pool;

    float gameTime;
    public float GameTime
    {
        get { return gameTime; }
    }

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        pool = FindObjectOfType(typeof(PoolManager)) as PoolManager;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }
}
