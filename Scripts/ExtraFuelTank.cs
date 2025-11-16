using UnityEngine;

public class ExtraFuelTank : VehicleDecorator
{
    public ExtraFuelTank(VehicleComponent vehicle) : base(vehicle) { }

    public override float MaxSpeed => _innerVehicle.MaxSpeed;
    public override float Torque => _innerVehicle.Torque;
    public override float Acceleration => _innerVehicle.Acceleration;
    public override float FuelCapacity => _innerVehicle.FuelCapacity + 50f;
    public override string Color => _innerVehicle.Color;
}