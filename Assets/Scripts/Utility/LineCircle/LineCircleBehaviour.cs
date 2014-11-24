using UnityEngine;
using System.Collections;

public class LineCircleBehaviour : MonoBehaviour {

    public float radius = 20;
    public float thickness = 10;
    public Color color = new Color(255,255,255);
    public int vertices = 20;
    private bool isDirty = true;


	// Use this for initialization
	void Start () {
        this.isDirty = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isDirty)
        {
            this.Rebuild();
        }
	}
    public void Rebuild()
    {
        Debug.Log("circle rebuild");
        // update style
        LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetColors(this.color, this.color);
        lineRenderer.SetWidth(this.thickness, this.thickness);

        // update vertices
        lineRenderer.SetVertexCount(this.vertices+1);
        float x = 0, y = 0, z = 0;
        float pie = 2 * Mathf.PI / this.vertices;
        for (int i = 0; i < this.vertices; i++)
        {
            x = Mathf.Cos(i * pie) * radius;
            z = Mathf.Sin(i * pie) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }

        // make the circle perfect
        x = Mathf.Cos(0) * radius;
        z = Mathf.Sin(0) * radius;
        lineRenderer.SetPosition(this.vertices, new Vector3(x, y, z));


        this.isDirty = false;
    }
    public void SetRadius(float radius) {
        this.radius = radius;
        this.isDirty = true;
    }
    public void SetThickness(float thickness) {
        this.thickness = thickness;
        this.isDirty = true;
    }
    public void SetColor(Color color) {
        this.color = color;
        this.isDirty = true;
    }
    public void SetVertices(int vertices) {
        this.vertices = vertices;
        this.isDirty = true;
    }
}
