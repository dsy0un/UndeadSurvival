using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    bool isLive = true;

    Rigidbody2D target;
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        target = GameManager.Instance.player.gameObject.GetComponent<Rigidbody2D>();

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
}
