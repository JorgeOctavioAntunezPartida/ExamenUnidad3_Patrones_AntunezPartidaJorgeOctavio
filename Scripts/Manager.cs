using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager _instance { get; private set; }
    public VehicleComponent vehicle = new Vehicle();

    void Awake()
    {
        vehicle = DecorarVehiculo(vehicle);

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public int score = 0;

    public VehicleComponent DecorarVehiculo(VehicleComponent vehicle)
    {
        //vehicle = new TurboEngine(vehicle);
        //vehicle = new SportsTires(vehicle);
        //vehicle = new ExtraFuelTank(vehicle);
        //vehicle = new NitroBoos(vehicle);
        //vehicle = new GothicStyle(vehicle);
        return vehicle;
    }

    void Start()
    {
        Debug.Log($"{vehicle.Color} Vel máx: {vehicle.MaxSpeed}, Acel: {vehicle.Acceleration}, Gasolina: {vehicle.FuelCapacity}, Torque: {vehicle.Torque}");
    }

    public void ChangeSceneGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TurboEngineAdd()
    {
        vehicle = new TurboEngine(vehicle);
    }

    public void SportsTiresAdd()
    {
        vehicle = new SportsTires(vehicle);
    }

    public void ExtraFuelTankAdd()
    {
        vehicle = new ExtraFuelTank(vehicle);
    }

    public void NitroBoosAdd()
    {
        vehicle = new NitroBoos(vehicle);
    }

    public void GothicStyleAdd()
    {
        vehicle = new GothicStyle(vehicle);
    }
}