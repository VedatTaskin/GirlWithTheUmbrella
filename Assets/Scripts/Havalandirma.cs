using UnityEngine;
using DG.Tweening;

public class Havalandirma : MonoBehaviour
{

    Sequence seq;
    Transform transOtherGO;
    public float ziplamaYuksekligi = 20;
    public float ziplamaMesafesi = 40;
    [SerializeField] float doingTime = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        
        seq = DOTween.Sequence();
        if (other.gameObject.CompareTag("Player"))
        {
            transOtherGO = other.gameObject.transform;
            other.GetComponent<Animator>().SetBool("isHanging", true);
            other.gameObject.GetComponent<PlayerMovement>().SetCanMove(false);

            seq.Append(transOtherGO.DOMoveZ(transOtherGO.position.z + ziplamaMesafesi, doingTime))
                .Join(transOtherGO.DOMoveY(transOtherGO.position.y + ziplamaYuksekligi, doingTime))
                 //.Join(transOtherGO.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd))
                 .OnComplete(() =>
                 {
                     //seq.Append(transOtherGO.DOMoveY(transOtherGO.position.y, 1));
                     //.Join(transOtherGO.DOMoveZ(transOtherGO.position.z +25, 1));
                     other.gameObject.GetComponent<PlayerMovement>().SetCanMove(true);
                 });
            

        }
    }
}
