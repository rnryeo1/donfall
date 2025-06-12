using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckOnCollisionEnter : MonoBehaviour
{
    public bool grounded = false;
    public float groundedCheckDistance;
    void Start()
    {
        groundedCheckDistance = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
    }


}
