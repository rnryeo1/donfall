using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneUI : MonoBehaviour
{
    public static GameSceneUI instance;
    public GameObject RestartUI;
    public TextMeshProUGUI scoreText;
    public GameObject pauseButton;

    public GameObject joystickUI;
    private int score;
    // Start is called before the first frame update
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
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "EnterScene")
        {
            RestartUI.SetActive(false);
            score = 0;
            scoreText.text = score.ToString();
        }
        else if (scene.name == "GameScene")
        {
            RestartUI.SetActive(false);
            score = 0;
            scoreText.text = score.ToString();
        }
    }
    public void plus1Score()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
    public void resetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public int getScore()
    {
        return score;
    }
}
