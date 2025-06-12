using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnterSceneManager : MonoBehaviour
{
    public Sprite ImageAudio1;
    public Sprite ImageAudio2;

    string log;
    public GameObject audiobutton;
    public GameObject achieveImageUI;
    public GameObject infoImageUI;
    private bool AudioOn = false;
    // Start is called before the first frame update
    void Start()
    {
        GPGSBinder.Inst.Init();
        achieveImageUI.SetActive(false);
        infoImageUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void closeAchievement()
    {
        achieveImageUI.SetActive(false);
    }
    public void closeinfo()
    {
        infoImageUI.SetActive(false);
    }

    public void goGameScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }
    public void exitgame()
    {
        Application.Quit();
    }

    public void AudioOnOff()
    {
        if (!AudioOn)
        {
            AudioListener.volume = 0;
            audiobutton.GetComponent<Image>().sprite = ImageAudio2;
            //AudioButton.GetComponent<Image>().sprite = ImageAudio2;
            AudioOn = true;
        }
        else
        {
            AudioListener.volume = 1;
            audiobutton.GetComponent<Image>().sprite = ImageAudio1;
            //AudioButton.GetComponent<Image>().sprite = ImageAudio1;
            AudioOn = false;
        }

    }

    public void openAchieveButton()
    {
        achieveImageUI.SetActive(true);
    }
    public void openinfo()
    {
        infoImageUI.SetActive(true);
    }
    public void ShowAllLeaderboardUI()
    {
        Debug.Log("클릭1");
        GPGSBinder.Inst.ShowAllLeaderboardUI();
    }
    public void ShowTargetLeaderboardUI_num()
    {
        Debug.Log("클릭2");
        GPGSBinder.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_rank);
    }
    // public void ReportLeaderboard_num()  //입력
    // {
    //     GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_rank, 1000, success => log = $"{success}");
    // }
    public void LoadAllLeaderboardArray_num()
    {
        Debug.Log("클릭3");
        GPGSBinder.Inst.LoadAllLeaderboardArray(GPGSIds.leaderboard_rank, scores =>
           {
               log = "";
               for (int i = 0; i < scores.Length; i++)
                   log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
           });
    }
    public void LoadCustomLeaderboardArray_num()
    {
        Debug.Log("클릭4");
        GPGSBinder.Inst.LoadCustomLeaderboardArray(GPGSIds.leaderboard_rank, 10,
       GooglePlayGames.BasicApi.LeaderboardStart.PlayerCentered, GooglePlayGames.BasicApi.LeaderboardTimeSpan.AllTime, (success, scoreData) =>
       {
           log = $"{success}\n";
           var scores = scoreData.Scores;
           for (int i = 0; i < scores.Length; i++)
               log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
       });
    }





}
