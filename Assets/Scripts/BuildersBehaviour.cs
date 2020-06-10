using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class BuildersBehaviour : MonoBehaviour
{
    public List<Builder> builders = new List<Builder>();

    public static BuildersBehaviour Instance;

    private IEnumerable coroutine;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }


}
