using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl: MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10f;         //移動スピード
    [SerializeField] private float jumpForce = 5f;      //ジャンプ力
    [SerializeField] private float bulletSpeed = 1f;    //弾の速度
    [SerializeField] private GameObject bullet;         //弾のPrefab
    private InputAction moveAction;                     //移動のアクションイベント
    private InputAction jumpAction;                     //ジャンプのアクションイベント
    private InputAction shotAction;                     //射撃のアクションイベント
    private Vector2 moveInput;                          //移動のベクトル
    private bool jumpRequested = false;                 //ジャンプのリクエストフラグ
    private bool shotRequested = false;                 //射撃のリクエストフラグ
    private bool isGround = true;                       //地面の接触フラグ

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //MoveとJumpという名前のInputActionを取得
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        shotAction = InputSystem.actions.FindAction("Attack");
        //入力を受け取る
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();

        jumpAction.performed += OnJump;
        jumpAction.Enable();

        shotAction.performed += OnShot;
    }

    private void Update()
    {
        Shot();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    /// <summary>
    /// 移動のアクションを作成
    /// </summary>
    /// <param name="context"></param>
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveInput = context.ReadValue<Vector2>();
        else if(context.canceled)
            moveInput = Vector2.zero;
    }

    /// <summary>
    /// ジャンプのアクションを作成
    /// </summary>
    /// <param name="context"></param>
    private void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if(context.performed)
        {
            jumpRequested = true;
        }
    }

    /// <summary>
    /// 射撃のアクションを作成
    /// </summary>
    /// <param name="context"></param>
    private void OnShot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shotRequested = true;
        }
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x,0, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);
    }

    /// <summary>
    /// ジャンプの処理
    /// </summary>
    private void Jump()
    {
        if(rb ==null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (jumpRequested && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            jumpRequested = false;
            isGround = false;
        }
    }

    /// <summary>
    /// 弾を生成する処理
    /// </summary>
    private void Shot()
    {
        if(shotRequested)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
            shotRequested = false;
        }
    }

    /// <summary>
    /// 接触判定をTagで取得
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //地面との接触判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

        //敵との接触判定
        //敵に触れたらHPを減らす
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (UiManager.instance != null)
            {
                UiManager.instance.GetHPNum();
            }
        }
    }
}
