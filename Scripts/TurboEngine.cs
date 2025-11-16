using UnityEngine;

public class TurboEngine: VehicleDecorator
{
    public TurboEngine(VehicleComponent vehicle) : base(vehicle) { }

    public override float MaxSpeed => _innerVehicle.MaxSpeed + 20f;
    public override float Torque => _innerVehicle.Torque;
    public override float Acceleration => _innerVehicle.Acceleration + 0.15f;
    public override float FuelCapacity =>  _innerVehicle.FuelCapacity;
    public override string Color => _innerVehicle.Color;
}