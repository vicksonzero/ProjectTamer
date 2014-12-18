using UnityEngine;
using System.Collections;

public abstract class SkillsBehaviour : Photon.MonoBehaviour
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
    [Tooltip("damages with damage type \n(0=normal, 1=Fire, \n2=Water, 3=Grass, \n4=Electric, 5=Rock)\n\nDamage is done when bullet hits target")]
    [HideInInspector]
    public float sqRange;

    public float[] damages = new float[6];
    public GameObject[] debuffs;


    public BPilotState state;
    public BPilot controller;


    #region Messages
    // on press action button
    public abstract void SkillStart(int skillID);

    public abstract void SkillStep(int skillID);

    public abstract void SkillStop(int skillID);
    #endregion // Messages

    #region public methods
    public void init(){
        this.sqRange = this.range * this.range;
    }
    public abstract float GetCooldownPercent();
    #endregion public methods


}
