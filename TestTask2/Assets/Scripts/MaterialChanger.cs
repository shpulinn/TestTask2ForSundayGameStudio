using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private List<Material> materials = new List<Material>();

    private MeshRenderer _meshRenderer;
    private int _currentMaterialIndex;

    private void Awake ()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _currentMaterialIndex = 0;

        _meshRenderer.material = materials[_currentMaterialIndex];
    }

    private void OnMouseDown ()
    {
        _currentMaterialIndex++;
        _currentMaterialIndex = (_currentMaterialIndex + materials.Count) % materials.Count;
        _meshRenderer.material = materials[_currentMaterialIndex];
    }
}
