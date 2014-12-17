using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchmakerBehaviour : MonoBehaviour {

    public Text uIText;
    public Camera cameraSet;
    public string versionNumber = "0.1.2";
    public bool solo = false;

    public RectTransform startButton;

    private  MultiplayerStates _multiplayerState;
    [HideInInspector]
    public MultiplayerStates multiplayerState{
        get
        {
            return this._multiplayerState;
        }
        set
        {
            this._multiplayerState = value;
            switch (this._multiplayerState)
            {
                case MultiplayerStates.masterWaiting:
                    this.startButton.Find("Text").GetComponent<Text>().text = "Waiting for client...";
                    break;
                case MultiplayerStates.masterReady:
                    this.startButton.Find("Text").GetComponent<Text>().text = "Start Game!";
                    break;
                case MultiplayerStates.client:
                    this.startButton.Find("Text").GetComponent<Text>().text = "Ready!";
                    break;
                case MultiplayerStates.clientReady:
                    this.startButton.Find("Text").GetComponent<Text>().text = "Waiting for master...";
                    break;
                default:
                    this.startButton.Find("Text").GetComponent<Text>().text = "Error occurred with _multiplayerState";
                    break;
            }
        }
    }
    public enum MultiplayerStates { client, clientReady, masterWaiting, masterReady };


	// Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(this.versionNumber);
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        //PhotonNetworkingMessage

	}
	void Update () {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        this.uIText.text = PhotonNetwork.connectionStateDetailed.ToString();
        //print(this.uIText.text);
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
        if (this.solo){
            this.multiplayerState = MultiplayerStates.masterReady;
        }
        else{
            if (PhotonNetwork.isMasterClient){
                this.multiplayerState = MultiplayerStates.masterWaiting;
            }
            else{
                this.multiplayerState = MultiplayerStates.client;
            }
        }
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
