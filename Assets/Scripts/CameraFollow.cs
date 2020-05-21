using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform _cameraTr;

    private Vector3 _oldPos;

    private Vector3 _offset;

    void Awake()
    {
        _cameraTr = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        _offset = _cameraTr.position - transform.position;
    
    }

    // Update is called once per frame
    void Update()
    {
        if(_cameraTr!=null)
        _cameraTr.position = transform.position + _offset;
    }
}
