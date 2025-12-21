using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI msgTMP;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .2f);
    }

    public void ShowMessage(string msg, float delay, float duration)
    {
        StartCoroutine(ShowMessageCoroutine(msg, delay, duration));
    }

    IEnumerator ShowMessageCoroutine(string msg, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        msgTMP.DOFade(1, .2f);
        msgTMP.text = msg;
        msgTMP.DOFade(0,.2f).SetDelay(duration);
    }

    public void Hide()
    {
        msgTMP.DOFade(0,.2f);
    }
}
