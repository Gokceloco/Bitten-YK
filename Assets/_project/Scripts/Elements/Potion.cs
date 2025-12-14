using DG.Tweening;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 1, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
