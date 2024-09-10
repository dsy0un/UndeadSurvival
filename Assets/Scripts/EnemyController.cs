using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    float health;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    RuntimeAnimatorController[] rac;

    bool isLive = true;

    Rigidbody2D target;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.gameObject.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        PhysicsEnemyMove();
    }

    private void LateUpdate()
    {
        EnemyTurn();
    }

    /// <summary>
    /// 적이 플레이어를 따라오도록 만든 함수
    /// </summary>
    /// <returns></returns>
    void PhysicsEnemyMove()
    {
        if (!isLive) return;
        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero; // 물리 속도가 이동에 영향을 주지 않도록 함
    }

    /// <summary>
    /// 적이 고개를 돌리는 함수
    /// </summary>
    /// <returns></returns>
    void EnemyTurn()
    {
        if (!isLive) return;
        sr.flipX = target.position.x < rb.position.x;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = rac[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHealth = data.health;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().Damage;

        if (health > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
