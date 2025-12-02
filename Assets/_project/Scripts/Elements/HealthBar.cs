using DG.Tweening;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fillBarParent;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetFillBar(float ratio)
    {
        //fillBarParent.localScale = new Vector3(ratio, 1, 1);

        fillBarParent.DOKill();
        fillBarParent.DOScaleX(ratio, .2f);

        if (ratio <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
