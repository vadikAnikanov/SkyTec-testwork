using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerModel : MonoBehaviour
{
    private Transform _parent;

    void Awake()
    {
        _parent = transform.parent;
    }

    private void OnCollisionEnter(Collision col)
    {
        transform.parent = _parent;
        gameObject.SetActive(false);
    }


}
