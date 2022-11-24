using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDestroy : MonoBehaviour
{

    public void DestroyFXAfter(int seconds)
    {
        var FX = GetComponent<ParticleSystem>();
        StartCoroutine(DestroyFX(FX, seconds));
    }
    IEnumerator DestroyFX(ParticleSystem FX, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(FX.gameObject);
    }
}
