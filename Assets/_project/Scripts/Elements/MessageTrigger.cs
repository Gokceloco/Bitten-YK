using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public string msg;
    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<Level>().ShowMessage(msg, duration);
            gameObject.SetActive(false);
        }
    }
}
