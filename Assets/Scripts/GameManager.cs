using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int BuilderCount;
    [SerializeField] private GameObject BuilderPrefab;
    [SerializeField] private int poolObjectCount;
    private GameObject BuilderContainer;
    public static List<Builder> builders;
    static int count = 0;
    void Awake()
    {
        BuilderContainer = GameObject.Find("Builders");
        builders = new List<Builder>();
        for (int i = 0; i < BuilderCount; i++)
        {
            SpawnBuilder(new Vector2(0f, 0f));
        }
        PoolingManager.Instance.GenerateBlocks(poolObjectCount);
    }

    private void SpawnBuilder(Vector2 pos)
    {
        count++;
        var builder = Instantiate(BuilderPrefab, pos, Quaternion.identity);
        builder.name = "B" + count.ToString();
        builder.transform.SetParent(BuilderContainer.transform);
    }
}
