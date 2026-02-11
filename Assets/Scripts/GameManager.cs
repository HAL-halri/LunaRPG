using UnityEngine;
using System.Collections.Generic; // ★リストを使うために必要！

// ★鍵の種類リスト（これを定義するとプルダウンで選べるようになる）
public enum KeyType
{
    None,       // なし
    LabKey1,     // 研究室の鍵
    LibraryKey, // 図書館の鍵
    RoofKey     // 屋上の鍵
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDialogueActive = false;

    // ★ hasKey変数の代わりに、「持っている鍵のリスト」を作る
    public List<KeyType> currentKeys = new List<KeyType>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ★鍵を追加する便利機能
    public void AddKey(KeyType key)
    {
        if (!currentKeys.Contains(key)) // 重複防止
        {
            currentKeys.Add(key);
        }
    }

    // ★鍵を持っているかチェックする便利機能
    public bool HasKey(KeyType key)
    {
        return currentKeys.Contains(key);
    }
}