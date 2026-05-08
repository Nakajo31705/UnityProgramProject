using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private int score_num;     //表示するスコア
    [SerializeField] private Text score_text;   //スコアのテキストオブジェクト
    [SerializeField] private int HP_num;        //表示するHP
    [SerializeField] private Text HP_text;      //HPのテキストオブジェクト
    [SerializeField] private int upScoreNum;    //上昇するスコアの値
    [SerializeField] private int downHPNum;     //1回当たりのHPを減らす値
    public static UiManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        score_num = 0;
    }
    private void Update()
    {
        DrawUI();
    }

    public void GetScoreNum()
    {
        score_num += upScoreNum;
    }

    public void GetHPNum()
    {
        if(HP_num > 0)
        {
            HP_num -= downHPNum;
        }
        else
        {
            HP_num = 0;
        }
        
    }

    private void DrawUI()
    {
        HP_text.text = "HP:" + HP_num;
        score_text.text = "Score:" + score_num;
    }
}
