using UnityEngine;
using System.Collections;

public class MonsterData : MonoBehaviour {


    [Tooltip("Max HP for the monster")]
    public float hp = 1000;

    [Tooltip("every physical damage will be reduced by armour")]
    public float armour = 0;

    [Tooltip("type of monster. used to calculate damage and to learn skills. use MonsterData.types enum whenever possible")]
    public int type = -1; // no element
    public enum types { Water, Fire, Grass, Rock, Electric};

    [Tooltip("Normal moving speed. can be overridden by other means")]
    public float movingSpeed = 2;

    [Tooltip("Normal moving speed. can be overridden by other means")]
    public float turningSpeed = 100;



    // favourite distance
    public AnimationCurve comfortZone;   // 1 if safe, 0 if danger
    public float comfortZoneScale = 300;
    public AnimationCurve attackZone;   // 1 if easy to attack, 0 if cannot attack

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
        //float interval = 1.0f / this.debug_ranges_count;
        for (int i = 0; i < this.debug_ranges_count; i++)
        {
            this.debug_ranges[i].transform.position = this.transform.position + new Vector3(0, 10, 0);
        }
    }
}
