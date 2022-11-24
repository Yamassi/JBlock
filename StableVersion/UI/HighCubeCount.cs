using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighCubeCount : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    private Transform textCount;
    private WallCollider wallCollider;
    void Start()
    {
        text = transform.GetComponent<TMPro.TextMeshProUGUI>();
        wallCollider = FindObjectOfType<WallCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = wallCollider.HighCubeCount.ToString() + " Left";
    }
}
