using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterController))]
public class ShootHeavyBulletBehaviour : MonoBehaviour {

    public int skillID = -1;

    public Vector3 offset;
    public Transform bullet;
    public float bulletSpeed = 100;

    [Tooltip("In seconds")]
    public float cooldown = 1;


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
        //Debug.Log("in skill");
        if (this.canShoot())
        {
            // set alarm for next canShoot
            this.setAlarm();

            // get enemyVector from other component
            MonsterController controller = this.GetComponent<MonsterController>();
            Vector3 enemyVector = controller.stateEnemyVector;
            Transform b = Instantiate(
                this.bullet, 
                this.transform.position + this.offset, 
                Quaternion.FromToRotation(Vector3.forward,enemyVector)
            ) as Transform;
            b.rigidbody.velocity = b.transform.forward * this.bulletSpeed;
        }
    }

    public void SkillStep(int skillID)
    {
        if (skillID != this.skillID) return;


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
    #endregion // private methods

}
