using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    private Vector3 startingPosition;

    public int lives = 3; // Add lives
    public GameObject gameOverUI;
    public PlayerLivesUI playerLivesUI;
    public GameManager gameManager;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion")){
            DeathSequence();
            gameManager.LoseLife(gameObject);
        }
    }

    
    public void DeathSequence()
    {
        enabled = false; //disables MovementController

        GetComponent<BombController>().enabled= false;

        spriteRendererDown.enabled= false;
        spriteRendererLeft.enabled= false;
        spriteRendererRight.enabled= false;
        spriteRendererUp.enabled= false;

        spriteDeath.enabled = true;

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1.25f); // Respawn after a delay if lives are greater than 0
        }
        else
        {
            Invoke(nameof(OnDeathSequenceEnded), 1.25f); // Call OnDeathSequenceEnded if no lives left
        }
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
    public void Respawn()
    {
        // Reset player position or other respawn logic
        transform.position = startingPosition;
        enabled = true;
        GetComponent<BombController>().enabled = true;
        spriteDeath.enabled = false;
        spriteRendererDown.enabled = true; // Set initial sprite renderer
        activeSpriteRenderer = spriteRendererDown;
    }
        private void GameOver()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;
        gameOverUI.SetActive(true); // Show Game Over UI
    }

}
