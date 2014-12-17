using UnityEngine;
using System.Collections;

public class BInputNetworkPlayer : Photon.MonoBehaviour {

    public BGameController gameController;


    private bool[] buttonsHold = new bool[3];


	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < this.buttonsHold.Length; i++)
        {
            if (this.buttonsHold[i])
            {
                this.NetSkillStep(i);
            }
        }
    }

    public void CommandMoveTo(Vector3 pos)
    {
        this.photonView.RPC("NetMove", PhotonTargets.MasterClient, pos);
    }
    public void CommandSkillStart(int skillID)
    {
        this.photonView.RPC("NetSkillStart", PhotonTargets.MasterClient, skillID);
    }
    public void CommandSkillStep(int skillID)
    {
        this.photonView.RPC("NetSkillStep", PhotonTargets.MasterClient, skillID);
    }
    public void CommandSkillStop(int skillID)
    {
        this.photonView.RPC("NetSkillStop", PhotonTargets.MasterClient, skillID);
    }

    #region RPCS

    [RPC]
    public void NetMove(Vector3 pos)
    {
        this.gameController.pilotsGO[1].GetComponent<BPilot>().CommandMoveTo(pos);
    }
    [RPC]
    public void NetSkillStart( int skillID)
    {
        this.gameController.pilotsGO[1].GetComponent<BPilot>().CommandSkillStart(skillID);
    }
    [RPC]
    public void NetSkillStep(int skillID)
    {
        this.gameController.pilotsGO[1].GetComponent<BPilot>().CommandSkillStep(skillID);
    }
    [RPC]
    public void NetSkillStop(int skillID)
    {
        this.gameController.pilotsGO[1].GetComponent<BPilot>().CommandSkillStop(skillID);
    }
    #endregion RPCS

}
