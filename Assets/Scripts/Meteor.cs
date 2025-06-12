using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 1f;
    public float bounceForce = 1f;

    private Rigidbody rb;
    private GameObject player;

    private bool onceDir = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        // 코루틴을 시작합니다.
        //SetRandomDirection();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
        //StartCoroutine(MoveInRandomDirection());

    }

    IEnumerator MoveInRandomDirection()
    {
        while (true)
        {
            // 새로운 랜덤한 방향을 설정합니다.

            if (transform.position.y <= 0.25f)
            {
                Debug.Log("확인2");
                SetRandomDirection();
            }
            // 일정한 시간 동안 기다립니다.
            yield return new WaitForSeconds(55f);
        }
    }
    void Update()
    {

        if (transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
        if (transform.position.y <= 0.25f && !onceDir)
        {
            Debug.Log("확인");
            SetRandomDirection();
            onceDir = true;
        }
    }
    void SetRandomDirection()
    {


        // 대상 객체를 향하는 방향 벡터를 구합니다.
        Vector3 directionToTarget = (player.transform.position - transform.position).normalized;

        // Rigidbody에 힘을 가해 방향을 변경합니다.
        rb.linearVelocity = directionToTarget * speed * 2;
    }
    void OnCollisionEnter(Collision collision)
    {
        // 충돌이 발생했을 때, 반사 힘을 적용합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayBallCol();
            player.GetComponent<PlayerController>().TakeDamage(0.3f);
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                Vector3 reflectDir = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal);
                reflectDir.y = 0f;
                rb.linearVelocity = reflectDir.normalized * speed * bounceForce;

                // 다른 객체에도 강한 힘을 가하여 반사 방향으로 이동하도록 합니다.
                //otherRb.velocity = reflectDir.normalized * speed * bounceForce / 2;
                Destroy(gameObject);
            }
        }
    }
}
