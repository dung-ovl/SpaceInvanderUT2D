using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoaderAndSaver : GameMonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public PlayerData PlayerData => playerData;

    private int currentLevel;

    public int CurrentLevel => currentLevel;

    static DataLoaderAndSaver instance;

    public static DataLoaderAndSaver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataLoaderAndSaver>();
            }
            return instance;
        }
    }

    override protected void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerData();
    }

    override protected void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadPlayerData()
    {
        this.playerData = SaveSystem.LoadPlayerData();
        Debug.Log(playerData.process);
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer(playerData);
    }

    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
    }
}