using UnityEngine;

public class TriangleCharacter : BaseCharacter
{
    protected override void UpdateSpeed(float dt)
    {
        float incValue = MaxSpeed / SpeedLinearIncreaseTime;

        if (Speed > MaxSpeed / 2)
        {
            incValue *= incValue;
        }

        Speed += incValue * dt;
        Speed = Mathf.Clamp(Speed, 0, MaxSpeed);
    }
}
