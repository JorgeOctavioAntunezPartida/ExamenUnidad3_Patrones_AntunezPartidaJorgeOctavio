using UnityEngine;

public class NitroBoos : VehicleDecorator
{
    public NitroBoos(VehicleComponent vehicle) : base(vehicle) { }

    public override float MaxSpeed => _innerVehicle.MaxSpeed + 20f;
    public override float Torque => _innerVehicle.Torque;
    public override float Acceleration => _innerVehicle.Acceleration;
    public override float FuelCapacity => _innerVehicle.FuelCapacity - 30f;
    public override string Color => _innerVehicle.Color;
}