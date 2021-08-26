using System;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour
{
    public float damage = 0.1f;
    public GameObject destroyEffect;
    [SerializeField] bool isEnemy;
    bool isCollided = false;

    [SerializeField] float force = 0.0f;


    public GameObject fracturedObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (fracturedObject != null)
                SpawnFracturedObject();
            else
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isCollided = true;
            if (fracturedObject != null) 
                SpawnFracturedObject();
            else 
                Destroy(gameObject);
        }
        if (!isCollided)
        {
            if (other.gameObject.CompareTag("Umbrella"))
            {
                isCollided = true;
                Debug.Log("Umbrella");
                UIControl.Instance.SetVisualTextReward();
                if (destroyEffect != null)
                    Instantiate(destroyEffect, transform.position, Quaternion.identity);
                if(other.gameObject.GetComponent<Animator>() != null)
                    other.gameObject.GetComponent<Animator>().SetTrigger("Bounce");

                if (isEnemy)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SpeedUp();

                    GetComponent<ChaseSystem>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                    //Vector3 way = (transform.position - other.transform.position).normalized;

                    //GetComponent<EnemyRagdoll>().AddForce(way * force);
                    GetComponent<EnemyRagdoll>().DoRagdol();

                    Destroy(gameObject, 3);
                }
                else
                {
                    GetComponent<Collider>().isTrigger = false;
                        /*
                    if (fracturedObject != null) { SpawnFracturedObject(); }
                    else { Destroy(gameObject); }*/
                }

            }
            else if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);              
                if (fracturedObject != null) { SpawnFracturedObject(); }
                //else { Destroy(gameObject); }
                CinemachineShake.instance.ShakeCamera(1.2f, 0.3f);
                UIControl.Instance.ShowNegativeEmoji();
            }
        }
    }

    private void AddForce(Vector3 tr)
    {
        Rigidbody[] AllRBs = GetComponentsInChildren<Rigidbody>(true);

        Vector3 way = (transform.position - tr).normalized;

        foreach (var rb in AllRBs)
        {
            rb.AddForce(way * force * rb.mass);
        }
    }

    private void SpawnFracturedObject()
    {
        if (destroyEffect != null)
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        GameObject fractObj = Instantiate(fracturedObject,transform.position,transform.rotation) as GameObject;
        fractObj.GetComponent<ExplodeObject>().Explode();
        Destroy(gameObject);
        Destroy(fractObj,3);
    }
}
