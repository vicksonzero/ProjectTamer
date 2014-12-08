using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterController))]
public class SkillBlastBehaviour : MonoBehaviour
{

    public int skillID = -1;
    [Tooltip("whether or not the monster continues to attack as long as the skill button is pressed")]
    public bool semiauto = false;
    [Tooltip("In seconds")]
    public float cooldown = 1;
    [Tooltip("PP as in pokemon. limited times to use the skill in the whole tournament. a portion of the remaining PP is regenerated after the battle")]
    public int pP = 20;
    [HideInInspector]
    public int ppRemaining = 1;
    
    public ProjectileBehaviour bullet;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. \n\nmore than 1 entry mean that more than 1 projectile per shot, \nwhich means that damage is scaled up.\n\nProjectiles will fly away from center and steer to target")]
    public Vector3[] offsets = new Vector3[1];
    public float bulletSpeed = 100;
    public float bulletSteerSpeed = 100;
    public float range = 50;
    [Tooltip("damages with damage type \n(0=normal, 1=Fire, \n2=Water, 3=Grass, \n4=Electric, 5=Rock)\n\nDamage is done when bullet hits target")]
    public float[] damages = new float[6];
    public GameObject[] debuffs;

    

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
    public void SkillStart(int skillID)
    {
        //Debug.Log("Skill");
        if (skillID != this.skillID) return;
        //Debug.Log("in skill");
        if (this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();
            this.spawnAllBullets();
        }
    }

    public void SkillStep(int skillID)
    {
        if (!this.semiauto) return;
        if (skillID != this.skillID) return;

        if (this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();
            this.spawnAllBullets();
        }

    }

    public void SkillStop(int skillID)
    {
        if (skillID != this.skillID) return;


    }
    #endregion // Messages

    #region public methods
    public void init()
    {
        this.controller = this.GetComponent<MonsterController>();
        this.ppRemaining = this.pP;
        if (this.skillID < 0)
        {
            print("invalid skillID");
        }
    }
    public float GetCooldown()
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
        print("spawnAllBullets() start");
        // get enemyVector from other component
        Vector3 spawnPos;
        foreach(Vector3 offset in this.offsets)
        {
            print("offset:"+offset.ToString());
            spawnPos = this.transform.localPosition + offset;
            ProjectileBehaviour b = Instantiate(
                this.bullet,
                this.transform.position + offset,
                Quaternion.LookRotation(offset,Vector3.up)
            ) as ProjectileBehaviour;
            print("1" + this.controller.enemy.ToString());
            b.target = this.controller.enemy;
            print("2" + b.target.gameObject.name);

            b.bulletSpeed = this.bulletSpeed;
            b.bulletSteerSpeed = this.bulletSteerSpeed;
            b.damages = this.damages;
            b.debuffs = this.debuffs;
        }
    }
    #endregion // private methods

}
