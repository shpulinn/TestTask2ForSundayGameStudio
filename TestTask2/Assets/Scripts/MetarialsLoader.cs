using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetarialsLoader : MonoBehaviour
{
    [SerializeField] private Material[] materialsToPreload;

    void Awake ()
    {
        foreach (Material material in materialsToPreload) {
            Resources.Load(material.name, typeof(Material));
        }
    }
}
