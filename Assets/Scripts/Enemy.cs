using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 0.1f;
    public float forcePower = 10f;
    public GameObject parcaliAdamPrefab;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Umbrella"))
        {
            Debug.Log("Umbrella");
            Destroy(gameObject);
            Instantiate(parcaliAdamPrefab, transform.position, Quaternion.identity);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);
            Debug.Log("Player");            
        }
        if (other.gameObject.CompareTag("sea"))
        {            
            Debug.Log("suya düþtüm");
        }

    }
}
