using UnityEngine;
using System.Collections;

public class CollidableTexture : MonoBehaviour {

    private Vector2 HalfInitSize { get; set; }
	private Vector3 OriginInWorld { get; set; }  // bottom-left of the gameObject
    private float WidthInWorld { get { return HalfInitSize.x * transform.localScale.x * 2f; } }
    private float HeightInWorld { get { return HalfInitSize.y * transform.localScale.y * 2f; } }
	private int WidthInPixel { get; set; }
	private int HeightInPixel { get; set; }
	private Color[] mTextureColors = null;
	private SpriteRenderer mMyRenderer;
			
	void Start()
    {
        #region assuming no initial rotation, compute the initial size of the sprite
		mMyRenderer = GetComponent<SpriteRenderer>();
        Bounds b = mMyRenderer.bounds;
        HalfInitSize = new Vector2(b.extents.x / transform.localScale.x,
                                   b.extents.y / transform.localScale.y);
        #endregion

        InitTextureCollision();
	}
	
    /// <summary>
    /// Assumes this and other both has SpriteRenderer, and both has CollidableTexture components
    /// </summary>
	public bool CollideTextures(CollidableTexture otherTex, Bounds otherBound, out Vector3 collidePoint)
    {
        collidePoint = Vector3.zero;
                
        #region details of per-pixel texture collision
		bool touches = mMyRenderer.bounds.Intersects(otherBound);
		if (touches) {
			bool pixelTouch = false;         
			ComputeOrigin();
			otherTex.ComputeOrigin();
		    int i=0;
		
		    while ( (!pixelTouch) && (i<WidthInPixel) ) 
		    {
		        int j = 0;
		        while ( (!pixelTouch) && (j<HeightInPixel) )
		        {
		            collidePoint = IndexToCameraPosition(i, j);
	            	Color myColor = GetColor(i, j);

		            if (myColor.a > 0)
		            {
		                Vector2 otherIndex = otherTex.CameraPositionToIndex(collidePoint);
		                if (otherTex.IndexInBound(otherIndex))	                    
		                {
		                    pixelTouch = (otherTex.GetColor((int)(otherIndex.x), (int)(otherIndex.y)).a > 0);
		                }
		            }
		            j++;
		        }
		        i++;
		    }
		    touches = pixelTouch;
		}
		#endregion
		return touches;

		/*
(105, 45): is the tip of the net-outline!

		Vector2 ind = otherTex.CameraPositionToIndex(IndexToCameraPosition(105, 45));
		collidePoint = otherTex.IndexToCameraPosition((int)ind.x, (int)ind.y);
		// collidePoint = IndexToCameraPosition(105, 45);
		return true;
*/
	}
	
	#region Texture collision supportw
	// call from Start()
	protected void InitTextureCollision() 
	{
        Texture2D t = GetComponent<SpriteRenderer>().sprite.texture;
		mTextureColors = null;
		if (null != t) {
			WidthInPixel = t.width;
			HeightInPixel = t.height;
		
			mTextureColors = t.GetPixels(0, 0, WidthInPixel, HeightInPixel);
		}
	}
	
	
	
	private Color GetColor(int i, int j) {
		return mTextureColors[i + j*WidthInPixel];
	}
	
    private Vector3 IndexToCameraPosition(int i, int j)  {
        float x = i * WidthInWorld / (float)(WidthInPixel-1);
        float y = j * HeightInWorld / (float)(HeightInPixel-1);
	
		Vector3 pos = OriginInWorld + x * transform.right + y * transform.up;
		pos.z = 0f;
		return pos;
    }

    private Vector2 CameraPositionToIndex(Vector3 pos)  {
		Vector3 delta = pos - OriginInWorld;
		delta.z = 0f;
        float i = (WidthInPixel-1) * (Vector3.Dot(delta, transform.right) / WidthInWorld);
        float j = (HeightInPixel-1) * (Vector3.Dot (delta, transform.up) / HeightInWorld);
        return new Vector2(i, j);
    }
	
	private bool IndexInBound(Vector2 index) {
		return ((index.x >= 0) && (index.x <= (WidthInPixel-1)) && (index.y >= 0) && (index.y <= (HeightInPixel-1)));
	}
	
	private void ComputeOrigin() {
		OriginInWorld = transform.position                 // middle of the bound (object position)
                         - (HalfInitSize.x * transform.localScale.x * transform.right)  // move left by extent.x
                         - (HalfInitSize.y * transform.localScale.y * transform.up);    // move up by extent.y
	}
	#endregion
}
