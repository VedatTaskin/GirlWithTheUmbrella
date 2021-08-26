using UnityEngine;
using UnityEngine.AI;

public class ChaseSystem : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    GameObject target;
    float distance;
    public string targetTag;
    [SerializeField] float attackStartDistance = 10;
    private bool isStopWalkingAnim = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(targetTag);
    }


    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackStartDistance && isStopWalkingAnim == false)
        {
            agent.SetDestination(target.transform.position);
            animator.Play("Walking");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GetComponent<NavMeshAgent>().isActiveAndEnabled)
        {
            agent.isStopped = true;
            isStopWalkingAnim = true;
            animator.Play("Idle");
            Invoke(nameof(LookTarget), 0.3f);
            Destroy(gameObject, 3);
        }
    }

    void LookTarget()
    {
        transform.LookAt(target.transform);
    }
}
