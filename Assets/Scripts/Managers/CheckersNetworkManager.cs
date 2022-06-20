using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckersNetworkManager : NetworkManager
{
    [SerializeField] GameObject gameOverHandlerPrefab, boardPrefab, 
        turnsHandlerPrefab;
    public static event Action OnClientConnected;
    public static event Action OnStartGame;
    public List<Player> playersList = new List<Player>();

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        OnClientConnected?.Invoke();
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        if (newSceneName.StartsWith("G"))  //Equals("полное название")
        {
            OnStartGame?.Invoke();
        }
    }

    public override void OnStartServer()
    {
        GameObject gOHLP = Instantiate(gameOverHandlerPrefab);
        DontDestroyOnLoad(gOHLP);
        NetworkServer.Spawn(gOHLP);
        GameObject bP = Instantiate(boardPrefab);
        NetworkServer.Spawn(bP);
        GameObject tHLP = Instantiate(turnsHandlerPrefab);
        NetworkServer.Spawn(tHLP);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject pl = Instantiate(playerPrefab);
        PlayerNetwork playNetwork = pl.GetComponent<PlayerNetwork>();
        playNetwork.IsWhite = numPlayers == 1;
        playNetwork.displayName = playNetwork.IsWhite ? "Белый" : "Чёрный";
        playersList.Add(playNetwork);
        NetworkServer.AddPlayerForConnection(conn, pl);

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        var player = conn.identity.GetComponent<PlayerNetwork>();
        playersList.Remove(player);
    }

    public override void OnStopServer()
    {
        playersList.Clear();
    }

}
