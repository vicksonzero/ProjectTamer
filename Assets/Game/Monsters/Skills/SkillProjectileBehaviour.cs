using UnityEngine;
using System.Collections;

public class SkillProjectileBehaviour : SkillsBehaviour
{
    
    [Header("Implementation")]
    public ProjectileBehaviour bullet;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will fly away from center and steer to target")]
    public Vector3[] offsets = new Vector3[1];
    public float bulletSpeed = 100;
    public float bulletSteerSpeed = 100;

    

    private float alarmStart = 0;
    private float alarmStop = 0;
    private MonsterController controller; // needs Requuirecomponent

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
        if(this.ppRemaining > 0 && this.canShoot())
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

        if (this.ppRemaining > 0 && this.canShoot())
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
    public override void init()
    {
        this.controller = this.GetComponent<MonsterController>();
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
        print("spawnAllBullets() called");
        // get enemyVector from other component
        Vector3 spawnPos;
        foreach(Vector3 offset in this.offsets)
        {
            spawnPos = this.transform.localPosition + offset;
            // Unity.Instantiate()
            //ProjectileBehaviour b = Instantiate(
            //    this.bullet,
            //    this.transform.position + offset,
            //    Quaternion.LookRotation(offset,Vector3.up)
            //) as ProjectileBehaviour;

            // photon network
            print(this.bullet.name);


            object[] bParam = new object[5];
            bParam[(int)ProjectileParameters.targetName] = (object)this.controller.enemy.name;
            bParam[(int)ProjectileParameters.bulletSpeed] = (object)this.bulletSpeed;
            bParam[(int)ProjectileParameters.bulletSteerSpeed] = (object)this.bulletSteerSpeed;
            bParam[(int)ProjectileParameters.damages] = (object)this.damages;
            //bParam[(int)ProjectileParameters.debuffs] = (object)this.debuffs;

            GameObject b = PhotonNetwork.Instantiate(
                this.bullet.name,
                this.transform.position + offset,
                Quaternion.LookRotation(offset, Vector3.up),
                0,
                bParam
            ) ;
            ProjectileBehaviour bBehaviour = b.GetComponent<ProjectileBehaviour>();
        }
    }
    #endregion // private methods

}
