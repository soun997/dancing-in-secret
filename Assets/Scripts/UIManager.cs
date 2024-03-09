using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region
    public Text titleText;
    #endregion


    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (UIManager.instance == null)
        {
            UIManager.instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "";
        StartCoroutine("PrintTitle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintTitle()
    {
        string title = "교수님 몰래 춤추기!";
        foreach (char c in title)
        {
            titleText.text += c;
            yield return new WaitForSeconds(0.15f);
        }

    }
}
