using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// To access the InputActions in this script, we use Interfaces
/// </summary>
public class PlayerController_Interfaces : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls _playerControls;
    private PlayerControllerSettings _playerSettings;

    public static UnityAction OnOpenPauseMenuCalled;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Player.SetCallbacks(this);

        _playerSettings = GetComponent<PlayerControllerSettings>();
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
        
        UIController.OnPauseMenuClosed += PauseMenuClosed;
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
        
        UIController.OnPauseMenuClosed -= PauseMenuClosed;
    }

    private void Update() => Move();

    private void Move()
    {
        Vector2 t_input = _playerControls.Player.Movement.ReadValue<Vector2>();
        transform.position += (Vector3) t_input * (_playerSettings.Speed * Time.deltaTime);
    }
    
    public void OnShoot(InputAction.CallbackContext pContext)
    {
        if (pContext.started)
            Debug.Log($"<size=22><color=white>Shoot!</color></size><size=13><color=orange>{pContext}</color></size>");
    }

    public void OnMovement(InputAction.CallbackContext pContext) { }
    
    public void OnOpenPauseMenu(InputAction.CallbackContext pContext)
    {
        if (pContext.started)
        {
            Debug.Log($"<size=22><color=white>Open Pause Menu!</color></size><size=13><color=orange>{pContext}</color></size>");
            OnOpenPauseMenuCalled?.Invoke();
        
            _playerControls.Player.Disable();
            _playerControls.UI.Enable();
        }
    }
    
    private void PauseMenuClosed()
    {
        _playerControls.UI.Disable();
        _playerControls.Player.Enable();
    }
}
