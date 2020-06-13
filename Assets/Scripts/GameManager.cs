using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _istance;

    public static GameManager Instance
    {
        get
        {
            if(_istance == null)
                Debug.Log("Game Manager null Instance");

            return _istance;
        }
    }

    public static List<Builder> builders;

    [SerializeField] private int BuilderCount;
    [SerializeField] private GameObject BuilderPrefab;
    [SerializeField] private int poolObjectCount;
    private GameObject BuilderContainer;

    //Grid Initialization
    private Grid grid = new Grid(64, 64, 1f);
    static int count = 0;

    void Awake()
    {
        _istance = this;

        BuilderContainer = GameObject.Find("Builders");
        builders = new List<Builder>();
        for(int i = 0; i < BuilderCount; i++)
        {
            SpawnBuilder(new Vector2(0f, 0f));
        }
        
    }
    private void Start()
    {
        PoolingManager.Instance.GenerateBlocks(poolObjectCount, 30);
    }
    private void SpawnBuilder(Vector2 pos)
    {
        count++;
        var builder = Instantiate(BuilderPrefab, pos, Quaternion.identity);
        builder.name = "B" + count.ToString();
        builder.transform.SetParent(BuilderContainer.transform);
    }
}
