using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGameMessage : MonoBehaviour {

    public Text textBox;
    [Tooltip("Seconds")]
    public float msgTime = 2;
    private float elapsedTime = 0;
    private float duration = 0;
	
	// Update is called once per frame
	void Update () {
        if (duration > 0)
        {
            this.elapsedTime += Time.deltaTime;
            if (this.elapsedTime < this.duration)
            {
                Color c = this.textBox.color;
                c.a = Mathf.Lerp(c.a, 0, this.elapsedTime / this.duration);
                this.textBox.color = c;
            }
            else
            {
                this.duration = 0;
            }
        }
	}

    public void Msg(string str)
    {
        this.Msg(str, this.msgTime);
    }
    public void Msg(string str, float duration)
    {
        this.textBox.text = str;
        Color c = this.textBox.color;
        c.a = 1;
        this.textBox.color = c;

        this.elapsedTime = 0;
        this.duration = duration;
    }

}
