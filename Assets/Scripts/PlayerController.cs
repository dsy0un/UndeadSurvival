using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 inputVec;
    [SerializeField]
    float moveSpeed;

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
    }

    private void FixedUpdate()
    {
        StartCoroutine(PhysicsPlayerMove());
    }

    private void LateUpdate()
    {
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
        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);

        yield return null;
    }

    /// <summary>
    /// �÷��̾��� ���� ������ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerTurn()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) sr.flipX = inputVec.x < 0;

        yield return null;
    }
}
