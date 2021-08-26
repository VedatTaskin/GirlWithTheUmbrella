using UnityEngine;

public class DusenCam : MonoBehaviour
{
    public GameObject SemsiyeyeCarpmaEfekti;
    public GameObject KafayaCarpmaEfekti;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Umbrella")
        {
            Destroy(Instantiate(SemsiyeyeCarpmaEfekti, transform.position, Quaternion.identity),1);                        
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(Instantiate(KafayaCarpmaEfekti, transform.position, Quaternion.identity), 1);
        }

        Destroy(this.gameObject, 0.5f);
    }
}
