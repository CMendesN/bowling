using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject bola;
    private Vector3 offset;
 // Use this for initialization
    void Start()
    {
        offset = transform.position - bola.transform.position;
    }
 // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = bola.transform.position + offset;
    }
}
