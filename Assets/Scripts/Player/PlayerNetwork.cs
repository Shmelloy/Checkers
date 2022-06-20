using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Player
{
    [SyncVar]bool playerReady;
    public bool isReady
    {
        get { return playerReady; }
    }
    [SyncVar(hook = nameof(HandleNameUpdate))] string name;

    public string displayName
    {
        get { return name; }
        [Server]
        set { name = value; }
    }

    void HandleNameUpdate(string oldName, string newName)
    {
        LobbyMenu lbMenu = FindObjectOfType<LobbyMenu>();
        lbMenu.UpdateName();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void SetReady()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        PlTrueSet();
    }

    [Command]
    void PlTrueSet()
    {
        playerReady = true;
        PlayerNetwork[] pn;
        pn = FindObjectsOfType<PlayerNetwork>();
        foreach (PlayerNetwork pln in pn)
        {
            if (!pln.isReady)
            {
                return;
            }
        }
        NetworkManager.singleton.ServerChangeScene("Game Scene");
    }

    [Command]
    public void NextTurn()
    {
        TurnsHandler.Instance.NextTurn();
    }
}
