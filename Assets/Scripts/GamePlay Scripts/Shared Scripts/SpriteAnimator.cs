using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 2f;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    public int maximumFrames = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentFrame+1 >= maximumFrames && maximumFrames != 0) return;
        timer += Time.deltaTime;

        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f/ framesPerSecond;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
