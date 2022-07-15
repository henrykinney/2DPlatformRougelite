using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI timertext;
    void Start()
    {
        timertext = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = GameObject.Find("GameManager").GetComponent<GameManager>().LevelTime;
        int seconds = (Mathf.FloorToInt(time) % 60);
        string secstr = seconds.ToString();
        if (seconds <= 9) {
            secstr = "0" + secstr;
        }
        timertext.text = "Time: " + Mathf.FloorToInt(time / 60) + ":" + secstr;
    }
}
