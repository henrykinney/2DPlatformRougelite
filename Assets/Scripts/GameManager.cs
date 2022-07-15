using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float LevelTime;
    public void StartGame() {
        SceneManager.LoadScene("SampleScene");
    }
    public void TransitionLevel() {
        SceneManager.LoadScene("HubScene");
    }
    void Start()
    {
        LevelTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LevelTime += Time.deltaTime;
    }
}
