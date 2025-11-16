using UnityEngine;

public class Vehicle : VehicleComponent
{
    public override float MaxSpeed => 45f;
    public override float Torque => 100;
    public override float Acceleration => 0.2f;
    public override float FuelCapacity => 100f;
    public override string Color =>  "red";
}