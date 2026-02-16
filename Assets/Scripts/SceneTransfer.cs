using UnityEngine;

public class SceneTransfer : MonoBehaviour
{
    public string targetSceneName;  // 次のシーン名
    public string targetSpawnPoint; // 【追加】次のシーンのどこに出るか（目印の名前）

    // staticをつけることで、シーンをまたいでデータを共有できる！
    public static string nextSpawnPointName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. 次の行き先を「予約」しておく
            nextSpawnPointName = targetSpawnPoint;

            // 2. フェードアウトしてシーン遷移（昨日のスクリプト）
            LevelLoader.instance.LoadNextLevel(targetSceneName);
        }
    }
}