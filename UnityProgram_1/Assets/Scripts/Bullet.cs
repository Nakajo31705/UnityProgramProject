using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 10f;
    private float destroyTime = 5f;
    private float destroyTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        destroyTimer += Time.deltaTime;
        Shot();
    }

    /// <summary>
    /// 生成されたら弾が動く
    /// </summary>
    private void Shot()
    {
        //弾が前方に進む処理
        rb.linearVelocity = transform.forward * bulletSpeed;
        //デストロイ時間を過ぎたら削除
        if (destroyTimer > destroyTime)
        {
            Destroy(gameObject);
            destroyTimer = 0f;
        }
    }

    /// <summary>
    /// エネミーに触れたら削除
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
