using UnityEngine;

public class monkey : PlayerController
{
    public float rotationSpeed = 3f;

    protected override void Update()
    {
        base.Update();

        if (Mathf.Abs(rb.linearVelocity.x) > 0 || Mathf.Abs(rb.linearVelocity.y) > 0)
        {
            if (rb.linearVelocity.x < 0)
            {
                transform.Rotate(0, 0, 100 * Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.Rotate(0, 0, 100 * Time.deltaTime * -1 * rotationSpeed);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }  
    }
}
