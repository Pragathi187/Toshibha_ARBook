using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public float damping = 1;

    void Start()
    {
        target = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        var rotation = Quaternion.LookRotation(transform.position - target.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
