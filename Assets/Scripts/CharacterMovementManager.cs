using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMovementManager : MonoBehaviour
{
    public static CharacterMovementManager instance;
    //public VariableJoystick joystick;
    //public DynamicJoystick joystick;
    public FixedJoystick joystick;
    public CharacterController controller;
    public float movementSpeed;
    public float rotationSpeed;
    public Canvas inputCanvas;
    public bool isJoystick;

    private GameObject player;
    private Rigidbody rb;
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


        }
        else if (scene.name == "GameScene")
        {

            player = GameObject.FindWithTag("Player");
            rb = player.GetComponent<Rigidbody>();
            inputCanvas.gameObject.SetActive(true);
        }
    }
    public void watchedRewardAds()
    {
        player.GetComponent<PlayerController>().watchedRewardAds();
    }
    public void RestartGame()
    {
        player.GetComponent<PlayerController>().RestartGame();
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        EnableJoystickInput();
    }
    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (joystick)
        {
            // var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            // //controller.SimpleMove(movementDirection * movementSpeed);



            // // 정규화된 방향으로 이동 벡터 설정
            // movementDirection.Normalize();

            // // 이동 벡터에 이동 속도를 곱해 최종 이동 벡터 생성
            // Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;

            // // 현재 위치에 이동 벡터를 더하여 오브젝트를 이동
            // transform.Translate(movement);
            //var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);

            //controller.transform.rotation = Quaternion.LookRotation(targetDirection);


            // Joystick 입력을 받아 이동 방향 설정
            Vector3 moveDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);

            // 이동 방향이 0이 아닌 경우에만 이동 처리
            if (moveDirection.magnitude > 0.1f)
            {
                // 정규화된 방향으로 이동 벡터 설정
                moveDirection.Normalize();

                // 이동 벡터에 이동 속도를 곱해 최종 이동 벡터 생성
                Vector3 movement = moveDirection * movementSpeed * Time.deltaTime;

                // 현재 위치에 이동 벡터를 더하여 오브젝트를 이동
                // player.transform.Translate(movement);
                // Rigidbody의 MovePosition 함수를 사용하여 이동
                rb.MovePosition(rb.position + movement);
            }

        }
    }





}
