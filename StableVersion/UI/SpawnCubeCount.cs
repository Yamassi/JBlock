using UnityEngine;

public class SpawnCubeCount : MonoBehaviour
{
    private TMPro.TextMeshPro text;
    private Transform textCount;
    private SpawnManager spawnManager;

    void Start()
    {
        text = transform.GetComponent<TMPro.TextMeshPro>();
        spawnManager = FindObjectOfType<SpawnManager>(true);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = spawnManager.Count.ToString();
    }
}
