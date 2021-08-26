using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public GameObject elmasPatlamaFX;
    private GameObject elmasPool;
    float mesafeSliderDeger = 0f;
    float healthDeger = 0f;
    float obstacleTimerValue = 1f;


    private void Start()
    {
        elmasPool= GameObject.FindGameObjectWithTag("ElmasPool");
    }

    void Update()
    {

        //A tuþuna basýn, düþman ölünce effect ve elmas toplama animasyonu 
        if (Input.GetKeyDown(KeyCode.A))
        {
            elmasPool.GetComponent<ElmasToplama>().ElmasTopla(Camera.main.WorldToScreenPoint(transform.position), "GamePlay");
            Destroy(Instantiate(elmasPatlamaFX,
                new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity), 0.3f);            
        }

        //win menu test
        if (Input.GetKeyDown(KeyCode.W))
        {
            UIControl.Instance.WinMenu();

        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UIControl.Instance.SetUmbrellaCount();

        }

        //loose menu test
        if (Input.GetKeyDown(KeyCode.L))
        {
            UIControl.Instance.LooseMenu();
        }

        //slider test
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (mesafeSliderDeger<1)
            {
                mesafeSliderDeger = mesafeSliderDeger + 0.1f;
                UIControl.Instance.SetMesafeSliderValue(mesafeSliderDeger);
            }            
        }


        //Health Heart Slide Test;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (healthDeger < 1)
            {
                healthDeger = healthDeger + 0.33f;
                UIControl.Instance.SetHealthValue(healthDeger);
            }
        }

        //Obstacle Timer Test
        if (Input.GetKeyDown(KeyCode.O))
        {
            UIControl.Instance.SetObstacleTimer(obstacleTimerValue); 
            
        }


        //visual reward test
        if (Input.GetKeyDown(KeyCode.V))
        {
            UIControl.Instance.SetVisualTextReward();
        }

    }
}
