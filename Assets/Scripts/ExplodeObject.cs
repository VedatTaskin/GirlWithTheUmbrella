using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    public float minForce = 100;
    public float maxForce = 300;
    public float radius = 0;
    public float upwardsValue = 2;
    public float destroyDelay = 2;

    public void Explode()
    {
        foreach(Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            Vector3 explosionPos =new Vector3 (UnityEngine.Random.Range(transform.position.x - 2, transform.position.x + 2), transform.position.y, transform.position.z);
            if (rb != null)
            {
                rb.AddExplosionForce(UnityEngine.Random.Range(minForce, maxForce),explosionPos, radius,upwardsValue);
            }
            Destroy(t.gameObject, destroyDelay);
        }
    }
}
