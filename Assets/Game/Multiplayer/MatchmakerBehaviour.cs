using UnityEngine;
using System.Collections;

public class MatchmakerBehaviour : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        //PhotonNetworkingMessage

	}
	void OnGUI () {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        print("Can't join random room");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        // spawn yourself
        Application.LoadLevel("MultiplayerChoose");
    }
}
