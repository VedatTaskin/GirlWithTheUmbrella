using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    [SerializeField] GameObject waterSplash;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(waterSplash, other.transform);
    }
}
