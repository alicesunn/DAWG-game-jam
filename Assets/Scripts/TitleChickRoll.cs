using UnityEngine;

public class TitleChickRoll : MonoBehaviour
{
    private SpriteRenderer rend;

    private const float MAX_ROLL_SPEED = 500.0f;
    private float rollDeg = 0.0f; // degrees not radians
    private Vector3 rollAxis = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // Have chicks start facing a random angle
        rollDeg = Random.Range(0.0f, 359.0f);
        rollAxis.x = Mathf.Cos(rollDeg * Mathf.Deg2Rad);
        rollAxis.y = Mathf.Sin(rollDeg * Mathf.Deg2Rad);
        transform.right = rollAxis;

        // Always face left for title screen
        rend.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRoll();
    }

    private void HandleRoll()
    {
        //rollAxis = chickScript.FacingRight() ? Vector3.back : Vector3.forward;
        transform.Rotate(MAX_ROLL_SPEED * Time.deltaTime * Vector3.forward, Space.World);
    }
}
