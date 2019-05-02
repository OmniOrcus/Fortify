using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class SceneSwitcher : MonoBehaviour {

    public int CurrentScene;
    public int MenuScene;

    public void Reload()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void Leave()
    {
        //SceneManager.LoadScene(MenuScene);
        if (NetworkManager.singleton.matchMaker != null)
        {
            MatchInfo match = NetworkManager.singleton.matchInfo;
            NetworkManager.singleton.matchMaker.DropConnection(match.networkId, match.nodeId, 0, NetworkManager.singleton.OnDropConnection);
        }
        Debug.Log("Stopping Client and Host");
        NetworkManager.singleton.StopHost();

    }

}
