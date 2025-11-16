using UnityEngine;

public abstract class VehicleDecorator : VehicleComponent
{
    protected VehicleComponent _innerVehicle;

    public VehicleDecorator(VehicleComponent vehicle)
    {
        _innerVehicle = vehicle;
    }
}