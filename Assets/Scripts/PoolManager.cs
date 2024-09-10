using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs;

    [HideInInspector]
    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        { 
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int i)
    {
        GameObject select = null;

        // ������ Ǯ �ȿ��� ��Ȱ��ȭ�Ǿ� �ִ� GameObject �� �ϳ��� ������

        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� ã���� ���Ӱ� �����ϰ� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }
}
