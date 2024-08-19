using UnityEngine;

public class ChickSpriteScript : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite whiteSprite;
    public Sprite singerSprite;
    private Sprite nextSprite;

    public bool isSinging = false;

    private StateScript state;
    private SpriteRenderer rend;
    private ChickScript chickScript;

    private const float TIME_PER_FLASH = 0.2f;
    private float flashStart = 0.0f;
    private int flashCount = 0;

    private const float MAX_ROLL_SPEED = 800.0f;
    private float rollSpeed = 0.0f;
    private float rollDeg = 0.0f; // degrees not radians
    private Vector3 rollAxis = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        rend = GetComponent<SpriteRenderer>();
        rend.sortingOrder = -1;
        chickScript = gameObject.GetComponentInParent<ChickScript>();

        // Have chicks start facing a random angle
        rollDeg = Random.Range(0.0f, 359.0f);
        rollAxis.x = Mathf.Cos(rollDeg * Mathf.Deg2Rad);
        rollAxis.y = Mathf.Sin(rollDeg * Mathf.Deg2Rad);
        transform.right = rollAxis;

        // Also have them face a random direction
        if (Random.Range(0, 2) == 0) rend.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRoll();

        if (flashCount > 0) Flash();
    }

    private void HandleRoll()
    {
        // (tried to) speed up/slow down rolls with movement speed
        rollSpeed = Mathf.Lerp(0.0f, MAX_ROLL_SPEED, chickScript.speed / chickScript.fastSpeed);
        rollAxis = chickScript.FacingRight() ? Vector3.back : Vector3.forward;
        transform.Rotate(rollSpeed * Time.deltaTime * rollAxis, Space.World);
    }

    public void OnPickup(int count)
    {
        flashCount = count;
        flashStart = Time.time;
        rend.sprite = whiteSprite;
        nextSprite = normalSprite;
    }

    public void Activate()
    {
        isSinging = true;
        rend.sprite = singerSprite;
    }

    private void Flash()
    {
        if (Time.time > flashStart + TIME_PER_FLASH)
        {
            rend.sprite = nextSprite;
            nextSprite = (nextSprite == whiteSprite) ? normalSprite : whiteSprite;
            flashStart = Time.time;

            if (rend.sprite == normalSprite) flashCount--;
        }
    }
}
