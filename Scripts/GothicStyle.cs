using UnityEngine;

public class GothicStyle : VehicleDecorator
{
    public GothicStyle(VehicleComponent vehicle) : base(vehicle) { }

    public override float MaxSpeed => _innerVehicle.MaxSpeed;
    public override float Torque => _innerVehicle.Torque;
    public override float Acceleration => _innerVehicle.Acceleration;
    public override float FuelCapacity => _innerVehicle.FuelCapacity;
    public override string Color => "black";
}