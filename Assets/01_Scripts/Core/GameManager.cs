using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolListSO _poolList;

    private void Awake()
    {
        //dynamicManager
        CreatePoolManager();
    }

    #region CreateManager
    private void CreatePoolManager()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolList.poolList.ForEach(p =>
        {
            PoolManager.Instance.CreatePool(p.PoolObj, p.PoolCnt);
        });
    }
    #endregion
}
