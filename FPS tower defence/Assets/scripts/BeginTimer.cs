using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeginTimer : MonoBehaviour
{
    TextMeshProUGUI text;
    public float timer;
    public GameObject beginTimer;
    public pauseMenu freeze;

    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        freeze.paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0.1)
        {
            timer -= Time.deltaTime;
            text.text = Mathf.RoundToInt(timer + 0.5f).ToString();

        }
        else
        {
            text.text = "";
            if (gameStarted == false)
            {
                freeze.paused = false;
                gameStarted = true;
            }
        }
    }
}
