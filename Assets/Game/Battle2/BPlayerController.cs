using UnityEngine;
using System.Collections;

public class BPlayerController : MonoBehaviour {

    [Tooltip("These inputs will be deactivated on Start, except for activeInput")]
    public GameObject[] inputs;
    public int chosenInput = 0;
    public BGameController gameController;
    public BInputNetworkPlayer networkPlayer;

	// Use this for initialization
	void Start () {
        this.disableAllInputs();
        this.inputs[this.chosenInput].SetActive(true);
	}

    private void disableAllInputs()
    {
        foreach (GameObject input in this.inputs)
        {
            input.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Move(int monsterID, Vector3 pos)
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.gameController.pilotsGO[monsterID].GetComponent<BPilot>().CommandMoveTo(pos);
        }
        else
        {
            this.networkPlayer.CommandMoveTo(pos);
        }
    }
    public void SkillStart(int monsterID, int skillID)
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.gameController.pilotsGO[monsterID].GetComponent<BPilot>().CommandSkillStart(skillID);
        }
        else
        {
            this.networkPlayer.CommandSkillStart(skillID);
        }
    }
    public void SkillStep(int monsterID, int skillID)
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.gameController.pilotsGO[monsterID].GetComponent<BPilot>().CommandSkillStep(skillID);
        }
        else
        {
            //this.networkPlayer.CommandSkillStep(skillID);
        }
    }
    public void SkillStop(int monsterID, int skillID)
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.gameController.pilotsGO[monsterID].GetComponent<BPilot>().CommandSkillStop(skillID);
        }
        else
        {
            this.networkPlayer.CommandSkillStop(skillID);
        }
    }

}
