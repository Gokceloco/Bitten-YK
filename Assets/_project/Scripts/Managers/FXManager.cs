using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem potionCollectedPS;
    public ParticleSystem zombieImpactPS;

    public void PlayPotionCollectedPS(Vector3 pos)
    {
        var newPS = Instantiate(potionCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
    public void PlayZombieImpactPS(Vector3 pos, Vector3 dir)
    {
        var newPS = Instantiate(zombieImpactPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos + dir);
        newPS.Play();
    }
}
