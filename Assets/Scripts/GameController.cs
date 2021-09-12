using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public NejikoController nejiko;
    public Text scoreLabel;
    public LifePanel lifePanel;

    // Update is called once per frame
    void Update()
    {
        //スコアラベルを更新
        int score = CalcScore();
        scoreLabel.text = "Score:" + score + "m";

        //ライフパネルを更新
        lifePanel.UpdateLife(nejiko.Life());

        //ネジ子のライフが０になったらゲームオーバー
        if (nejiko.Life() <= 0)
        {
            //これ以降のUpdateは止める
            enabled = false;

            //ハイスコアの更新
            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            //2秒以降のReturnToTitleを呼び出す
            Invoke("ReturnToTitle", 2.0f);
        }
        
    }

    int CalcScore()
    {
        //ネジ子の走行距離をスコアとする
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        //タイトルシーンに切り替え
        Application.LoadLevel("Title");
    }
    
}
