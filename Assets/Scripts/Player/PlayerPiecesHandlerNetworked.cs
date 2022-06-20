using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiecesHandlerNetworked : PlayerPiecesHandler
{
    



    public override void OnStartServer()
    {
        CheckersNetworkManager.OnStartGame += HandleGameStarted;
    }

    public override void OnStopServer()
    {
        CheckersNetworkManager.OnStartGame -= HandleGameStarted;
    }


    protected override void Spawn(GameObject prefab, int xPos, int zPos)
    {
        base.Spawn(prefab, xPos, zPos);
        NetworkServer.Spawn(instanceToSpawn, connectionToClient);

    }
}
