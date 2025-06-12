using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
