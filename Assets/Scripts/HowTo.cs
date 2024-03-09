using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowTo : MonoBehaviour
{
    public GameObject explainPanel;
    public Text titleText;
    public Text text1;
    public GameObject text2;
    public GameObject btn;

    // Start is called before the first frame update
    void Start()
    {
        explainPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplainUp()
    {
        SoundManager.instance.Btn_Click_Open();
        explainPanel.SetActive(true);

        titleText.text = "";
        text1.text = "";
        text2.SetActive(false);
        btn.SetActive(false);

        StartCoroutine("PrintTitle");       
    }

    IEnumerator PrintTitle()
    {
        string title = "교수님 몰래 춤춰라!";
        foreach (char c in title)
        {
            titleText.text += c;
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("PrintText");
    }

    IEnumerator PrintText()
    {
        string text = "지루한 강의에 지쳐버린 당신은 교수님 몰래 춤을 추기로 결심합니다.";
        foreach (char c in text)
        {
            text1.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.5f);
        text2.SetActive(true);
        yield return new WaitForSeconds(2f);
        btn.SetActive(true);
    }

    public void ExplainDown()
    {
        SoundManager.instance.Btn_Click_Close();
        explainPanel.SetActive(false);
    }
}
