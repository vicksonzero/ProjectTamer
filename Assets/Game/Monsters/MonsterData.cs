using UnityEngine;
using System.Collections;

public class MonsterData : MonoBehaviour {

    public string name = "MonsterName";
    public string description = "MonsterDescription";
    [Tooltip("Max HP for the monster")]
    public float hp = 1000;

    [Tooltip("every physical damage will be reduced by armour")]
    public float armour = 0;

    [Tooltip("type of monster. used to calculate damage and to learn skills. ")]
    public Types type = Types.Water; // no element
    public enum Types { Water, Fire, Grass, Rock, Electric};

    [Tooltip("Normal moving speed. can be overridden by other means")]
    public float movingSpeed = 2;
    [HideInInspector]
    public float sqMovingSpeed = 2;

    [Tooltip("Normal turning speed. can be overridden by other means")]
    public float turningSpeed = 100;

    public float unitRadius = 40;


    [Header("AI")]
    // favourite distance
    public AnimationCurve comfortZone;   // 1 if safe, 0 if danger
    public float comfortZoneScale = 300;
    public AnimationCurve attackZone;   // 1 if easy to attack, 0 if cannot attack

    void start()
    {
        this.sqMovingSpeed = this.movingSpeed * this.movingSpeed;
    }

}
