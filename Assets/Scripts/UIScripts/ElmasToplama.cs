using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// elmas havuzu oluþturuluyor 20 tane olacak þekilde
// daha sonra bu elmaslarýn instantiate edileceði ve sürüklenip yok edileceði noktalarýn pozisyon bilgileri ile çaðrýlýyor. 


public class ElmasToplama : MonoBehaviour
{
    public GameObject elmasImagePrefab;
    public int UretilecekElmasImageSayisi; 
    List<GameObject> elmasImages = new List<GameObject>();
    int counter = 0; //  kaçýncý sýradaki elmastayýz.
    Sequence elmasAnimation;
    public Transform GamePlayElmasCountPosition;
    public Transform WinMenuElmasCountPosition;
    private Vector3 targetPos; // elmaslarýn sürükleneceði poziyon


    // Ýstenen sayýda Elmas üretiyoruz baþlangýçta
    void Start()
    {
        for (int i = 0; i < UretilecekElmasImageSayisi; i++)
        {
            GameObject elmasImage = Instantiate(elmasImagePrefab, transform.position, Quaternion.identity,transform.parent);
            elmasImage.SetActive(false);
            elmasImages.Add(elmasImage);
        }
    }


    //Havuzdaki elmaslar sýra ile verilen pozisyonda instantiate ediliyor, target pozisyona sürüklenecek
    public void ElmasTopla (Vector3 currentPos, string callPlace)
    { 

        if (counter == UretilecekElmasImageSayisi-1)
        {            
            counter = 0;
        }
        if (callPlace == "WinMenu")
        {
            targetPos= new Vector3(WinMenuElmasCountPosition.position.x-50, WinMenuElmasCountPosition.position.y-30,WinMenuElmasCountPosition.position.z);
        }
        if (callPlace == "GamePlay")
        {
            targetPos = new Vector3(GamePlayElmasCountPosition.position.x-70, GamePlayElmasCountPosition.position.y - 60, GamePlayElmasCountPosition.position.z);
        }

        elmasImages[counter].transform.position = currentPos;
        elmasImages[counter].SetActive(true);
        ElmasToplamaAnimasyonu(counter,targetPos);
        counter++;        
    }

    //Elmaslar target pozisyona sürüklenip yok ediliyor ve sayýsý bir artýyor.
    private void ElmasToplamaAnimasyonu(int i,Vector3 targetPos)
    {
        elmasAnimation = DOTween.Sequence();

        elmasAnimation.Append(elmasImages[i].transform.DOMove(targetPos, 1f)
            .SetEase(Ease.InFlash))
            .Join(elmasImages[i].transform.DOScale(Vector3.one * 0.5f, 1f))
            .OnComplete(() =>
            {                
                UIControl.Instance.SetElmasCount();               
                elmasImages[i].SetActive(false);
                elmasImages[i].transform.DOScale(Vector3.one * 2, 1);                
            });
    }
}
