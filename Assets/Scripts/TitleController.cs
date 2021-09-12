using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{

    public Text highScoreLabel;

    public void Start()
    {
        //ハイスコアの表示
        highScoreLabel.text = "High Score:" + PlayerPrefs.GetInt("HighScore") + "m";
    }
    public void OnStartButtonClicked()
    {
        Application.LoadLevel("Main");
    }
}
