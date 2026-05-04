using UnityEngine;

public class puddle : TemporaryObject
{
    public float fadeSpeed = 1.0f;

    public SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (selfDestructTimer < 1)
        {
            Color currentColor = sr.color;

            float newAlpha = currentColor.a - (fadeSpeed * Time.deltaTime);
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
    }
}
