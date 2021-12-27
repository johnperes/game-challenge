using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public enum AudioType { Walk, DiceHit, DiceFinish, GetItem, BattleStart, BattleEnd, HitPlayer, GameEnd, Click };
    
    // Singleton
    public static AudioController Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    AudioClip[] musicList;

    [SerializeField]
    AudioClip walkClip;

    [SerializeField]
    AudioClip diceHitClip;

    [SerializeField]
    AudioClip diceFinishClip;

    [SerializeField]
    AudioClip getItemClip;

    [SerializeField]
    AudioClip battleStartClip;

    [SerializeField]
    AudioClip battleEndClip;

    [SerializeField]
    AudioClip hitPlayer;

    [SerializeField]
    AudioClip gameEndClip;

    [SerializeField]
    AudioClip clickClip;

    void Start() {
        // Plays a random music
        musicSource.clip = musicList[Random.Range(0, musicList.Length)];
        musicSource.Play();
        UpdateVolume();
    }

    // Adjust the sound volume
    public void UpdateVolume() {
        if (!StaticData.MusicOn) {
            musicSource.mute = true;
        } else {
            musicSource.mute = false;
        }
        musicSource.volume = (StaticData.SfxVolume * .25f) / 10f;
        sfxSource.volume = StaticData.SfxVolume * .25f;
    }

    // Plays an audio song
    public void Play(AudioType type)
    {
        switch (type)
        {
            case AudioType.Walk:
                sfxSource.PlayOneShot(walkClip);
                break;
            case AudioType.DiceHit:
                sfxSource.PlayOneShot(diceHitClip);
                break;
            case AudioType.DiceFinish:
                sfxSource.PlayOneShot(diceFinishClip);
                break;
            case AudioType.GetItem:
                sfxSource.PlayOneShot(getItemClip);
                break;
            case AudioType.BattleStart:
                sfxSource.PlayOneShot(battleStartClip);
                break;
            case AudioType.BattleEnd:
                sfxSource.PlayOneShot(battleEndClip);
                break;
            case AudioType.HitPlayer:
                sfxSource.PlayOneShot(hitPlayer);
                break;
            case AudioType.GameEnd:
                sfxSource.PlayOneShot(gameEndClip);
                break;
            case AudioType.Click:
                sfxSource.PlayOneShot(clickClip);
                break;
        }
    }
}
