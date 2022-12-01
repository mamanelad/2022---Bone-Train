using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : MonoBehaviour
{
    enum RepositoryItems
    {
        Fuel,
        Water,
        Food
    }
    
    [SerializeField] Dictionary<RepositoryItems, int> repDic;
    [SerializeField] private Repository mapRepository;
    private void Awake()
    {
        repDic = new Dictionary<RepositoryItems, int>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool EnoughResources()
    {
        foreach (var item in r)
        {
            
        }
    }
}
