using UnityEngine;
using System.Collections;

public class ScreenMockupBehaviour : MonoBehaviour {

    public Texture[] textures;
    public int textureID=0;

	// Use this for initialization
	void Start () {
        this.guiTexture.texture = textures[this.textureID];

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        this.guiTexture.texture = textures[++this.textureID % this.textures.Length];
    }
}
