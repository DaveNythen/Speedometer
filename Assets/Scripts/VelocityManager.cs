using UnityEngine;

public class VelocityManager : MonoBehaviour
{
    public delegate void VelocityChanged(float vel);
    public static event VelocityChanged OnVelocityChange;

    private float velocity = 0;
    private float acceletarion = 60f;
    private float deceleration = 35f;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.A) && velocity < AppConstants.MAX_VELOCITY)
        {
            velocity += acceletarion * Time.deltaTime;
            OnVelocityChange?.Invoke(velocity);
        }
        if (!Input.GetKey(KeyCode.A) && velocity > 0)
        {
            velocity -= deceleration * Time.deltaTime;
            OnVelocityChange?.Invoke(velocity);
        }
    }
}
