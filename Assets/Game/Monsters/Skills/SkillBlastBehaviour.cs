using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterController))]
public class SkillBlastBehaviour : MonoBehaviour
{

    [Header("Skills basics")]
    public int skillID = -1;
    [Tooltip("whether or not the monster continues to attack as long as the skill button is pressed")]
    public bool semiauto = false;
    [Tooltip("In seconds")]
    public float cooldown = 1;
    [Tooltip("PP as in pokemon. limited times to use the skill in the whole tournament. a portion of the remaining PP is regenerated after the battle")]
    public int pP = 20;
    [HideInInspector]
    public int ppRemaining;
    public float range = 50;

    [Header("Implementation")]
    public BlastBehaviour blast;
    [Tooltip("relative positions to spawn bullet. \nrotated with player. Damage will be done ")]
    public Vector3 offset = Vector3.zero;
    [Tooltip("relative direction to spawn bullet. \nrotated with player. ")]
    public Vector3 blastDirection = Vector3.forward;
    [Tooltip("damages with damage type \n(0=normal, 1=Fire, \n2=Water, 3=Grass, \n4=Electric, 5=Rock)\n\nDamage is done when bullet hits target")]
    public float[] damages = new float[6];
    public GameObject[] debuffs;

    

    private float alarmStart = 0;
    private float alarmStop = 0;
    private MonsterController controller; // needs Requirecomponent

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
            this.spawnPS();
            this.applyDamage();
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
            this.spawnPS();
            this.applyDamage();
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

    public void applyDamage()
    {
        throw new System.NotImplementedException();
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
        print("spawnPS() start");
        Vector3 spawnPos = this.transform.localPosition + this.offset;

        Vector3 shootVector = this.transform.TransformDirection( this.blastDirection);

        GameObject b = PhotonNetwork.Instantiate(
            this.blast.name,
            spawnPos,
            Quaternion.LookRotation(shootVector),
            0
        );
        b.transform.SetParent(this.transform);
        b.transform.localPosition = this.offset;
        b.particleSystem.startSpeed = this.range;
        b.particleSystem.Play();
    }
    #endregion // private methods

}
