using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    public GameObject elmasPatlamaFX;
    private GameObject elmasPool;

    private void Awake()
    {
        elmasPool = GameObject.FindGameObjectWithTag("ElmasPool");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Umbrella"))
        {
            
            elmasPool.GetComponent<ElmasToplama>().ElmasTopla(Camera.main.WorldToScreenPoint(transform.position), "GamePlay");
            Destroy(Instantiate(elmasPatlamaFX,
                new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity), 0.3f);
            Destroy(gameObject);

        }
    }

}
