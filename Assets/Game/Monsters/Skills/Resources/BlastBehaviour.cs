using UnityEngine;
using System.Collections;

/**
 * Heavy bullet behaviour
 * used with AutoDieBehaviour
 */
public class BlastBehaviour : MonoBehaviour
{
    void Update()
    {
        if (!this.particleSystem.IsAlive()) Destroy(this.gameObject);
    }
}
