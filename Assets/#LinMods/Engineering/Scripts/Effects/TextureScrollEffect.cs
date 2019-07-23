using UnityEngine;
using System.Collections;

public class TextureScrollEffect: MonoBehaviour
{
    public Vector2 TextureScrollSpeed;
	
    
    // Update is called once per frame
	void Update(/*void*/)
    {
     Vector2 ScrollOffset = GetComponent<Renderer>().material.mainTextureOffset;
     ScrollOffset += TextureScrollSpeed*Time.deltaTime*60.0f;
     if (ScrollOffset.x < 0.0f)
       ScrollOffset.x += 1.0f;
     else if (ScrollOffset.x >= 1.0f)
       ScrollOffset.x -= 1.0f;
     if (ScrollOffset.y < 0.0f)
       ScrollOffset.y += 1.0f;
     else if (ScrollOffset.y >= 1.0f)
       ScrollOffset.y -= 1.0f;
     GetComponent<Renderer>().material.mainTextureOffset = ScrollOffset;
	}
}
