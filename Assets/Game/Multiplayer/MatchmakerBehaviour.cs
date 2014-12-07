using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchmakerBehaviour : MonoBehaviour {

    public Text uIText;
    public Camera cameraSet;
    public int monsterID = -1;




    private static MatchmakerBehaviour _instance;

    public static MatchmakerBehaviour instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MatchmakerBehaviour>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }


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
        if (PhotonNetwork.isMasterClient)
        {
            this.monsterID = 0;
        }
        else if (PhotonNetwork.isNonMasterClientInRoom)
        {
            this.monsterID = 1;
        }
        else
        {
            this.monsterID = -1;
        }
        cameraSet.GetComponent<multiplayerCameraBehaviour>().nextScreen();
    }
}
