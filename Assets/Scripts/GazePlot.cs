using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePlot : MonoBehaviour
{

    public bool setTimer;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        setTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (setTimer)
        {
            timer += Time.deltaTime;
        }
    }
    
    public void ChangeScene()
    {
        SceneChangeManager.SCENE.GameStart();
    }

    // collision : 부딪힌 애
    public void OnTriggerEnter2D(Collider2D collision)
    {
        setTimer = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "game")
        {
            if (timer >= 3.0f)
            {
                ChangeScene();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        setTimer = false;
        timer = 0;
    }
}
