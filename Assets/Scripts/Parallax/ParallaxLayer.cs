using UnityEngine;
 
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;

    private Vector3 _internalPos;

    private void Start()
    {
        _internalPos = transform.localPosition;
    }
 
    public void Move(float delta)
    {
        Vector3 newPos = _internalPos;
        newPos.x -= delta * parallaxFactor;
        
        // Added for Pixel Snapping
        transform.localPosition = new Vector2(
            Mathf.Round(newPos.x * 48f) / 48f,
            newPos.y
            );

        _internalPos = newPos;
    }
 
}