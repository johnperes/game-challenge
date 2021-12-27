using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Sprite[] MusicSpriteList;

    [SerializeField]
    Sprite[] VolumeSpriteList;

    [SerializeField]
    Image MusicImage;

    [SerializeField]
    Image VolumeImage;

    [SerializeField]
    PlayerChar[] Player1PrefabList;

    [SerializeField]
    PlayerChar[] Player2PrefabList;

    [SerializeField]
    GameObject[] Player1SampleList;

    [SerializeField]
    GameObject[] Player2SampleList;

    [SerializeField]
    TMP_Text Player1Health;

    [SerializeField]
    TMP_Text Player2Health;

    [SerializeField]
    TMP_Text Player1Attack;

    [SerializeField]
    TMP_Text Player2Attack;

    int selectedPlayer1 = 0;
    int selectedPlayer2 = 0;

    void Start() {
        UpdateChangePlayer1();
        UpdateChangePlayer2();
        UpdateChangeMusic();
        UpdateChangeVolume();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void FullScreen() {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    public void Windowed() {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1066, 600, false);
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    public void ChangeMusic() {
        StaticData.MusicOn = !StaticData.MusicOn;
        UpdateChangeMusic();
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    void UpdateChangeMusic() {
        if (StaticData.MusicOn) {
            MusicImage.sprite = MusicSpriteList[0];
        } else {
            MusicImage.sprite = MusicSpriteList[1];
        }
        AudioController.Instance.UpdateVolume();
    }

    public void ChangeVolume() {
        StaticData.SfxVolume++;
        UpdateChangeVolume();
        VolumeImage.sprite = VolumeSpriteList[StaticData.SfxVolume];
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    void UpdateChangeVolume() {
        if (StaticData.SfxVolume > 3) {
            StaticData.SfxVolume = 0;
        }
        AudioController.Instance.UpdateVolume();
    }

    public void ChangePlayer1()
    {
        Player1SampleList[selectedPlayer1].SetActive(false);
        selectedPlayer1++;
        if (selectedPlayer1 >= Player1PrefabList.Length) {
            selectedPlayer1 = 0;
        }
        UpdateChangePlayer1();
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    public void UpdateChangePlayer1() {
        StaticData.Player1Prefab = Player1PrefabList[selectedPlayer1].playerPrefab;
        Player1Health.text = Player1PrefabList[selectedPlayer1].health.ToString();
        Player1Attack.text = Player1PrefabList[selectedPlayer1].attack.ToString();
        Player1SampleList[selectedPlayer1].SetActive(true);
    }

    public void ChangePlayer2()
    {
        Player2SampleList[selectedPlayer2].SetActive(false);
        selectedPlayer2++;
        if (selectedPlayer2 >= Player2PrefabList.Length) {
            selectedPlayer2 = 0;
        }
        UpdateChangePlayer2();
        // Play sound effect
        AudioController.Instance.Play(AudioController.AudioType.Click);
    }

    public void UpdateChangePlayer2() {
        StaticData.Player2Prefab = Player2PrefabList[selectedPlayer2].playerPrefab;
        Player2Health.text = Player2PrefabList[selectedPlayer2].health.ToString();
        Player2Attack.text = Player2PrefabList[selectedPlayer2].attack.ToString();
        Player2SampleList[selectedPlayer2].SetActive(true);
    }
}
