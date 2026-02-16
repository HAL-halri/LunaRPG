using UnityEngine;

public class CameraSorter : MonoBehaviour
{
    void Start()
    {
        // カメラの設定を強制的に「Y軸（高さ）で描画順を決める」モードに変更する
        Camera cam = GetComponent<Camera>();
        cam.transparencySortMode = TransparencySortMode.CustomAxis;
        cam.transparencySortAxis = new Vector3(0, 1, 0);
    }
}