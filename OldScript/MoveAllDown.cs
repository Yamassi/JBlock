
using System.Collections.Generic;
using UnityEngine;

public class MoveAllDown : MonoBehaviour
{
    private DestroyTrigger destroyTrigger;
    public List<GameObject> itemsForDown;
    private WallController wallController;
    private void Start()
    {
        destroyTrigger = GameObject.FindObjectOfType<DestroyTrigger>();
        wallController = GameObject.FindObjectOfType<WallController>();
    }
    private void Update()
    {

        if (destroyTrigger.IsDestroyed)
        {

            for (int i = 0; i < itemsForDown.Count; i++)
            {

                CubeController cubeController = itemsForDown[i].GetComponent<CubeController>();
                cubeController.MoveDownOnOneStep();
                if (cubeController.transform.position.y == 0)
                {
                    itemsForDown.Remove(itemsForDown[i]);
                }

            }
            wallController.RemoveDestroyedInDictionary(itemsForDown);
            destroyTrigger.IsDestroyed = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (!itemsForDown.Contains(other.gameObject))
        {
            itemsForDown.Add(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
