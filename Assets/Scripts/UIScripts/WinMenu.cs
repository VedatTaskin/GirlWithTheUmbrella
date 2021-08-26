using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinMenu : MonoBehaviour
{

    private float range;  // elmaslarýn menude instantiate edilme rasgeleliði
    public int rewardCoin=20; // verilecek elmas ödülü 
    private GameObject elmasPool;
    public Text rewardCoinText;

    private void Awake()
    {
        rewardCoinText.text = rewardCoin.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {       
        elmasPool = GameObject.FindGameObjectWithTag("ElmasPool");
        StartCoroutine(ElmasToplamaTekrarla(rewardCoin));        
    }
 

    IEnumerator ElmasToplamaTekrarla(int length)
    {
        for (int i = 0; i < length+1; i++)
        {
            range = Random.Range(-500, 500);
            Vector3 rastgelePos = transform.position + Vector3.one * range;            
            yield return new WaitForSeconds(0.05f);
            elmasPool.GetComponent<ElmasToplama>().ElmasTopla(rastgelePos, "WinMenu");
            rewardCoinText.text = rewardCoin--.ToString();
        }        
    }
}
