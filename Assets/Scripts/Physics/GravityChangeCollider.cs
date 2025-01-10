using UnityEngine;

public class GravityChangeCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
