using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Vector2 inputVec;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(PlayerMove());
        StartCoroutine(PhysicsPlayerMove());
        StartCoroutine(PlayerTurn());
    }

    /// <summary>
    /// ����Ű�� ������ -1���� 1���� inputVec ������ �߰��Ǵ� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerMove()
    {
        while (true)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            yield return null;
        }
    }

    /// <summary>
    /// ������ �������� �����Ǵ� �Լ�
    /// </summary>
    /// <returns>Null</returns>
    IEnumerator PhysicsPlayerMove()
    {
        while (true)
        {
            Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + nextVec);

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// �÷��̾��� ���� ������ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerTurn()
    {
        while (true)
        {
            anim.SetFloat("Speed", inputVec.magnitude);

            if (inputVec.x != 0) sr.flipX = inputVec.x < 0;

            yield return new WaitForEndOfFrame();
        }
    }
}
