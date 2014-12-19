using UnityEngine;
using System.Collections;

public class SkillBlastBehaviour : SkillsBehaviour
{

    [Header("Implementation")]
    public BlastBehaviour blast;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. Damage will be done ")]
    public Vector3 offset = Vector3.zero;
    [Tooltip("relative direction to spawn bullet. \nrotated with player. ")]
    public Vector3 blastDirection = Vector3.forward;

    

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
        //Debug.Log("in skill");
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
            this.spawnPS();
            this.ApplyDamage(this.controller.enemy);
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
            this.spawnPS();
            this.ApplyDamage(this.controller.enemy);
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

    private void spawnPS()
    {
        this.photonView.RPC("netSpawnPS", PhotonTargets.All, this.skillID);
    }

    [RPC]
    public void netSpawnPS(int skillID)
    {
        print("net spawnPS() start");
        Vector3 spawnPos = this.transform.localPosition + this.offset;

        Vector3 shootVector = this.transform.TransformDirection( this.blastDirection);

        GameObject b = Instantiate(
            this.blast,
            spawnPos,
            Quaternion.LookRotation(shootVector)
        ) as GameObject;
        b.transform.SetParent(this.transform);
        b.transform.localPosition = this.offset;
        b.particleSystem.startSpeed = this.range;
        b.particleSystem.Play();
    }
    #endregion // private methods

}
