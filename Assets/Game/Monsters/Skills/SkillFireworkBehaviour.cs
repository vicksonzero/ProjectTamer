using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterController))]
public class SkillFireworkBehaviour : MonoBehaviour
{

    public int skillID = -1;

    public Vector3 offset;

    public Transform fireworkPrefab;

    [Tooltip("In seconds")]
    public float cooldown = 3;


    private float alarmStart = 0;
    private float alarmStop = 0;

    #region unity events
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion // unity events

    #region Messages
    public void SkillStart(int skillID)
    {
        //Debug.Log("Skill");
        if (skillID != this.skillID) return;
        
    }

    public void SkillStep(int skillID)
    {
        //Debug.Log("Skill");
        if (skillID != this.skillID) return;
        //Debug.Log("in skill");
        if (this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();
            this.createFirework(this.transform.position + this.offset);
        }
    }

    public void SkillStop(int skillID)
    {
        if (skillID != this.skillID) return;


    }
    #endregion // Messages

    public float GetCooldown()
    {
        return (Time.time-this.alarmStart)/(this.alarmStop - this.alarmStart);
    }

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
    private void createFirework(Vector3 pos)
    {
        PhotonNetwork.Instantiate(this.fireworkPrefab.name, pos, Quaternion.identity,0);
    }
    #endregion // private methods

}
