using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    public GameObject settingUI;
    public GameObject GameSceneCanvas;
    public GameObject movementMgr;

    string log;

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "EnterScene")
        {

            GameSceneCanvas.SetActive(false);
            movementMgr.SetActive(false);
        }
        else if (scene.name == "GameScene")
        {

            movementMgr.SetActive(true);
            GameSceneCanvas.SetActive(true);
            settingUI.SetActive(false);
        }
    }
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //settingUI = GameObject.Find("Canvas/SettingUI");
        settingUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goEnterScene()
    {
        int score = GameSceneUI.instance.getScore();
#if RELEASE_MODE
        GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_rank, score, success => log = $"{success}");
#endif
        settingUI.SetActive(false);
        SceneManager.LoadScene("EnterScene");
    }
    public void resumeButton()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void pauseButton()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
