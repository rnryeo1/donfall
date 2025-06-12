using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float bounceForce = 2f;

    private Rigidbody rb;

    [SerializeField] FloatingHealthBar healthBar;
    float health;
    float maxHealth = 3;
    private string log;
    GameObject[] Objs;
    GameObject[] Obstacles;
    // Start is called before the first frame update

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "EnterScene")
        {

        }
        else if (scene.name == "GameScene")
        {
            GameSceneUI.instance.pauseButton.SetActive(true);
        }
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // 충돌이 발생했을 때, 반사 힘을 적용합니다.
        if (collision.gameObject.CompareTag("Object"))
        {
            AudioManager.Instance.PlayBallCol();
            GameSceneUI.instance.plus1Score();
            UpHp(0.5f);
            Destroy(collision.gameObject);
        }

    }
    public void watchedRewardAds()
    {
        GameSceneUI.instance.pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
        destroyResource();
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        transform.position = new Vector3(0, 0, 0);

        int lifecnt3 = RewardAdsManager.instance.getlifecount();
        lifecnt3--;
        RewardAdsManager.instance.setlifecount(lifecnt3);
        GameSceneUI.instance.RestartUI.SetActive(false);
    }
    //재시작
    public void RestartGame()
    {
        int score = GameSceneUI.instance.getScore();
# if RELEASE_MODE
        GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_rank, score, success => log = $"{success}");
#endif
        GameSceneUI.instance.pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
        GameSceneUI.instance.resetScore();
        destroyResource();
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        transform.position = new Vector3(0, 0, 0);
        GameSceneUI.instance.RestartUI.SetActive(false);
    }
    void Update()
    {
        if (transform.position.y <= -15)
        {
            int score = GameSceneUI.instance.getScore();
#if RELEASE_MODE
            GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_rank, score, success => log = $"{success}");
#endif
            //Destroy(gameObject);
            //RestartGame();
            GameSceneUI.instance.pauseButton.SetActive(false);
            Time.timeScale = 0.0f;
            GameSceneUI.instance.RestartUI.SetActive(true);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            int score = GameSceneUI.instance.getScore();
#if RELEASE_MODE
            GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_rank, score, success => log = $"{success}");
#endif

            GameSceneUI.instance.pauseButton.SetActive(false);
            Time.timeScale = 0.0f;
            GameSceneUI.instance.RestartUI.SetActive(true);
            //RestartGame();
        }


    }
    public void UpHp(float _hp)
    {
        health += _hp;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void destroyResource()
    {
        Objs = GameObject.FindGameObjectsWithTag("Object");
        for (int i = 0; i < Objs.Length; i++)
        {
            Destroy(Objs[i].gameObject);
        }
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Obstacles.Length; i++)
        {
            Destroy(Obstacles[i].gameObject);
        }
    }

}
