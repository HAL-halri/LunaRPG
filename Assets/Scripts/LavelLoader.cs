using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; // アニメーターを入れる箱
    public float transitionTime = 1f; // 1秒かける

    // どこからでも呼べるようにする（シングルトン）
    public static LevelLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 次のシーンへ行く命令
    public void LoadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    // コルーチン（時間差処理）
    IEnumerator LoadLevel(string levelName)
    {
        // 1. アニメーション再生（暗くする）
        transition.SetTrigger("Start");

        // 2. 暗くなるまで待つ
        yield return new WaitForSeconds(transitionTime);

        // 3. シーン切り替え
        SceneManager.LoadScene(levelName);
    }
}