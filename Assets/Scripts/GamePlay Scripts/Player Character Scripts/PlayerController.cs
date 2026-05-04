using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    public static PlayerController Instance;
    private Vector2 movementInput;
    public SpriteRenderer spriteRenderer;
    //private bool facingLeft = false;
    public Camera mainCamera;
    public bool currentlyControlled = true;
    public Inventory inventory;
    public CharacterData characterData;

    protected override void Awake()
    {
        base.Awake();
        if (currentlyControlled == true)
        {
            Instance = this;
            isPlayer = true;
        }
        mainCamera = Camera.main;
    }

    protected override void Start()
    {
        base.Start();
        inventory = GetComponent<Inventory>();
    }
    private void OnEnable()
    {
        GameController.OnLevelUp += FullHeal;
    }

    private void OnDisable()
    {
        GameController.OnLevelUp -= FullHeal;
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void becomeCurrentlyControlledCharacter() //TODO: need to make it so that when a character is selected the others are all marked as not a player & not controlled
    {
        sendNewTargetToCamera();
        PlayerController[] allChars = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        if (allChars != null)
        {
            foreach (PlayerController c in allChars)
            {
                c.enabled = false;
                c.GetComponentInParent<PlayerInput>().enabled = false;
            }
        }
        this.enabled = true;
        this.GetComponentInParent<PlayerInput>().enabled = true;
        currentlyControlled = true;
    }

    public void sendNewTargetToCamera()
    {
        mainCamera.GetComponent<LockCameraToPlayer>().UpdateCameraTarget(transform);
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, movementInput.y * moveSpeed);
    }

    protected override void Update()
    {
        //UpdateFacingDirection();
        base.Update();
        if (movementInput.x != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = movementInput.x < 0;
        }
        if (isPlayer == true && mainCamera != null)
        {
            float targetCamSize = Mathf.Abs(targetScale.x) * 2f;

            if (Mathf.Abs(mainCamera.orthographicSize - targetCamSize) > 0.01f)
            {
                mainCamera.orthographicSize = Mathf.MoveTowards(
                    mainCamera.orthographicSize,
                    targetCamSize,
                    growthSpeed * Time.deltaTime * 2.0f);
            }
        }
    }
}
