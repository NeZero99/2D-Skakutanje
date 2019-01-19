using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class PlayServices : MonoBehaviour {

    public static PlayServices servisi;

    private void Awake()
    {
        servisi = this;
        DontDestroyOnLoad(servisi);
    }

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignIn();

        if (Social.localUser.authenticated)
        {
            OpenSave(false);
        }
    }

    void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            //ovde valjda treba da se hendluje success ili failture ali valjda nije obavezno
        });
    }

    #region SkorTabela
    public void dodavanjeSkoraUTabelu(string IDtabele, int skor)
    {
        Social.ReportScore(skor, IDtabele, (bool success) => {
            Debug.Log("Skor je ubacen u servise: " + success.ToString());
        });
    }
    public void prikazivanjeTabele()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CggIxK7n5R8QAhAB");
        }
    }
    #endregion

    #region Saving
    private bool isSaving = false;
    public void OpenSave(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame
                .OpenWithAutomaticConflictResolution(
                "JumpOnTime",
                GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpened);
        }
    }

    private void SaveGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)//writing
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(PlayerPrefs.GetInt("HighScore", 0).ToString());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved at: " + DateTime.Now.ToString()).Build();

                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else//reading
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadUpdate);
            }
        }
    }

    private void LoadUpdate(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string saveData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            PlayerPrefs.SetInt("HighScore", Convert.ToInt32(data));
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log("Sacuvano: " + status);
    }
    #endregion
}
