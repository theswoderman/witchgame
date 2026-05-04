using UnityEngine;

public class orb : PlayerController
{
    public bool rise;
    public GameObject sprite;
    public float topY;
    public float bottomY;
    public float interval;

    protected override void Start()
    {
        base.Start();
        rise = true;
    }

    protected override void Update()
    {
        base.Update();

        if (rise)
        {
            if (sprite.transform.localPosition.y < topY)
            {
                sprite.transform.localPosition = new Vector3(0, sprite.transform.localPosition.y + interval, 0);
            }
            else
            {
                rise = false;
            }

        }
        else
        {
            if (sprite.transform.localPosition.y > bottomY)
            {
                sprite.transform.localPosition = new Vector3(0, sprite.transform.localPosition.y - interval, 0);
            }
            else
            {
                rise = true;
            }
        }
    }
}
