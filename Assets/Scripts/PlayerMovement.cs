using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 移動スピード（Unity画面で調整可能）
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // 自分のRigidbody2Dコンポーネントを取得して変数に入れる
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (GameManager.instance.isDialogueActive == true)
        {
            rb.velocity = Vector2.zero;
        }
        // 入力チェック（WASDキー）
        // GetAxisRaw は -1, 0, 1 しか返さないので、慣性がつかない
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isDialogueActive == true) return;
        // 物理演算を使って移動させる
        // 入力方向にスピードを掛けて、直接速度(velocity)を書き換える
        rb.velocity = movement.normalized * moveSpeed;
    }
}