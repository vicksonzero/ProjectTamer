using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterData))]
public class MonsterController : MonoBehaviour
{
    [HideInInspector]
    public MonsterData data;
    public int controllerID = -1;
    public Transform enemy;
    // the position it is told to go to, or stay.
    private Vector3 commandGivenPosition;

    // command that lets the monster choose its actions
    private int commandStance; enum stances { aggressive, defensive, evasive };

    // is it in danger? does it need to flee?
    private float stateComfort = 1; // 1=safe, 0=very danger


    [HideInInspector]
    public Vector3 stateEnemyVector;
    [HideInInspector]
    public Vector3 stateMoveVector;

    private bool doFaceEnemy = false;

    //======================================================================
    #region Unity Events

    // Use this for initialization
    void Start()
    {
        this.AIInit();
    }

	// Update is called once per frame
    void Update()
    {
        this.AIPerceive();
        this.AIDecide();
        this.Action();
    }

    #endregion // Unity Events
    //======================================================================
    #region player commands
    public void CommandMoveTo(Vector3 pos)
    {
        //Debug.Log(pos);
        this.commandGivenPosition = pos;
        //StartCoroutine("stepMove");

    }

    void CommandPose(string pose)
    {

    }

    void CommandSkillStart(int skill)
    {
        //Debug.Log("Command");
        this.SendMessage("SkillStart", skill);
    }

    void CommandSkillStep(int skill)
    {
        this.SendMessage("SkillStep", skill);
    }

    void CommandSkillStop(int skill)
    {
        this.SendMessage("SkillStop", skill);
    }
    #endregion //player commands
    //======================================================================
    #region AI decisions

    private void AIInit()
    {
        this.commandGivenPosition = this.transform.position;
        this.data = this.GetComponent<MonsterData>();
    }

    /**
     * gathers information from surroundings
     * updates state variables
     */
    private void AIPerceive()
    {

        this.stateEnemyVector = this.enemy.transform.position - this.transform.position;
        this.stateMoveVector = this.commandGivenPosition - this.transform.position;

        this.stateComfort = this.data.comfortZone.Evaluate(this.stateEnemyVector.magnitude / this.data.comfortZoneScale);

    }
    /**
     * gathers information from surroundings 
     * combines state variables and command variables to make decisions
     */
    private void AIDecide()
    {
        this.doFaceEnemy = true;
    }

    /**
     * Do things after thinking
     */
    private void Action()
    {
        //this.stepDangerColor();
        this.stepMove();
        if (this.doFaceEnemy)
        {
            this.stepTurnFaceEnemy();
        }
    }

    private void stepTurnFaceEnemy()
    {

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(this.stateEnemyVector,Vector3.up), this.data.turningSpeed * Time.deltaTime);

        //Vector3 enemy2dDirection = this.stateEnemyVector;
        //enemy2dDirection.y = 0;
        //Vector3 my2dDirection = this.transform.rotation.eulerAngles;
        //my2dDirection.y = 0;
        //
        //float rotAngle = Vector3.Angle(my2dDirection, enemy2dDirection);
        //print(rotAngle);
        //if(rotAngle>3) this.transform.Rotate(Vector3.up, rotAngle * Time.deltaTime);

        //Quaternion newFaceRotation = Quaternion.Lerp(this.transform.rotation,Quaternion.Euler(this.stateEnemyVector),this.data.turningSpeed*Time.deltaTime);
        //this.transform.rotation = newFaceRotation;
    }


    #endregion // AI decisions
    //======================================================================
    #region actual actions

    
    private void stepMove()
    {

        MonsterData data = this.GetComponent<MonsterData>();


        if (this.stateMoveVector.magnitude > data.movingSpeed)
        {
            this.transform.position += this.stateMoveVector.normalized * data.movingSpeed;
        }
        else
        {
            this.transform.position = this.commandGivenPosition;
        }
        

        //this.transform.position = this.givenPosition;

    }

    private void stepDangerColor()
    {

        float hue = 0.25f * stateComfort;
        this.transform.FindChild("Model").renderer.material.color = new HSBColor(hue, 1, 1, 1).ToColor();
    }


    #endregion //actual actions
    //======================================================================

}
