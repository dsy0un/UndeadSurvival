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

    }

    private void Update()
    {
        PlayerMove();
    }

    private void FixedUpdate()
    {
        PhysicsPlayerMove();
    }

    private void LateUpdate()
    {
        PlayerTurn();
    }

    /// <summary>
    /// 방향키를 누르면 -1부터 1까지 inputVec 변수에 추가되는 함수
    /// </summary>
    /// <returns></returns>
    void PlayerMove()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// 실제로 움직임이 구현되는 함수
    /// </summary>
    /// <returns>Null</returns>
    void PhysicsPlayerMove()
    {
        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    /// <summary>
    /// 플레이어의 고개를 돌리는 함수
    /// </summary>
    /// <returns></returns>
    void PlayerTurn()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) sr.flipX = inputVec.x < 0;
    }
}
