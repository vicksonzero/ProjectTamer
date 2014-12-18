using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BFGameCreatePilot))]
public class BGameController : Photon.MonoBehaviour {


    public Transform arena;
    public Transform[] spawnPoints = new Transform[2];


    public int[][] chosenMonsters;

    [HideInInspector]
    public MonsterSelectControlBehaviour monsterSelectControl;
    [HideInInspector]
    public GameObject[] pilotsGO = new GameObject[2];
    [HideInInspector]
    public bool isGoingSolo = false;
    public BPlayerController player;

    private BFGameCreatePilot pilotCreator;

	// Use this for initialization
	void Start () {
        this.pilotCreator = this.GetComponent<BFGameCreatePilot>();
        GameObject monsterSelectControl_go = GameObject.Find("MonsterSelectControl");
        // init chosen monsters, either from last scene, or use default ones
        if (monsterSelectControl_go == null)
        {
            this.chosenMonsters = new int[2][]{new int[2]{0,1},new int[2]{2,3}};
            this.isGoingSolo = true;
        }
        else
        {
            MonsterSelectControlBehaviour monsterSelectControl = monsterSelectControl_go.GetComponent<MonsterSelectControlBehaviour>();
            this.chosenMonsters = monsterSelectControl.chosenMonsters;
        }
        print(this.chosenMonstersToString());

        // network view
        if (PhotonNetwork.isMasterClient)
        {
            int id0 = PhotonNetwork.AllocateViewID();
            int id1 = PhotonNetwork.AllocateViewID();
            this.photonView.RPC("createPilots", PhotonTargets.All, id0, id1);
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.player.monsterID = 0;
        }
        else
        {
            this.player.monsterID = 1;
        }

	}

    public void OnMonsterTakesDamage(BPilot pilot)
    {
        // dies if the pilot has no hp
        if (pilot.state.hp <= 0)
        {
            //this.EndGame(pilot.enemy.GetComponent<BPilot>().pilotID);
            this.photonView.RPC("EndGame", PhotonTargets.All, pilot.enemy.GetComponent<BPilot>().pilotID);
        }
    }

    [RPC]
    public void EndGame(int winnerID)
    {
        BPilot winner = this.pilotsGO[winnerID].GetComponent<BPilot>();
        // disable all monsters
        foreach(GameObject pilot_go in pilotsGO)
        {
            pilot_go.SetActive(false);
        }
        this.GetComponent<BEndGame>().EndGameScreen(
            winner.data.name + " "+
            "(Player " + (winner.pilotID == 0 ? "A" : "B") + ") "+
            "Wins!"
        );
    }
	
	// Update is called once per frame
	void Update () { }

    [RPC]
    void createPilots(int id0, int id1)
    {
        this.pilotsGO[0] = this.pilotCreator.CreatePilotWithMonster(0, this.chosenMonsters[0][0], id0);
        this.pilotsGO[1] = this.pilotCreator.CreatePilotWithMonster(1, this.chosenMonsters[1][0], id1);

        this.pilotsGO[0].transform.position = this.spawnPoints[0].position;
        this.pilotsGO[0].transform.rotation = this.spawnPoints[0].rotation;

        this.pilotsGO[1].transform.position = this.spawnPoints[1].position;
        this.pilotsGO[1].transform.rotation = this.spawnPoints[1].rotation;

        this.pilotsGO[0].transform.SetParent(this.arena);
        this.pilotsGO[1].transform.SetParent(this.arena);

        this.pilotsGO[0].GetComponent<BPilot>().enemy = this.pilotsGO[1].transform;
        this.pilotsGO[1].GetComponent<BPilot>().enemy = this.pilotsGO[0].transform;

        this.pilotsGO[0].GetComponent<BPilot>().gameController = this;
        this.pilotsGO[1].GetComponent<BPilot>().gameController = this;


    }


    private string chosenMonstersToString()
    {
        string str = "chosen monsters: ";

        str += this.chosenMonsters[0][0] + " ";
        str += this.chosenMonsters[0][1] + " ";
        str += this.chosenMonsters[1][0] + " ";
        str += this.chosenMonsters[1][1] + " ";


        return str;

    }

}
