using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] GameObject onlinePage;
    [SerializeField] InputField addressInput;

    public void Join()
    {
        NetworkManager.singleton.networkAddress = addressInput.text;
        NetworkManager.singleton.StartClient();
    }

    public void HandleConnected()
    {
        onlinePage.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        CheckersNetworkManager.OnClientConnected += HandleConnected;
    }

    private void OnDestroy()
    {
        CheckersNetworkManager.OnClientConnected -= HandleConnected;
    }
}
