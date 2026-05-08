using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform playerBody;  //プレイヤーの回転
    [SerializeField] private float sensitivity;     //マウスの操作感度

    private InputSystem_Actions inputActions;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        //Lookのアクションが起きたらlookInputに保存
        inputActions.Player.Look.performed += context =>
        {
            lookInput = context.ReadValue<Vector2>();
        };

        //Lookのアクションを停止する
        inputActions.Player.Look.canceled += _ =>
        {
            lookInput = Vector2.zero;
        };
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisabele() => inputActions.Disable();

    /// <summary>
    /// マウス操作の処理
    /// </summary>
    private void Update()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
