using System.Collections;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Collider[] AllColliders;

    [SerializeField] Rigidbody[] rigs;

    bool isWorkingAboutRagdoll;

    Collider mainCollider;

    Animator anim;

    void Start()
    {
        isWorkingAboutRagdoll = false;
        mainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        anim = GetComponent<Animator>();
        DoRagdoll(true);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        if (!isWorkingAboutRagdoll)
        {
            isWorkingAboutRagdoll = true;
            StartCoroutine(DoRagdollAsync(isRagdoll));
        }
    }

    IEnumerator DoRagdollAsync(bool isRagdoll)
    {
        mainCollider.enabled = !isRagdoll;
        foreach (var col in AllColliders)
        {
            yield return new WaitForSeconds(0.01f);
            col.enabled = isRagdoll;
        }
        foreach (var rig in rigs)
        {
            rig.useGravity = isRagdoll;
            rig.isKinematic = !isRagdoll;
        }
        isWorkingAboutRagdoll = false;
        anim.enabled = !isRagdoll;
    }
}
