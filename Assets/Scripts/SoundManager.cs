using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    #region 변수
    public AudioClip btn_sound_open;
    public AudioClip btn_sound_close;
    public AudioClip page_turn1;
    public AudioClip page_turn2;
    public AudioClip chalk_fastwrite;
    public AudioClip chalk_slowwrite;
    public AudioClip surprise;
    public AudioClip[] songs = new AudioClip[3];


    AudioSource myAudio;
    #endregion

    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void Surprise()
    {
        myAudio.PlayOneShot(surprise);
    }

    public void Btn_Click_Open()
    {
        myAudio.PlayOneShot(btn_sound_open);
    }

    public void Btn_Click_Close()
    {
        myAudio.PlayOneShot(btn_sound_close);
    }

    public void FastWrite()
    {
        myAudio.PlayOneShot(chalk_fastwrite);
    }

    public void SlowWrite()
    {
        myAudio.PlayOneShot(chalk_slowwrite);
    }

    public void PageTurn1()
    {
        myAudio.PlayOneShot(page_turn1);
    }

    public void PageTurn2()
    {
        myAudio.PlayOneShot(page_turn2);
    }

    public void PlaySong()
    {
        int r = Random.Range(0, 3);
        myAudio.PlayOneShot(songs[r]);
    }

    public void PauseSong()
    {
        myAudio.Stop();
    }
}
