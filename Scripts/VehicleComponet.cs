using UnityEngine;

public abstract class VehicleComponent
{
    public abstract float MaxSpeed { get; }
    public abstract float Torque { get; }
    public abstract float Acceleration { get; }
    public abstract float FuelCapacity { get; }
    public abstract string Color { get; }
}