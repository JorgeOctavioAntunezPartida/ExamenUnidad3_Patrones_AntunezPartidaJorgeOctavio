using UnityEngine;

public class SportsTires : VehicleDecorator
{
    public SportsTires(VehicleComponent vehicle) : base(vehicle) { }


    public override float MaxSpeed => _innerVehicle.MaxSpeed;
    public override float Torque => _innerVehicle.Torque + 10;
    public override float Acceleration => _innerVehicle.Acceleration + 0.5f;
    public override float FuelCapacity => _innerVehicle.FuelCapacity;
    public override string Color => _innerVehicle.Color;
}
