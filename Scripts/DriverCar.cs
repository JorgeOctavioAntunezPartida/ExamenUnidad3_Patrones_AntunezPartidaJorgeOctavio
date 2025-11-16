using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DriveCar : MonoBehaviour
{
    [Header("Ruedas")]
    [SerializeField] private Rigidbody2D _frontTireRB;
    [SerializeField] private Rigidbody2D _backTireRB;

    [Header("Cuerpo del coche")]
    [SerializeField] private Rigidbody2D _carBodyRB;

    [Header("Motor")]
    [SerializeField] private float _motorTorque;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeedKmh;
    [SerializeField] private string _color;

    [Header("Freno")]
    [SerializeField] private float _brakeStrength = 8f;

    [Header("Combustible")]
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuelConsumptionRate = 3f;
    public float CurrentFuel { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _statsText;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private float _moveInput;      // -1, 0, 1
    private bool _isBraking;
    private float _throttle;       // 0..1, acelerador progresivo

    public void SetColorSprite(string colorName)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        switch (colorName.ToLower())
        {
            case "red": spriteRenderer.color = Color.red; break;
            case "blue": spriteRenderer.color = Color.blue; break;
            case "green": spriteRenderer.color = Color.green; break;
            case "yellow": spriteRenderer.color = Color.yellow; break;
            case "black": spriteRenderer.color = Color.black; break;
            case "white": spriteRenderer.color = Color.white; break;
            case "cyan": spriteRenderer.color = Color.cyan; break;
            case "magenta": spriteRenderer.color = Color.magenta; break;
            case "gray":
            case "grey": spriteRenderer.color = Color.gray; break;

            default:
                Debug.LogWarning($"Color '{colorName}' no está soportado.");
                break;
        }
    }

    private void Awake()
    {
        UpdateUI();
    }

    private void Start()
    {
        CurrentFuel = Manager._instance.vehicle.FuelCapacity;

        _maxSpeedKmh = Manager._instance.vehicle.MaxSpeed;
        _motorTorque = Manager._instance.vehicle.Torque;
        _acceleration = Manager._instance.vehicle.Acceleration;
        _maxFuel = Manager._instance.vehicle.FuelCapacity;
        _color = Manager._instance.vehicle.Color;

        SetColorSprite(_color);
    }

    private void Update()
    {
        var kb = Keyboard.current;

        // Movimiento izquierda / derecha
        _moveInput = 0f;
        if (kb.aKey.isPressed) _moveInput = -1f;
        else if (kb.dKey.isPressed) _moveInput = 1f;

        // Freno con SPACE
        _isBraking = kb.spaceKey.isPressed;

        // Acelerador progresivo (controlado por _acceleration)
        bool wantsToAccelerate =
            Mathf.Abs(_moveInput) > 0.01f &&
            !_isBraking &&
            CurrentFuel > 0f;

        if (wantsToAccelerate)
        {
            // Mientras mantengas el input, el acelerador sube hacia 1
            _throttle = Mathf.MoveTowards(
                _throttle,
                1f,
                _acceleration * Time.deltaTime
            );
        }
        else
        {
            // Suelta acelerador (vuelve a 0 poco a poco)
            _throttle = Mathf.MoveTowards(
                _throttle,
                0f,
                _acceleration * Time.deltaTime
            );
        }

        UpdateUI();
    }

    private void FixedUpdate()
    {
        // Si estamos frenando, o ya no hay gasolina → frenar
        if (_isBraking || CurrentFuel <= 0f)
        {
            BrakeWheel(_frontTireRB);
            BrakeWheel(_backTireRB);
            LimitMaxSpeed();  // por si viene muy rápido
            return;
        }

        // Aplicar torque al motor, afectado por el "acelerador" (_throttle)
        float torque = -_moveInput * _motorTorque * _throttle * Time.fixedDeltaTime;
        _frontTireRB.AddTorque(torque);
        _backTireRB.AddTorque(torque);

        // Limitar velocidad máxima del coche
        LimitMaxSpeed();

        // Consumir combustible solo cuando realmente hay aceleración
        if (Mathf.Abs(_moveInput) > 0.01f && _throttle > 0.01f)
        {
            float fuelUsed = _fuelConsumptionRate * _throttle * Time.fixedDeltaTime;
            CurrentFuel = Mathf.Max(0f, CurrentFuel - fuelUsed);
        }
    }

    private void BrakeWheel(Rigidbody2D rb)
    {
        rb.angularVelocity = Mathf.Lerp(
            rb.angularVelocity,
            0f,
            _brakeStrength * Time.fixedDeltaTime
        );
    }

    /// <summary>
    /// Limita la velocidad lineal del coche para que no supere _maxSpeedKmh.
    /// </summary>
    private void LimitMaxSpeed()
    {
        if (_carBodyRB == null) return;

        // Convertimos la velocidad máxima de km/h a m/s (Unity units/s)
        float maxSpeedMS = _maxSpeedKmh / 3.6f;

        Vector2 v = _carBodyRB.linearVelocity;
        if (v.magnitude > maxSpeedMS)
        {
            _carBodyRB.linearVelocity = v.normalized * maxSpeedMS;
        }
    }

    private void UpdateUI()
    {
        // UI de combustible
        if (_fuelText != null)
        {
            _fuelText.text = $"Combustible: {CurrentFuel:0.0}/{_maxFuel:0.0}";
        }

        // UI de velocidad
        if (_speedText != null && _carBodyRB != null)
        {
            float speedMS = _carBodyRB.linearVelocity.magnitude;
            float speedKmh = speedMS * 3.6f;
            _speedText.text = $"Velocidad: {speedKmh:0} km/h";
        }

        if (_statsText != null)
        {
            _statsText.text = $"Velocidad Maxima: {_maxSpeedKmh} km/h\nTorque: {_motorTorque}\nAceleracion: {_acceleration}";
        }
    }

    public void SetColor(string colorName)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        switch (colorName.ToLower())
        {
            case "red": spriteRenderer.color = Color.red; break;
            case "blue": spriteRenderer.color = Color.blue; break;
            case "green": spriteRenderer.color = Color.green; break;
            case "yellow": spriteRenderer.color = Color.yellow; break;
            case "black": spriteRenderer.color = Color.black; break;
            case "white": spriteRenderer.color = Color.white; break;
            case "cyan": spriteRenderer.color = Color.cyan; break;
            case "magenta": spriteRenderer.color = Color.magenta; break;
            case "gray":
            case "grey": spriteRenderer.color = Color.gray; break;

            default:
                Debug.LogWarning($"Color '{colorName}' no está soportado.");
                break;
        }
    }

    public void ChangeSceneMain()
    {
        SceneManager.LoadScene("Main");
    }
}