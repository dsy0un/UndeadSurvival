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
        if (isLive) 
        {
            StartCoroutine(PhysicsEnemyMove());
            StartCoroutine(EnemyTurn());
        }
    }

    /// <summary>
    /// ���� �÷��̾ ��������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator PhysicsEnemyMove()
    {
        while (true)
        {
            Vector2 dirVec = target.position - rb.position;
            Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + nextVec);
            rb.velocity = Vector2.zero; // ���� �ӵ��� �̵��� ������ ���� �ʵ��� ��

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// ���� ���� ������ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyTurn()
    {
        while (true)
        {
            sr.flipX = target.position.x < rb.position.x;

            yield return new WaitForEndOfFrame();
        }
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = rac[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHealth = data.health;
        health = maxHealth;
    }
}
