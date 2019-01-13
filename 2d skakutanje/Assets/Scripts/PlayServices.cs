using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayServices : MonoBehaviour {

    public static PlayServices servisi;

    private void Awake()
    {
        servisi = this;
        DontDestroyOnLoad(servisi);
    }

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            //ovde valjda treba da se hendluje success ili failture ali valjda nije obavezno
        });
    }

    #region Tabela
    public void dodavanjeSkoraUTabelu(string IDtabele, int skor)
    {
        Social.ReportScore(skor, IDtabele, (bool success) => {
            // handle success or failure
        });
    }
    public void prikazivanjeTabele()
    {
        //Social.ShowLeaderboardUI();
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CggIxK7n5R8QAhAB");
    }
    #endregion
}
