using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeUretmeveDusurme : MonoBehaviour
{
    public GameObject[] dusenObjeler;
    private int obstacleOrder;
    float engelDusmeZamani =0.50f;
    public int dusecekObjeSayisi = 5;


    // player belli bir noktaya geldiðinde algýlanýp kafasýna obje düþürülecek
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("kýz geldi");
            StartCoroutine(SpawnObstacle());
        }
        
    }

    //obje belli bir noktada instantiate edilecek ve geri sayým ekranda gösterilecek
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

    //Objenin üretilmesi gereken pozisyon ayarlanýyor
    private Vector3 ObstaclePos()
    {
        return new Vector3(transform.position.x, transform.position.y+20, transform.position.z+38);
    }

    // obje havuzundan rasgele bir obje seçilecek
    private int ObstacleOrder()
    {
        obstacleOrder = Random.Range(0, dusenObjeler.Length);
        return obstacleOrder; 
    }
}
