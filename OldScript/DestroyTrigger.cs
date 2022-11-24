
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private bool isDestroyed = false;
    public bool IsDestroyed
    {
        get { return isDestroyed; }
        set { isDestroyed = value; }
    }
    public List<GameObject> items;

    private void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (!items.Contains(other.gameObject))
        {
            items.Add(other.gameObject);
        }



        if (items.Count >= 9)
        {

            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            // wallController.RemoveDestroyedInDictionary(items);
            // items.Clear();
            isDestroyed = true;

        }

    }
    void OnTriggerExit(Collider other)
    {
        items.Remove(other.GetComponent<CubeController>().gameObject);
    }

}
