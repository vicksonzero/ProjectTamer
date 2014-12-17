using UnityEngine;
using System.Collections;

public abstract class OSkills : MonoBehaviour
{

    [Header("Skills basics")]
    public int skillID = -1;
    public string name = "";
    public string description = "";
    [Tooltip("whether or not the monster continues to attack as long as the skill button is pressed")]
    public bool semiauto = false;
    [Tooltip("Time needed for a monster to stand still BEFORE using the attack")]
    public float channelTime = 0;
    [Tooltip("Time needed for a monster to stand still DURING the attack")]
    public float skillTime = 0;
    [Tooltip("Time needed for a monster to stand still AFTER using the attack")]
    public float recoveryTime = 0;
    [Tooltip("In seconds")]
    public float cooldown = 1;
    [Tooltip("PP as in pokemon. limited times to use the skill in the whole tournament. a portion of the remaining PP is regenerated after the battle")]
    public int pP = 20;
    [HideInInspector]
    public int ppRemaining;
    public float range = 50;
    [Tooltip("damages with damage type \n(0=normal, 1=Fire, \n2=Water, 3=Grass, \n4=Electric, 5=Rock)\n\nDamage is done when bullet hits target")]
    public float[] damages = new float[6];
    public GameObject[] debuffs;


}
