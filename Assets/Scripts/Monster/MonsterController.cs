using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MonsterData))]
public class MonsterController : MonoBehaviour
{

    public Transform enemy;
    // the position it is told to go to, or stay.
    private Vector3 commandGivenPosition;

    // command that lets the monster choose its actions
    private int commandStance; enum stances { aggressive, defensive, evasive };

    // is it in danger? does it need to flee?
    private float stateComfort = 1; // 1=safe, 0=very danger


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
        Debug.Log(pos);
        this.commandGivenPosition = pos;
        //StartCoroutine("stepMove");

    }

    void CommandPose(string pose)
    {

    }

    void CommandUseSkill(int skill)
    {

    }
    #endregion //player commands
    //======================================================================
    #region AI decisions

    private void AIInit()
    {
        this.commandGivenPosition = this.transform.position;
    }

    /**
     * gathers information from surroundings
     * updates state variables
     */
    private void AIPerceive()
    {
        MonsterData data = this.GetComponent<MonsterData>();

        Vector3 distance = this.transform.position - this.enemy.transform.position;
        this.stateComfort = data.comfortZone.Evaluate(distance.magnitude / data.comfortZoneScale);

    }
    /**
     * gathers information from surroundings 
     * combines state variables and command variables to make decisions
     */
    private void AIDecide()
    {

    }

    /**
     * Do things after thinking
     */
    private void Action()
    {

        float hue = 0.25f * stateComfort;
        this.transform.FindChild("Model").renderer.material.color = new HSBColor(hue, 1, 1, 1).ToColor();
        this.stepMove();
    }


    #endregion // AI decisions
    //======================================================================
    #region actual actions

    
    private void stepMove()
    {

        MonsterData data = this.GetComponent<MonsterData>();

        Vector3 moveVector = this.commandGivenPosition - this.transform.position;

        if (moveVector.magnitude > data.speed)
        {
            this.transform.position += moveVector.normalized * data.speed;
        }
        else
        {
            this.transform.position = this.commandGivenPosition;
        }
        

        //this.transform.position = this.givenPosition;

    }


    #endregion //actual actions
    //======================================================================

}
