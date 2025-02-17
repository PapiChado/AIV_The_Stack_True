using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    public GameManager manager;
    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
