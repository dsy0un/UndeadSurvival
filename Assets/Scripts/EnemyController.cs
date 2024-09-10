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
    /// ���� �÷��̾ ��������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    void PhysicsEnemyMove()
    {
        if (!isLive) return;
        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero; // ���� �ӵ��� �̵��� ������ ���� �ʵ��� ��
    }

    /// <summary>
    /// ���� ���� ������ �Լ�
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
