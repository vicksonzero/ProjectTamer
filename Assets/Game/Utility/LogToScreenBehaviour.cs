using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(DelegateLogBehaviour))]
public class LogToScreenBehaviour : MonoBehaviour {

    public Text debugLabel;

    void Update()
    {
        this.debugLabel.text = this.GetComponent<DelegateLogBehaviour>().output + "\n" + this.debugLabel.text;
    }
}
