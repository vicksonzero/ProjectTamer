using UnityEngine;
using System.Collections;

public class SkillAOEBehaviour : SkillsBehaviour
{
    
    [Header("Implementation")]
    public AOEBehaviour effectPrefab;
    [Tooltip("The feet of enemy, or at my feet")]
    public OSkillAOE.SpawnAt spawnAt;
    public float duration;

    

    private float alarmStart = 0;
    private float alarmStop = 0;

    #region unity events

    // Don't use this for initialization
    // use init() instead
    void Start() { this.init(); } 
	
	// Update is called once per frame
	void Update () {}
    #endregion // unity events

    #region Messages
    // on press action button
    public override void SkillStart(int skillID)
    {
        //Debug.Log("Skill");
        if (skillID != this.skillID) return;
        if(this.ppRemaining <= 0) {
            Debug.Log("no PP remaining");
            return;
        }
        if (this.controller.state.enemyVector.sqrMagnitude > this.sqRange)
        {
            Debug.Log("Too far can't shoot");
            return;
        }
        if(this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();
            Vector3[] positions = new Vector3[1];
            if (this.spawnAt == OSkillAOE.SpawnAt.ENEMY)
            {
                positions[0] = this.controller.enemy.position;
            }
            else
            {
                positions[0] = this.controller.transform.position;
            }
            this.spawnAOE(positions);
            this.ppRemaining--;
        }
    }

    public override void SkillStep(int skillID)
    {
        if (!this.semiauto) return;
        if (skillID != this.skillID) return;
        if (this.ppRemaining <= 0)
        {
            Debug.Log("no PP remaining");
            return;
        }
        if (this.controller.state.enemyVector.sqrMagnitude > this.sqRange)
        {
            Debug.Log("Too far can't shoot");
            return;
        }
        if (this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();
            Vector3[] positions = new Vector3[1];
            if (this.spawnAt == OSkillAOE.SpawnAt.ENEMY)
            {
                positions[0] = this.controller.enemy.position;
            }
            else
            {
                positions[0] = this.controller.transform.position;
            }
            this.spawnAOE(positions);
            this.ppRemaining--;
        }

    }

    public override void SkillStop(int skillID)
    {
        if (skillID != this.skillID) return;


    }
    #endregion // Messages

    #region public methods
    public void init()
    {
        base.init();
        this.ppRemaining = this.pP;
        if (this.skillID < 0)
        {
            print("invalid skillID");
        }
    }
    public override float GetCooldownPercent()
    {
        return (Time.time-this.alarmStart)/(this.alarmStop - this.alarmStart);
    }



    #endregion public methods

    #region private methods
    private bool canShoot()
    {
        return Time.time >= this.alarmStop;
    }
    private void setAlarm()
    {
        this.alarmStart = Time.time;
        this.alarmStop = Time.time + this.cooldown;
    }

    private void spawnAOE(Vector3[] pos)
    {
        this.photonView.RPC("netSpawnAOE", PhotonTargets.All, this.skillID, pos);
    }

    [RPC]
    public void netSpawnAOE(int skillID, Vector3[] pos)
    {
        if (skillID != this.skillID)
        {
            print("wrong skillID");
            return;
        }
        print("netSpawnAllBullets() called");
        // get enemyVector from other component
        foreach (Vector3 sp in pos)
        {

            print("offset:" + sp.ToString());
            AOEBehaviour b = Instantiate(
                this.effectPrefab,
                sp,
                Quaternion.AngleAxis(Random.value*360,Vector3.up)
            ) as AOEBehaviour;
            b.target = this.controller.enemy;
            b.ownerSkill = this;
            b.startAlarm(this.duration);

            //b.debuffs = this.debuffs;
        }
    }
    #endregion // private methods

}
