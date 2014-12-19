using UnityEngine;
using System.Collections;

public class BPilot : MonoBehaviour {

    public int pilotID = -1;
    public MonsterData data;
    public OSkills[] skillsDef = new OSkills[3];
    public enum ControlRights { none, game, player0, player1 };
    public ControlRights haveControl = ControlRights.player0;

    [HideInInspector]
    public BPilotState state;
    [HideInInspector]
    public BPilotNetwork network;
    [HideInInspector]
    public SkillsBehaviour[] skills = new SkillsBehaviour[3];
    [HideInInspector]
    public Transform enemy;
    [HideInInspector]
    public BGameController gameController;
    [HideInInspector]
    public Transform nameTag;
    [HideInInspector]
    public BGameController game;

	// Use this for initialization
	void Start () {
        //this.state = this.gameObject.AddComponent<BPilotState>();
        this.AIInit();
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient || this.gameController.isGoingSolo)
        {
            this.AIPerceive();
            this.AIDecide();
            this.Action();
        }
        else  // is client
        {

        }
        this.stepUpdateNameTag();
	}




    #region public methods

    public float getHP()
    {
        return this.state.hp;

    }
    public float getMaxHP()
    {
        return this.data.hp;

    }
    public float getHPPercent()
    {
        return this.state.hp / this.data.hp;

    }
    public void TakeDamage(float[] damages)
    {
        print("Taking damage:"+ damages.ToString());

        float[] trueDamages = BElements.TranslateDamage(damages, (int)this.data.element);
        float trueDmg = 0;
        for (int i = 0; i < damages.Length; i++)
        {
            trueDmg += trueDamages[i];
        }

        print("Translated damage:" + trueDamages.ToString() + "-->" + trueDmg);


        this.state.hp -= trueDmg;
        game.OnMonsterTakesDamage(this);
    }
    public void TakeBuff(string name)
    {
        print("Taking buff:" + name);
    }

    #endregion public methods


    //=====================================================================================

    #region AI decisions

    private void AIInit()
    {
        this.state.givenPosition = this.transform.position;
        this.state.hp = this.data.hp;
    }

    /**
     * gathers information from surroundings
     * updates state variables
     */
    private void AIPerceive()
    {
        if(this.enemy!=null)
            this.state.enemyVector = this.enemy.transform.position - this.transform.position;
        this.state.moveVector = this.state.givenPosition - this.transform.position;

        //print(this.state.givenPosition.ToString());
        //print(this.state.moveVector.ToString());
    }
    /**
     * gathers information from surroundings 
     * combines state variables and command variables to make decisions
     */
    private void AIDecide()
    {
        //this.doFaceEnemy = true;
    }

    /**
     * Do things after thinking
     */
    private void Action()
    {
        //this.stepDangerColor();
        this.stepMove();
        //if (this.doFaceEnemy)
        //{
            this.stepTurnFaceEnemy();
        //}
    }



    #endregion // AI decisions


    //=====================================================================================


    #region actual action

    private void stepTurnFaceEnemy()
    {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(this.state.enemyVector, Vector3.up), this.data.turningSpeed * Time.deltaTime);
    }

    private void stepMove()
    {


        if (this.state.moveVector.sqrMagnitude > this.data.movingSpeed)
        {
            this.transform.position += this.state.moveVector.normalized * this.data.movingSpeed;
        }
        else
        {
            this.transform.position = this.state.givenPosition;
        }


        //this.transform.position = this.givenPosition;

    }

    private void stepUpdateNameTag()
    {
        this.nameTag.guiText.text = this.name + " " + this.getHP().ToString("F2") + "/" + this.getMaxHP().ToString("F2");
    }

    #endregion actual action

    //=====================================================================================

    #region Commands
    public void CommandMoveTo(Vector3 pos)
    {
        print("1");
        this.state.givenPosition = pos;
    }
    public void CommandSkillStart(int skillID)
    {
        this.skills[skillID].SkillStart(skillID);

    }
    public void CommandSkillStep(int skillID)
    {
        this.skills[skillID].SkillStep(skillID);
    }
    public void CommandSkillStop(int skillID)
    {
        this.skills[skillID].SkillStop(skillID);
    }
    
    #endregion Commands



}
