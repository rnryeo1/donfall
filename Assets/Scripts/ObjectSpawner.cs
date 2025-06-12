using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject rangeObject1;
    BoxCollider rangeCollider1;
    public GameObject rangeObject2;
    BoxCollider rangeCollider2;
    public GameObject rangeObject3;
    BoxCollider rangeCollider3;
    public GameObject rangeObject4;
    BoxCollider rangeCollider4;
    Vector3 randpos;

    public GameObject ObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rangeObject1 = GameObject.Find("Spawners/ObjectsSpawner/ObjectsSpawner1");
        rangeObject2 = GameObject.Find("Spawners/ObjectsSpawner/ObjectsSpawner2");
        rangeObject3 = GameObject.Find("Spawners/ObjectsSpawner/ObjectsSpawner3");
        rangeObject4 = GameObject.Find("Spawners/ObjectsSpawner/ObjectsSpawner4");
        rangeCollider1 = rangeObject1.GetComponent<BoxCollider>();
        rangeCollider2 = rangeObject2.GetComponent<BoxCollider>();
        rangeCollider3 = rangeObject3.GetComponent<BoxCollider>();
        rangeCollider4 = rangeObject4.GetComponent<BoxCollider>();

        StartCoroutine(WaveSpawn());
    }
    // Update is called once per frame
    IEnumerator WaveSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject objects = Instantiate(ObjectPrefab, Return_RandomPosition(), Quaternion.identity); //짝 2,4,6,
        }
    }
    Vector3 Return_RandomPosition()
    {
        float random = Random.Range(1, 5);
        if (random == 1)
        {
            randpos = RandomPosition1();
        }
        else if (random == 2)
        {
            randpos = RandomPosition2();
        }
        else if (random == 3)
        {
            randpos = RandomPosition3();
        }
        else if (random == 4)
        {
            randpos = RandomPosition4();
        }
        return randpos;
    }
    Vector3 RandomPosition1()
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
    Vector3 RandomPosition2()
    {
        Vector3 originPosition = rangeObject2.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider2.bounds.size.x;
        float range_Z = rangeCollider2.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;


        return respawnPosition;
    }
    Vector3 RandomPosition3()
    {
        Vector3 originPosition = rangeObject3.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider3.bounds.size.x;
        float range_Z = rangeCollider3.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
    Vector3 RandomPosition4()
    {

        Vector3 originPosition = rangeObject4.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider4.bounds.size.x;
        float range_Z = rangeCollider4.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;

        return respawnPosition;
    }
}
