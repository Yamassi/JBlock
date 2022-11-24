
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private Transform wall;
    private Dictionary<int, GameObject> gameGrid;
    public Dictionary<int, GameObject> GameGrid
    {
        get { return gameGrid; }
        set { gameGrid = value; }
    }


    private void Start()
    {
        wall = transform;
        gameGrid = new Dictionary<int, GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<CubeController>())
        {
            CubeController cubeController = other.GetComponent<CubeController>();
            var stepAmount = 0;
            var cubeYposition = other.transform.position.y;
            if (cubeController.isStatic)
            {
                gameGrid.Add(Mathf.RoundToInt(cubeYposition), other.gameObject);
            }
            else if (!cubeController.isStatic)
            {
                stepAmount = FindFreeCell(cubeYposition, gameGrid);

                gameGrid.Add(stepAmount, other.gameObject);

                cubeController.MoveUpOnStep(stepAmount);
            }

        }

    }
    int FindFreeCell(float cubeYposition, Dictionary<int, GameObject> gameGrid)
    {
        if (gameGrid != null)
        {
            var gridAmount = gameGrid.Count;
            if (gridAmount == 0)
            {
                return gridAmount;
            }
            else if (gridAmount != 0)
            {

                for (int i = 0; i < gridAmount; i++)
                {
                    if (!gameGrid.ContainsKey(i))
                    {
                        return i;
                    }
                }
                return gridAmount;
            }
            else
            {
                return 0;
            }

        }
        else
        {
            return 0;
        }
    }

    public void RemoveDestroyedInDictionary(List<GameObject> items)
    {
        List<int> list = new List<int>();
        foreach (var item in items)
        {
            foreach (var cube in gameGrid)
            {
                if (gameGrid.ContainsValue(item))
                {
                    list.Add(cube.Key);
                }
            }
        }
        foreach (var item in list)
        {
            gameGrid.Remove(item);
        }

    }
}
