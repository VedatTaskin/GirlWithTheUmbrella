using System.Collections;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    Rigidbody[] AllRigidbodys;

    void Start()
    {
        AllRigidbodys = GetComponentsInChildren<Rigidbody>(true);
    }

    internal void DoRagdol()
    {
        StartCoroutine(DoRagdollAsync());
    }

    internal void AddForce(Vector3 tr)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        tr.z *= 5;
        tr.y = 0;
        rb.AddForce(tr * rb.mass, ForceMode.Impulse);
    }

    IEnumerator DoRagdollAsync()
    {
        yield return new WaitForSeconds(0.01f);

        GetComponent<Animator>().enabled = false;
        foreach (Rigidbody rigidbody in AllRigidbodys)
        {
            rigidbody.isKinematic = false;
        }
    }
}
