using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private static GameManager _istance;

    public static GameManager Instance
    {
        get
        {
            if (_istance == null)
                Debug.Log("Game Manager null Instance");

            return _istance;
        }
    }

    public List<Builder> builders;

    [SerializeField] private int BuilderCount;
    [SerializeField] private GameObject BuilderPrefab;
    [SerializeField] private int poolObjectCount;
    [SerializeField] private List<GameObject> _bHomes;
    private GameObject BuilderContainer;

    //Grid Initialization
    private Grid grid = new Grid(64, 64, 1f);
    static int count = 0;

    void Awake()
    {
        _istance = this;
        
    }
    private void Start()
    {
        PoolingManager.Instance.GenerateBlocks(poolObjectCount, 200);
        foreach(var home in _bHomes)
        {
            var builder = SpawnBuilder();
            var builderID = builder.GetComponent<Builder>().GetID();
            foreach(var h2 in _bHomes)
            {
                if(builderID == h2.GetComponent<BuildersHome>()._builderID)
                {
                    builder.GetComponent<Builder>()._home = h2;
                    builder.transform.position = h2.transform.position;
                }
            }
        }

    }
    private GameObject SpawnBuilder()
    {
        count++;
        var builder = Instantiate(BuilderPrefab, Vector2.zero, Quaternion.identity);
        builder.name = "B" + count.ToString();
        return builder;
    }

    
}
