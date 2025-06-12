using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject rangeObject1;
    BoxCollider rangeCollider1;
    public GameObject ObstaclePrefab;
    // Start is called before the first frame update
    void Start()
    {
        rangeObject1 = GameObject.Find("Spawners/MeteorSpawner/MeteorSpawner1");
        rangeCollider1 = rangeObject1.GetComponent<BoxCollider>();
        StartCoroutine(WaveSpawn());
    }
    IEnumerator WaveSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject objects = Instantiate(ObstaclePrefab, returnPosition(), Quaternion.identity); //짝 2,4,6,
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    Vector3 returnPosition()
    {
        Vector3 originPosition = rangeObject1.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider1.bounds.size.x;
        float range_Z = rangeCollider1.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;

        return respawnPosition;
    }
}
