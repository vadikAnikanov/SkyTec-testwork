using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private Canvas _canvashealth;
    private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField]
    private List<MeshRenderer> _meshRendererList;


    void Start()
    {
        _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        _canvashealth = GetComponentInChildren<Canvas>();
    }

    public void SetVisibility(bool state)
    {
        _skinnedMesh.enabled = state;
        _meshRendererList[0].enabled = state;
        _meshRendererList[1].enabled = state;
        _canvashealth.enabled = state;
    }
}
