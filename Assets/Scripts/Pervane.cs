using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pervane : MonoBehaviour
{

    Vector3 rot = new Vector3(0, 360, 0);

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(rot, 2f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
    }
}
