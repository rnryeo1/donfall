using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 1f;
    public float bounceForce = 1f;

    private Rigidbody rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        // 코루틴을 시작합니다.
        SetRandomDirection();
        //StartCoroutine(MoveInRandomDirection());
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Move the object in the current direction
    //     transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //     // Change direction when out of bounds
    //     SetRandomDirection();

    // }

    IEnumerator MoveInRandomDirection()
    {
        while (true)
        {
            // 새로운 랜덤한 방향을 설정합니다.
            SetRandomDirection();
            // 일정한 시간 동안 기다립니다.
            yield return new WaitForSeconds(55f);
        }
    }

    void SetRandomDirection()
    {
        // 랜덤한 방향을 생성합니다.
        // Vector3 randomDirection = Random.onUnitSphere;

        // // 대상 객체를 향하는 방향 벡터를 구합니다.
        // Vector3 directionToTarget = (targetObject.transform.position - transform.position).normalized;

        // // 평면 상에 유지하기 위해 방향을 평면화합니다 (Y 축은 위 방향입니다).
        // randomDirection.y = 0;

        // // 상수 속도를 보장하기 위해 방향을 정규화합니다.
        // randomDirection.Normalize();

        // // Rigidbody에 힘을 가해 방향을 변경합니다.
        // rb.AddForce(randomDirection * speed, ForceMode.VelocityChange);

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
