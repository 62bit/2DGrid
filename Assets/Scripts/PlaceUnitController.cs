using System;
using System.Collections.Generic;
using UnityEngine;


public class PlaceUnitController : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject tempSelectionBlock;
    [SerializeField] private KeyCode hotKey;

    public List<GameObject> objectList;

    private GameObject TreeContainer;

    private void Awake()
    {
        TreeContainer = GameObject.Find("Trees");

    }

    public static void PassToBuilders(List<Vector2> units)
    {
        int unitsPerBuilder = Math.Abs(units.Count / BuilderID.ReturnID());
        int counter = 0;
        foreach (var builder in GameManager.builders)
        {
            if (GameManager.builders.IndexOf(builder) != GameManager.builders.Count - 1)
            {
                for (int i = 0; i < unitsPerBuilder; i++)
                {
                    builder.units.Enqueue(units[counter]);
                    counter++;
                }
            }
            else
            {
                int lastUnits = units.Count - counter;

                for (int i = 0; i < lastUnits; i++)
                {
                    builder.units.Enqueue(units[counter]);
                    counter++;
                }
            }
        }
    }

    public void PlaceObject(Vector2 pos)
    {
        var tree = Instantiate(placeableObjectPrefab, pos, Quaternion.identity);
        Grid.Instance.gridObjects.Add(tree, pos);
        tree.transform.SetParent(TreeContainer.transform);
    }
    
    public void PlaceObjectTemp(Vector2 pos)
    {
        var temp = Instantiate(tempSelectionBlock, pos, Quaternion.identity);
    }
}
