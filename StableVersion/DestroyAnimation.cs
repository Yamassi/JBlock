using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, -0.1f, 0f);
    public void DestroyAnimationFX()
    {
        StartCoroutine(DestroyPieces());
    }
    IEnumerator DestroyPieces()
    {
        yield return new WaitForSeconds(1f);
        transform.position += offset;
        yield return new WaitForSeconds(0.01f);
        transform.position += offset;
        yield return new WaitForSeconds(0.01f);
        transform.position += offset;
        yield return new WaitForSeconds(0.01f);
        transform.position += offset;
        yield return new WaitForSeconds(0.01f);
        transform.position += offset;
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
