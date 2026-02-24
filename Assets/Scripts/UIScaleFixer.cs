using UnityEngine;

public class UIScaleFixer : MonoBehaviour
{
    // ここに「研究日誌の時の理想のサイズ」を入れる
    // World Space Canvasの場合、0.01 とか 0.005 とか小さい数字のことが多いです
    public Vector3 targetWorldScale = new Vector3(0.01f, 0.01f, 1f);

    void LateUpdate()
    {
        // 親オブジェクトがいる場合だけ計算する
        if (transform.parent != null)
        {
            Vector3 parentScale = transform.parent.lossyScale;

            // 0で割るエラーを防ぐ
            if (parentScale.x == 0 || parentScale.y == 0 || parentScale.z == 0) return;

            // 「親の大きさ」を打ち消すように、自分のローカルサイズを逆算する
            // 目標の大きさ ÷ 親の大きさ ＝ 設定すべき自分の大きさ
            transform.localScale = new Vector3(
                targetWorldScale.x / parentScale.x,
                targetWorldScale.y / parentScale.y,
                targetWorldScale.z / parentScale.z
            );
        }
    }
}