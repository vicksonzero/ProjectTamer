using UnityEngine;
using System.Collections;

public class SkillCrushBehaviour : SkillsBehaviour
{
    
    [Header("Implementation")]
    public CrushBehaviour effectPrefab;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will then steer to target")]
    public Transform spawnPoint;
    public float crushSpeed = 100;
    public float crushDuration = 1;
    public float crushElapsed = 1;

    public AudioClip startSound;


    private CrushBehaviour crushGO;

    public BPilotState.MovePattern beforeChaseMovePattern;

    

    private float alarmStart = 0;
    private float alarmStop = 0;

    #region unity events

    // Don't use this for initialization
    // use init() instead
    void Start() { this.init(); } 
	
	// Update is called once per frame
	void Update () {
        if (this.crushElapsed > -1)
        {
            if (this.crushElapsed < this.crushDuration)
            {
                this.crushElapsed += Time.deltaTime;
            }
            else
            {
                this.controller.state.movePattern = this.beforeChaseMovePattern;
                Destroy(crushGO.gameObject);
                this.crushElapsed = -1;
            }
        }
    }
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
        this.crushElapsed = -1;
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
        this.audio.PlayOneShot(startSound);

        this.crushGO = Instantiate(this.effectPrefab, this.spawnPoint.position, Quaternion.identity) as CrushBehaviour;
        Physics.IgnoreCollision(this.crushGO.collider, this.collider);
        
        this.crushGO.target = this.controller.enemy;
        this.crushGO.ownerSkill = this;
        this.crushGO.transform.SetParent(this.transform);

        this.beforeChaseMovePattern = this.controller.state.movePattern;
        this.controller.state.movePattern = BPilotState.MovePattern.ChaseEnemy;
        this.crushElapsed = 0;
    }
    #endregion // private methods

}
