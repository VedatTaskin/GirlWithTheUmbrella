using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class LevelSonu : MonoBehaviour
{

    public GameObject confettiPrefab;
    public Transform puanliYolBaslangici;
    public GameObject levelSonuMagicPrefab;
    public GameObject puanliYol;
    

    [SerializeField]
    Ease easeType = Ease.OutBounce;

    Sequence seq;
    int dusulecekYerIndex;
    Transform dusulecekYer;

    private void Start()
    {
        dusulecekYerIndex = Random.Range(5, puanliYol.gameObject.transform.childCount);
        dusulecekYer = puanliYol.gameObject.transform.GetChild(dusulecekYerIndex); 
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetBool("isHanging", true);
            other.gameObject.GetComponent<PlayerMovement>().SetCanMove(false);

            AnimationSequence(other.gameObject);
            GameObject levelSonuMagic = Instantiate(levelSonuMagicPrefab, other.transform.position, Quaternion.identity);
            levelSonuMagic.transform.SetParent(other.transform); 
        }
        dusulecekYer.GetComponentInChildren<Text>().color = Color.red;
    }

    private void AnimationSequence(GameObject otherGO)
    {
        seq = DOTween.Sequence();

            seq.Append(otherGO.transform.DOMove(new Vector3(dusulecekYer.transform.position.x, dusulecekYer.transform.position.y+3, 
                dusulecekYer.transform.position.z), 4))
            .SetEase(easeType)

            .OnComplete(() =>
            {
                Confetti(otherGO.transform.position);
            });
    }

    void Confetti(Vector3 Pos)
    {
        Destroy(Instantiate(confettiPrefab, Pos, Quaternion.identity),3);
        UIControl.Instance.WinMenu();
    }

}

