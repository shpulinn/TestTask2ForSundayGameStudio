using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
