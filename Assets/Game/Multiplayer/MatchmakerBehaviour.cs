using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchmakerBehaviour : MonoBehaviour {

    public Text uIText;
    public Camera cameraSet;


	// Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        //PhotonNetworkingMessage

	}
	void Update () {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        this.uIText.text = PhotonNetwork.connectionStateDetailed.ToString();
        print(this.uIText.text);
	}
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.isMasterClient?"Master":"client");

    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        print("Can't join random room");
        RoomOptions ro = new RoomOptions();
        ro.maxPlayers = 2;
        PhotonNetwork.CreateRoom("",ro,new TypedLobby("",LobbyType.Default));
    }

    void OnJoinedRoom()
    {
        cameraSet.GetComponent<multiplayerCameraBehaviour>().nextScreen();
    }
    public static int getMonsterID()
    {
        if (PhotonNetwork.isMasterClient)
        {
            return 0;
        }
        else if (PhotonNetwork.isNonMasterClientInRoom)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
