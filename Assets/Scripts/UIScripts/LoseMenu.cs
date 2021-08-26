using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{

    public Text coinCountText;


    // Start is called before the first frame update
    void Start()
    {
        coinCountText.text = PlayerPrefs.GetInt("ElmasScore").ToString();
    }
}
