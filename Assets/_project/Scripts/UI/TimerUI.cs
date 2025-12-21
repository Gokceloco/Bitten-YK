using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public AudioManager audioManager;
    public Image fillBar;
    public TextMeshProUGUI timerTMP;

    private CanvasGroup _canvasGroup;

    private float _previousTime;
    private float _currentTime;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .2f);
    }
    public void Hide()
    {
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }

    public void SetFillBar(float ratio, int remainingTime)
    {
        fillBar.fillAmount = ratio;
        timerTMP.text = remainingTime.ToString();        

        if (remainingTime <= 5)
        {
            timerTMP.color = Color.red;
            _currentTime = remainingTime;

            if (_previousTime != _currentTime)
            {
                audioManager.PlayTimerAS();
                _previousTime = _currentTime;
            }
        }        
    }
}
