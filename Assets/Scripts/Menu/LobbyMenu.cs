using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    PlayerNetwork[] pn;
    [SerializeField] Button startGameButton;
    [SerializeField] Text[] playerNameTexts = new Text[2];

    public void UpdateName()
    {
        pn = FindObjectsOfType<PlayerNetwork>();
        for (int i = 0; i < pn.Length; i++)
        {
            playerNameTexts[i].text = pn[i].displayName;
        }
    }

    public void NextScene()
    {
        pn = FindObjectsOfType<PlayerNetwork>();
        foreach (PlayerNetwork pln in pn)
        {
            pln.SetReady();
        }
        //((CheckersNetworkManager)NetworkManager.singleton).CheckingReady();
    }

}
