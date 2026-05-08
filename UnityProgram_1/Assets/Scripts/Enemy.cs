using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   //移動速度
    [SerializeField] private float destroyTime = 3.0f;

    private void Update()
    {
        Moving();
        DestroyCount();
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    private void Moving()
    {
        transform.position += Vector3.forward * -moveSpeed * Time.deltaTime;
    }

    private void DestroyCount()
    {
        Destroy(gameObject, destroyTime);
    }

    /// <summary>
    /// 弾に当たったら自信を削除
    /// スコアカウントを更新
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            if (UiManager.instance != null)
            {
                UiManager.instance.GetScoreNum();
            }
        }
    }
}
