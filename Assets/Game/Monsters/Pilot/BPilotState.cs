using UnityEngine;
using System.Collections;

public class BPilotState : MonoBehaviour {

    public enum MovePattern { Stop, FollowGameController, FollowPlayer, FollowPlayerWander, ChaseEnemy, Wander};
    public MovePattern movePattern = MovePattern.FollowPlayer;

    public Vector3 givenPosition;

    public Vector3 enemyVector;
    public Vector3 moveVector;

    public float hp=100;


}
