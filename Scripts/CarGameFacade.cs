using UnityEngine;

public class CarGameFacade : MonoBehaviour
{
    public void AddAllDecorators()
    {
        Manager._instance.TurboEngineAdd();
        Manager._instance.SportsTiresAdd();
        Manager._instance.ExtraFuelTankAdd();
        Manager._instance.NitroBoosAdd();
        Manager._instance.GothicStyleAdd();
    }
}