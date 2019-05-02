using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MenuManager : MonoBehaviour {

    NetworkManager netManager;
    public string hotSeatScene;
    public string multiplayerScene;

    public GameObject[] ExtraScreens;
    GameObject DisplayedExtra;



    // Use this for initialization
    void Start () {
        DisplayedExtra = ExtraScreens[0];
        netManager = NetworkManager.singleton;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.L)) {
            TestList();
        };
	}

    public void ChangeExtra(int ex)
    {
        DisplayedExtra.SetActive(false);
        DisplayedExtra = ExtraScreens[ex];
        DisplayedExtra.SetActive(true);
    }

    public void StartHotseat()
    {
        netManager.onlineScene = hotSeatScene;
        netManager.StartHost();
    }

    public void StartLan() {
        NetMode();
        netManager.StartHost();
    }

    public void JoinLan()
    {
        NetMode();
        netManager.StartClient();
    }

    public void TestCreate()
    {
        netManager.StartMatchMaker();
        netManager.matchMaker.CreateMatch("", 2, true, "", "", "", 0, 0, netManager.OnMatchCreate);
    }

    public void TestList() {
        netManager.StartMatchMaker();
        netManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, netManager.OnMatchList);
        Debug.Log("number of matches: " + netManager.matches.Count);
    }

    public void StartOnline()
    {
        NetMode();
        netManager.StartMatchMaker();
        netManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, netManager.OnMatchList);
        GetMatch();
    }

    void GetMatch() {
        if (netManager.matches == null) {
            Debug.Log("Waiting for match list.");
            Invoke("GetMatch", 1);
        } else
        {
            if (netManager.matches.Count == 0)
            {
                Debug.Log("Creating new match");
                netManager.matchMaker.CreateMatch("", 2, true, "", "", "", 0, 0, netManager.OnMatchCreate);
            }
            else
            {
                Debug.Log("Joining match");
                netManager.matchMaker.JoinMatch(netManager.matches[0].networkId, "", "", "", 0, 0, netManager.OnMatchJoined);
            }
        }
    }

    void NetMode()
    {
        netManager.onlineScene = multiplayerScene;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
