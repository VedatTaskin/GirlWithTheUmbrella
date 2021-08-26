using System.Collections;
using UnityEngine;
using Cinemachine;
public class FinisLine : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera FirstCamera;
    [SerializeField] CinemachineVirtualCamera SecondCamera;
    [SerializeField] CinemachineVirtualCamera ThirdCamera;
    [SerializeField]
    GameObject dancingGirl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(DanceAsync(other.gameObject));
    }

    IEnumerator DanceAsync(GameObject other)
    {
        other.GetComponent<Animator>().SetBool("isHanging", false);

        yield return new WaitForSeconds(1.5f);

        GameObject finalGirl = Instantiate(dancingGirl, other.transform.position, Quaternion.Euler(Vector3.zero));
        CalculateScore(other.transform.position.z);
        FirstCamera.Follow = finalGirl.transform;
        FirstCamera.LookAt = finalGirl.transform;
        Destroy(other);
        SecondCamera.Follow = finalGirl.transform;
        SecondCamera.LookAt = finalGirl.transform;
        SecondCamera.Priority = 12;
        yield return new WaitForSeconds(2f);
        ThirdCamera.Follow = finalGirl.transform;
        ThirdCamera.LookAt = finalGirl.transform;
        ThirdCamera.Priority = 13;
    }

    private void CalculateScore(float z)
    {
        int score = (int)(z - transform.position.z);
    }
}
