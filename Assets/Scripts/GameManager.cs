using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int BuilderCount;
    [SerializeField] private GameObject BuilderPrefab;
    public static List<Builder> builders;

    void Awake()
    {
        builders = new List<Builder>();
        for (int i=0; i<BuilderCount; i++)
        {
            SpawnBuilder(new Vector2(0f, 0f));
        }
    }


    private void SpawnBuilder(Vector2 pos)
    {
        Instantiate(BuilderPrefab, pos, Quaternion.identity);
    }
}
