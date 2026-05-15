using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Net.NetworkInformation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5.0f;    //プレイヤーの移動速度
    [SerializeField] private float jumpFoce = 10.0f;    //プレイヤーのジャンプ力
    [SerializeField] private Camera cam;                //カメラ
    [SerializeField] private GameObject havePos;        //ボムを持つ場所
    private Vector2 moveInput;              //移動のベクトル
    private bool jumpRequested = false;     //ジャンプのアクションフラグ
    private bool clickRequested = false;    //クリックのアクションフラグ
    private bool isGround = true;           //地面の接触判定フラグ
    private bool isHaving = false;          //ボムを所持しているかのフラグ

    //isHavingのゲッター
    public bool GetHaving()
    {
        return isHaving;
    }

    private void Start()
    {
    }

    private void Update()
    {
        HaveBomb();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    /// <summary>
    /// 移動のアクションを作成
    /// </summary>
    /// <param name="value"></param>
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    /// <summary>
    /// ジャンプのアクションを作成
    /// </summary>
    /// <param name="value"></param>
    private void OnJump(InputValue value)
    {
        if(value.isPressed)
            jumpRequested = true;
    }

    /// <summary>
    /// クリックのアクションを作成
    /// </summary>
    /// <param name="value"></param>
    private void OnClick(InputValue value)
    {
        if (value.isPressed)
            clickRequested = true;
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed *  Time.deltaTime);
    }

    /// <summary>
    /// ジャンプの処理
    /// </summary>
    private void Jump()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if(jumpRequested && isGround)
        {
            rb.AddForce(Vector3.up * jumpFoce, ForceMode.Impulse);
            jumpRequested = false;
            isGround = false;
        }
    }

    /// <summary>
    /// ボムを保持する処理
    /// Rayを飛ばして、TagがBombならプレイヤーの前に移動します。
    /// </summary>
    private void HaveBomb()
    {
        RaycastHit hit;
        //カメラの中央座標を取得
        Vector3 cameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.ScreenPointToRay(cameraCenter);

        if (clickRequested)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, transform.forward * 10.0f, Color.red);
                clickRequested = false;
                Transform objectHit = hit.transform;

                if (objectHit.CompareTag("Bomb"))
                {
                    objectHit.position = new Vector3(havePos.transform.position.x, 0.5f, havePos.transform.position.z);
                    objectHit.transform.SetParent(this.gameObject.transform);
                }
            }
            clickRequested = false;
            isHaving = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
