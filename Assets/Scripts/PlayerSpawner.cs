using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        // 1. 予約された名前を確認
        string targetName = SceneTransfer.nextSpawnPointName;

        // コンソールに表示！（ここが大事）
        Debug.Log("① 命令された場所の名前: " + targetName);

        if (!string.IsNullOrEmpty(targetName))
        {
            // 2. その名前のオブジェクトを探す
            GameObject spawnPoint = GameObject.Find(targetName);

            if (spawnPoint != null)
            {
                Debug.Log("② 発見！ " + targetName + " の位置にワープします！");
                transform.position = spawnPoint.transform.position;
            }
            else
            {
                // ここでエラーが出たら、名前が間違っている証拠！
                Debug.LogError("③ 失敗！ シーンの中に '" + targetName + "' という名前のオブジェクトが見つかりません！");
                Debug.LogError("ヒント：ヒエラルキーにあるオブジェクトの名前を、一字一句コピーしてドアの設定に貼り付けてください。");
            }
        }
        else
        {
            Debug.Log("④ 命令なし：通常の初期位置から開始します（初回起動など）");
        }
    }
}