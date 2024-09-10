using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float damage;
    public float Damage
    {
        get { return damage; }
    }
    [SerializeField]
    int per;
    public int Per
    {
        get { return per; }
    }

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
