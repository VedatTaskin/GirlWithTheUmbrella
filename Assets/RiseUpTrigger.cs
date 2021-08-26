using UnityEngine;

public class RiseUpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Animator>().SetBool("isHanging", false);
    }
}
