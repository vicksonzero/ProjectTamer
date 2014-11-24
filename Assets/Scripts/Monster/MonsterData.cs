using UnityEngine;
using System.Collections;

public class MonsterData : MonoBehaviour {


    // favourite distance
    public AnimationCurve comfortZone;   // 1 if safe, 0 if danger
    public float comfortZoneScale = 300;
    public AnimationCurve attackZone;   // 1 if easy to attack, 0 if cannot attack

    public float speed = 2;



    private Transform[] debug_ranges;
    private int debug_ranges_count = 20;
    public Transform debugCircles;
    public bool debugComfort = false;

    // skill names
    enum skill { };

	// Use this for initialization
	void Start () {
        if(this.debugComfort) this.debugConfortInit();
	}

    // Update is called once per frame
    void Update()
    {
        if(this.debugComfort) this.debugConfortUpdate();
	}



    private void debugConfortInit()
    {
        //Debug.Log(dangerZone.Evaluate(0.5f));
        this.debug_ranges = new Transform[this.debug_ranges_count];

        float interval = 1.0f / this.debug_ranges_count;
        for (int i = 0; i < this.debug_ranges_count; i++)
        {
            this.debug_ranges[i] = Instantiate(debugCircles, this.transform.position, this.transform.rotation) as Transform;
            this.debug_ranges[i].SendMessage("SetRadius", this.comfortZoneScale * i * interval);
            float hue = 0.25f * this.comfortZone.Evaluate(i * interval);
            this.debug_ranges[i].SendMessage("SetColor", new HSBColor(hue, 0.7f, 1, 0.5f).ToColor());

        }
    }
    
    private void debugConfortUpdate()
    {
        float interval = 1.0f / this.debug_ranges_count;
        for (int i = 0; i < this.debug_ranges_count; i++)
        {
            this.debug_ranges[i].transform.position = this.transform.position + new Vector3(0, 10, 0);
        }
    }
}
