using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeUretmeveDusurme : MonoBehaviour
{
    public GameObject[] dusenObjeler;
    private int obstacleOrder;
    float engelDusmeZamani =0.50f;
    public int dusecekObjeSayisi = 5;


    // player belli bir noktaya geldi�inde alg�lan�p kafas�na obje d���r�lecek
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("k�z geldi");
            StartCoroutine(SpawnObstacle());
        }
        
    }

    //obje belli bir noktada instantiate edilecek ve geri say�m ekranda g�sterilecek
    IEnumerator SpawnObstacle()
    {
        UIControl.Instance.SetObstacleTimer(1f);
        for (int i = 0; i < dusecekObjeSayisi; i++)
        {            
            yield return new WaitForSeconds(engelDusmeZamani);
            GameObject randomObject = dusenObjeler[ObstacleOrder()];
            Instantiate(randomObject, new Vector3(ObstaclePos().x,ObstaclePos().y,ObstaclePos().z+i*10f),randomObject.transform.rotation);
        }

    }

    //Objenin �retilmesi gereken pozisyon ayarlan�yor
    private Vector3 ObstaclePos()
    {
        return new Vector3(transform.position.x, transform.position.y+20, transform.position.z+38);
    }

    // obje havuzundan rasgele bir obje se�ilecek
    private int ObstacleOrder()
    {
        obstacleOrder = Random.Range(0, dusenObjeler.Length);
        return obstacleOrder; 
    }
}
