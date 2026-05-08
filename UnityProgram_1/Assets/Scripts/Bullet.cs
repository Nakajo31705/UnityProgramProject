using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float destroyTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        Destroy(gameObject, destroyTime);
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
