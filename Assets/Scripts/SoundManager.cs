using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public AudioClip     shoot, playerAttack, pickup, enemyAttack, btnClick;
    public AudioSource   audioSource;
    //public AudioSource backgrounSoundSource;
    public static bool   isSoundOn = true;

    private Toggle       soundToggle;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt(Constant.PREFS_SOUND) == 1 ? true : false;
        soundToggle = GetComponent<Toggle>();
        if (soundToggle != null)
        {
            soundToggle.GetComponent<Toggle>().isOn = isSoundOn;
            soundToggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(soundToggle);
            });
        }
    }

    void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
           
            //if (backgrounSoundSource != null && backgrounSoundSource.clip != null) { backgrounSoundSource.mute = false; }
            audioSource.mute = false;
            isSoundOn = true;
            PlaySound(Constant.SOUND_CLICK);
        }
        else
        {
            audioSource.mute = true;
            isSoundOn = false;
            //if (backgrounSoundSource != null && backgrounSoundSource.clip != null) { backgrounSoundSource.mute = true; }
        }
        PlayerPrefs.SetInt(Constant.PREFS_SOUND, isSoundOn ? 1 : 0);
    }

    public void PlaySound(string clip)
    {
        if (isSoundOn)
        {
            switch (clip)
            {
                case Constant.SOUND_CLICK:
                    audioSource.PlayOneShot(btnClick);
                    break;
                case Constant.SOUND_SHOOT:
                    audioSource.PlayOneShot(shoot);
                    break;
                case Constant.SOUND_PLAYER_ATTACK:
                    audioSource.PlayOneShot(playerAttack);
                    break;
                case Constant.SOUND_ENEMY_ATTACK:
                    audioSource.PlayOneShot(enemyAttack);
                    break;
                case Constant.SOUND_PICKUP:
                    audioSource.PlayOneShot(pickup);
                    break;
            }
        }
    }

    //public void PlaySound()
    //{
    //    backgrounSoundSource.Play();
    //}
    //public void PauseSound()
    //{
    //    backgrounSoundSource.Pause();
    //}
}
