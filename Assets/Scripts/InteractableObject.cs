using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    // 種類を選べるようにする（普通、鍵、扉）
    public enum ObjectType { Info, KeyItem, LockedDoor }
    [Header("種類の設定")]
    public ObjectType objectType = ObjectType.Info;
    [Header("鍵の設定（鍵アイテム または 扉の場合）")]
    public KeyType targetKey = KeyType.None; // ★ここで種類を選ぶ
    [Header("設定")]
    public string objectName = "Object";
    [TextArea(3, 10)]
    public string[] sentences; // 文章リスト

    [Header("UIの割り当て")]
    public GameObject promptUI;
    public GameObject messageWindow;
    public TMP_Text dialogueText;
    public TMP_Text nameText;

    private bool isPlayerClose = false;
    private int index = 0;
    private bool isTalking = false;

    // ★扉用の特別な変数（ドアの絵を変えたい時用）
    private SpriteRenderer mySprite;

    void Start()
    {
        promptUI.SetActive(false);
        messageWindow.SetActive(false);
        mySprite = GetComponent<SpriteRenderer>(); // 自分の絵を取得
    }

    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.F))
        {
            if (!isTalking)
            {
                CheckAndStart(); // ★会話を始める前にチェックを入れる
            }
            else
            {
                NextSentence();
            }
        }
    }

    // ★状況をチェックして会話を始める
    void CheckAndStart()
    {
        if (objectType == ObjectType.LockedDoor)
        {
            // ★修正：指定された種類の鍵を持っているかチェック
            if (GameManager.instance.HasKey(targetKey) == false)
            {
                StartDialogue(); // 「鍵がかかっている」へ
                return;
            }
        }
        StartDialogue();
    }

    void StartDialogue()
    {
        GameManager.instance.isDialogueActive = true;
        isTalking = true;
        promptUI.SetActive(false);
        messageWindow.SetActive(true);
        if (nameText != null) nameText.text = objectName;

        index = 0;

        // ★扉の場合のメッセージ切り替え
        if (objectType == ObjectType.LockedDoor)
        {
            dialogueText.text = sentences[0];
            if(GameManager.instance.HasKey(targetKey))
            {
                // 鍵がある時：2行目を読む ("鍵を開けた！")
                // ※sentences[1]がなければ0を読む
                if (sentences.Length > 1) dialogueText.text = sentences[1];
                else dialogueText.text = sentences[0];
            }
        }
        else
        {
            // 普通の会話やアイテム
            dialogueText.text = sentences[index];
        }
    }

    void NextSentence()
    {
        // ★扉で鍵がない場合は、長話させずに即終了
        if (objectType == ObjectType.LockedDoor && GameManager.instance.HasKey(targetKey) == false)
        {
            EndDialogue();
            return;
        }

        index++;
        if (index < sentences.Length)
        {
            dialogueText.text = sentences[index];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        GameManager.instance.isDialogueActive = false;
        isTalking = false;
        if (messageWindow != null) messageWindow.SetActive(false);
        if (promptUI != null) promptUI.SetActive(true);


        if (objectType == ObjectType.KeyItem)
        {
            // ★修正：指定された種類の鍵を「鍵束」に追加する
            GameManager.instance.AddKey(targetKey);

            Debug.Log(targetKey + " を入手！");
            gameObject.SetActive(false);
        }
        else if (objectType == ObjectType.LockedDoor)
        {
            // ★修正：指定された種類の鍵を持っている場合のみ開く
            if (GameManager.instance.HasKey(targetKey) == true)
            {
                Debug.Log("扉が開いた！");
                gameObject.SetActive(false);
            }
        }
    }

    void CancelDialogue()
    {
        isTalking = false;
        if (messageWindow != null) messageWindow.SetActive(false);
        if (promptUI != null) promptUI.SetActive(false); // 離れたら[F]も消す

        GameManager.instance.isDialogueActive = false; // 動けるようにする

        // ★ここには「アイテムを消す処理」を書かない！
        // ただウィンドウを閉じるだけ。
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = true;
            if (!isTalking) promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = false;

            // ★修正！ EndDialogue ではなく CancelDialogue を呼ぶ
            CancelDialogue();
        }
    }
}