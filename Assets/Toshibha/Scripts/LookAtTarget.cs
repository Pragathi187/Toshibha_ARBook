using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        target = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
        this.transform.LookAt(targetPostition);
    }
}
