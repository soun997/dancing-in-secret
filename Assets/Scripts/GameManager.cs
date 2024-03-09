using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 준비 카운트 판넬
    public GameObject readyPanel;
    public Text readyText;
    public bool isDone = false; // 이 판넬이 내려갔는지 여부

    // 플레이타임
    public Text playTime;
    public float minute;
    public float second;
    public float time;
    public bool isTimeover = false;

    // 일시정지 판넬
    public GameObject pausePanel;

    // 게임오버 판넬
    public GameObject gameoverPanel;
    public Text text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject retry_btn;
    public GameObject quit_btn;

    // 게임 클리어 판넬
    public GameObject gameclearPanel;
    public GameObject clearText;
    public GameObject dialog;
    public GameObject cleartime;
    public Text cleartimeText;
    public GameObject retry_btn2;
    public GameObject quit_btn2;
    public bool isClear = false;
 
    #region 게임 관련 변수들
    // 스트레스 지수
    public Slider stress;
    public Text stressText;
    int stressVal;
    public bool isBtnDown = false;

    // 선생님
    public Image teacher;
    public Sprite teacher_back1;
    public Sprite teacher_back2;
    public Sprite teacher_turn;
    public Sprite teacher_front;
    public bool isFront = false;
    public bool isDetected = false;

    // 칠판 필기
    public Text boardText;

    // 댄스 애니메이션
    public GameObject[] dances = new GameObject[5];
    public Image dancer;
    public Sprite normal;
    public Sprite desk;
    int index;

    // 댄스 조명
    IEnumerator light_up;
    public GameObject[] lights = new GameObject[5];
    int r;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        time = 120;

        pausePanel.SetActive(false);

        gameoverPanel.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        retry_btn.SetActive(false);
        quit_btn.SetActive(false);

        gameclearPanel.SetActive(false);
        clearText.SetActive(false);
        dialog.SetActive(false);
        cleartime.SetActive(false);
        retry_btn2.SetActive(false);
        quit_btn2.SetActive(false);

        readyPanel.SetActive(true);
        StartCoroutine("Ready");

        stress.maxValue = 100;
        stress.minValue = 0;
        stress.value = 0;

        for (int i = 0; i < dances.Length; i++)
        {
            dances[i].SetActive(false);
            lights[i].SetActive(false);
        }

        light_up = LightUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone == true)
        {
            time -= Time.deltaTime;
            minute = time / 60;
            second = time % 60;
            if (second < 10)
                playTime.text = "0" + (int)minute + ":0" + (int)second;
            else
                playTime.text = "0" + (int)minute + ":" + (int)second;

            if (time < 0)
                isTimeover = true;
        }

        if (isBtnDown == true)
        {
            stress.value += Time.deltaTime * 3;
            stressVal = 100 - (int)stress.value;

            if (stressVal >= 100)
                stressText.text = "100";
            else if (stressVal < 100 && stressVal >= 10)
                stressText.text = " " + stressVal;
            else
                stressText.text = "  " + stressVal;
        }

        if (isFront == true && isBtnDown == true)
        {
            isDetected = true;
        }

        if (isDetected == true)
        {
            isDetected = false;
            isBtnDown = false;
            dances[index].SetActive(false);
            dancer.sprite = normal;
            dances[index].SetActive(false);

            StartCoroutine("GameOver");

        }

        if (stress.value == 100)
        {
            StartCoroutine("GameClear");
        }
    }

    IEnumerator Ready()
    {
        for (int i = 3; i > 0; i--)
        {
            readyText.text = "   " + i;
            yield return new WaitForSeconds(1.0f);
        }
        readyText.text = "시작!";
        yield return new WaitForSeconds(1.0f);
        isDone = true;
        readyPanel.SetActive(false);

        StartCoroutine("HandWriting");
        StartCoroutine("WriteOnBoard");
    }

    IEnumerator HandWriting()
    {
        while (true)
        {
            int turn = Random.Range(0, 3);
            yield return new WaitForSeconds(1.0f);

            if (turn == 0)
            {
                isFront = false;

                float sec = Random.Range(0.3f, 2.0f);
                boardText.gameObject.SetActive(false);
                teacher.sprite = teacher_turn;
                yield return new WaitForSeconds(sec);

                turn = Random.Range(0, 2);
                if (turn == 0)
                {
                    isFront = true;
                    teacher.sprite = teacher_front;
                    yield return new WaitForSeconds(2.0f);
                }
            }
            else
            {
                isFront = false;

                boardText.gameObject.SetActive(true);
                teacher.sprite = teacher_back1;

                yield return new WaitForSeconds(1.0f);
                teacher.sprite = teacher_back2;
                yield return new WaitForSeconds(1.0f);
                boardText.gameObject.SetActive(true);
                teacher.sprite = teacher_back1;

            }

        }
    }

    IEnumerator WriteOnBoard()
    {
        string[] writings = { "캐시 메모리", "패리티 블록", "플래시 메모리", "직접 사상 방식",
            "테이블 스키마", "릴레이션 정규화", "ER 다이어그램" };

        int i = 0;

        while (true)
        {
            boardText.text = "";

            if (i == writings.Length)
                i = 0;

            foreach (char c in writings[i])
            {
                boardText.text += c;
                yield return new WaitForSeconds(0.5f);
            }
            i++;
            yield return new WaitForSeconds(2.0f);
        }
    }
    
    IEnumerator GameOver()
    {
        SoundManager.instance.Surprise();

        gameoverPanel.SetActive(true);

        string text = "... 자네는 F일세";
        text1.text = "";
        foreach (char c in text)
        {
            text1.text += c;
            if (c == '.')
                yield return new WaitForSeconds(0.5f);
            else
                yield return new WaitForSeconds(0.3f);
        }
        text2.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        text3.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        quit_btn.SetActive(true);
        retry_btn.SetActive(true);

    }

    IEnumerator GameClear()
    {
        gameclearPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        clearText.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        dialog.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 0;
        cleartime.SetActive(true);
        cleartimeText.text = (int)minute + "m " + (int)second + "s";

        retry_btn2.SetActive(true);
        quit_btn2.SetActive(true);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Retry()
    {
        Time.timeScale = 1;
        SceneChangeManager.SCENE.GameStart();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ButtonDown()
    {
        isBtnDown = true;
        index = Random.Range(0, 5);

        dancer.sprite = desk;
        dances[index].SetActive(true);

        SoundManager.instance.PlaySong();
        StartCoroutine(light_up);
    }

    IEnumerator LightUp()
    {
        while (true)
        {
            r = Random.Range(0, 5);

            lights[r].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            lights[r].SetActive(false);
        } 
    }

    public void ButtonUp()
    {
        isBtnDown = false;

        dancer.sprite = normal;
        dances[index].SetActive(false);
        lights[r].SetActive(false);
        SoundManager.instance.PauseSong();
        StopCoroutine(light_up);
    }
}