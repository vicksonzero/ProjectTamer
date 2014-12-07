using UnityEngine;
using System.Collections;

public class PlayerPointAtBehaviour : MonoBehaviour {


    public GameObject[] monster;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void tellMove(int monsterID, Vector3 pos)
    {
        this.getMonsterById(monsterID).GetComponent<PhotonView>().RPC("CommandMoveTo",PhotonTargets.MasterClient, pos);
    }

    public void tellSkillStart(int monsterID, int skillID)
    {
        //Debug.Log("PlayerController");
        this.getMonsterById(monsterID).GetComponent<PhotonView>().RPC("CommandSkillStart", PhotonTargets.MasterClient, skillID);
    }

    public void tellSkillStep(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).GetComponent<PhotonView>().RPC("CommandSkillStep", PhotonTargets.MasterClient, skillID);
    }

    public void tellSkillStop(int monsterID, int skillID)
    {
        this.getMonsterById(monsterID).GetComponent<PhotonView>().RPC("CommandSkillStop", PhotonTargets.MasterClient, skillID);
    }

    private GameObject getMonsterById(int i)
    {
        return this.monster[i];

    }
}
