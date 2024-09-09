using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector2 playerPos = GameManager.Instance.player.transform.position;
        Vector2 mapPos = transform.position;

        float dirX = playerPos.x - mapPos.x;
        float dirY = playerPos.y - mapPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY) transform.Translate(Vector2.right * dirX * 40);
                else if (diffX < diffY) transform.Translate(Vector2.up * dirY * 40);
                break;
            case "Enemy":
                if (!col.enabled) return;

                if (diffX > diffY) transform.Translate(Vector2.right * dirX * 20 + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
                else if (diffX < diffY) transform.Translate(Vector2.up * dirY * 20 + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
                break;
        }
    }
}
