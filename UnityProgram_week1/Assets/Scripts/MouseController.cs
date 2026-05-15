using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 20.0f;    //マウス感度
    [SerializeField] private Transform cam;                     //カメラ
    private float xRotation = 0.0f;                             //上下の回転角
    private Vector2 lookInput;                                  //マウスの移動量
    private void Start()
    {
        //マウスカーソルを画面中央に固定し、非表示にする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseLook();
    }

    /// <summary>
    /// マウスの入力アクションを作成
    /// </summary>
    /// <param name="value"></param>
    private void OnLook(InputValue value)
    {
        //マウスの移動量を取得
        lookInput = value.Get<Vector2>();  
    }

    /// <summary>
    /// 視点移動の計算と処理
    /// </summary>
    private void HandleMouseLook()
    {
        //マウス感度がフレームレートに依存しないようにする
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        //上下回転
        xRotation -= mouseY;
        //上下の首の角度を90度で制限
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        //上下回転(カメラだけを上下に回転)
        cam.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

        //左右回転(プレイヤーの体ごと回転させる)
        transform.Rotate(Vector3.up * mouseX);
    }
}
