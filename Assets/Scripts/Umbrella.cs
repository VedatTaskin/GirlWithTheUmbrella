using UnityEngine;

public class Umbrella : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy")) { 
            Destroy(other.gameObject);
            Debug.Log("þemsiye");
        }
    }
}
