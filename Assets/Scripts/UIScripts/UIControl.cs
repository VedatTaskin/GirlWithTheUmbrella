using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIControl : MonoBehaviour
{
    private static UIControl instance;
    public static UIControl Instance { get => instance; set => instance = value; }


    [Header("GamePlay variables")]
    [SerializeField] GameObject gamePlayMenu;
    [SerializeField] Slider mesafeSlider;
    [SerializeField] Image health;
    [SerializeField] GameObject obstacleTimer ;
    [SerializeField] Image obstacleTimerImageForFill;
    [SerializeField] TextMeshProUGUI gamePlayElmasCountText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI umbrellaCountText;
    [SerializeField] TextMeshProUGUI visualTextReward;
    private string[] visualTextRewards = new string [] { "Awesome", "Perfect", "Good", "Fantastic", "Crazy", "Don't Stop", "Go go go" };
    [SerializeField] Sprite [] positiveEmojies;
    [SerializeField] Sprite[] negativeEmojies;
    [SerializeField] GameObject emojiGO;

    [Header("LooseMenu variables")]
    [SerializeField] GameObject looseMenu;
    [SerializeField] Text looseMenuElmasCountText;

    [Header("WinMenu variables")]
    [SerializeField] GameObject winMenu;
    [SerializeField] Text winMenuElmasCountText;


    [SerializeField] float etapUzunlugu;
    [SerializeField] Transform playerTransform;
    private int umbrellaCount = 0;

    private int elmasScore = 0;    
    private int rewardCount=0;
    Sequence visualRewardAnimation;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        elmasScore= PlayerPrefs.GetInt("ElmasScore");
        gamePlayElmasCountText.text = elmasScore.ToString();
        obstacleTimerImageForFill.fillAmount = 0f;
        obstacleTimer.gameObject.SetActive(false);
    }

    private void MesafeSliderGuncelle()
    {
        SetMesafeSliderValue(playerTransform.position.z / etapUzunlugu);
    }

    internal void SetMesafeSliderValue(float value)
    {
        mesafeSlider.value = value;
        mesafeSlider.maxValue = 1f;
    }

    internal void SetHealthValue(float value)
    {
        health.fillAmount = 1-value;
    }

    internal void SetObstacleTimer(float value)
    {
        if (!obstacleTimer.gameObject.activeInHierarchy)
        {
            obstacleTimer.gameObject.SetActive(true);
            StartCoroutine(WaitForObstacleTimer(value));
        }
        
    }

    internal void SetElmasCount()
    {
        if (gamePlayMenu.activeInHierarchy)
        {
            elmasScore++;
            gamePlayElmasCountText.text = elmasScore.ToString();
        }
        if (winMenu.activeInHierarchy)
        {
            winMenuElmasCountText.text = elmasScore.ToString();
            elmasScore++;            
        }

        PlayerPrefs.SetInt("ElmasScore", elmasScore);        
    }

    internal void SetVisualTextReward()
    {
        StartCoroutine(WaitForVisualTextReward());
    }

    internal void ShowPositiveEmoji()
    {
        emojiGO.GetComponent<Image>().sprite = positiveEmojies[Random.Range(0, positiveEmojies.Length)];
        StartCoroutine(WaitForShowingEmoji());
    }

    internal void ShowNegativeEmoji()
    {
        emojiGO.GetComponent<Image>().sprite = negativeEmojies[Random.Range(0, negativeEmojies.Length)];
        StartCoroutine(WaitForShowingEmoji());
    }

    internal void SetLevelText(int seviye)
    {
        seviye = seviye + 1;
        levelText.text = seviye.ToString();
    }

    internal void SetUmbrellaCount()
    {
        umbrellaCount = umbrellaCount + 1;
        umbrellaCountText.text = umbrellaCount.ToString();
    }

    internal void LooseMenu()
    {        
        StartCoroutine(WaitForLooseMenu());
    }

    internal void WinMenu()
    {
        StartCoroutine(WaitForWinMenu());        
    }

    IEnumerator WaitForLooseMenu()
    {
        yield return new WaitForSeconds(1.5f);
        
        looseMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
        looseMenuElmasCountText.text= PlayerPrefs.GetInt("ElmasScore").ToString();

    }

    IEnumerator WaitForWinMenu()
    {
        yield return new WaitForSeconds(4f);
        winMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
        winMenuElmasCountText.text = PlayerPrefs.GetInt("ElmasScore").ToString();
    }

    IEnumerator WaitForVisualTextReward()
    {
        rewardCount++;
        if (rewardCount<3)
        {
            visualTextReward.gameObject.SetActive(true);
            visualTextReward.text = visualTextRewards[(int)Random.Range(0, visualTextRewards.Length)];
            visualRewardAnimation = DOTween.Sequence();
            visualRewardAnimation.Append(visualTextReward.transform.DOShakePosition(1, 5, 5, 2))
                .SetEase(Ease.InFlash);
            yield return new WaitForSeconds(0.5f);
            visualTextReward.gameObject.SetActive(false);
        }
        else
        {
            ShowPositiveEmoji();
            rewardCount = 0;
        }
        
    }

    IEnumerator WaitForObstacleTimer(float value)
    {
        
        while (value > 0)
        {
            yield return new WaitForSeconds(0.05f);
            obstacleTimerImageForFill.fillAmount = value;
            value -= 0.05f; 
        }
        obstacleTimer.gameObject.SetActive(false);
    }

    IEnumerator WaitForShowingEmoji()
    {        
        emojiGO.gameObject.SetActive(true);        
        emojiGO.transform.DOScale(1.5f,1).SetEase(Ease.InOutBounce);
        yield return new WaitForSeconds(0.5f);
        emojiGO.transform.DORewind();
        emojiGO.gameObject.SetActive(false);
    }



}
