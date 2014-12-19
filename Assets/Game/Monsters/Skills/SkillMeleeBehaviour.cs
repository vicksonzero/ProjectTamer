using UnityEngine;
using System.Collections;

public class SkillMeleeBehaviour : SkillsBehaviour
{
    
    [Header("Implementation")]
    public ProjectileBehaviour bulletPrefab;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will then steer to target")]
    public Transform[] spawnPoints = new Transform[0];
    public float bulletSpeed = 100;
    public float bulletSteerSpeed = 100;

    

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
            this.spawnAllBullets();
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
            this.spawnAllBullets();
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

    private void spawnAllBullets()
    {
        this.photonView.RPC("netSpawnAllBullets", PhotonTargets.All,this.skillID);
    }

    [RPC]
    public void netSpawnAllBullets(int skillID)
    {
        if (skillID != this.skillID)
        {
            print("wrong skillID");
            return;
        }
        print("netSpawnAllBullets() called");
    }
    #endregion // private methods

}
