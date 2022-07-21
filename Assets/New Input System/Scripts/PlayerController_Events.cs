using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// To access the InputActions, in this script we use Events
/// </summary>
public class PlayerController_Events : MonoBehaviour
{
    private PlayerControls _playerControls;

    private InputAction _eventShoot;
    private InputAction _eventOpenPauseMenu;

    private PlayerControllerSettings _playerSettings;

    public static UnityAction OnOpenPauseMenuCalled;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerSettings = GetComponent<PlayerControllerSettings>();
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();

        _eventShoot = _playerControls.Player.Shoot;
        _eventOpenPauseMenu = _playerControls.Player.OpenPauseMenu;
        
        _eventShoot.started += Shoot;
        _eventOpenPauseMenu.started += RaisePauseMenuEvent;
        
        UIController.OnPauseMenuClosed += PauseMenuClosed;
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
        
        _eventShoot.started -= Shoot;
        _eventOpenPauseMenu.started -= RaisePauseMenuEvent;
        
        UIController.OnPauseMenuClosed -= PauseMenuClosed;
    }

    private void Update() => Move();

    private void Move()
    {
        Vector2 t_input = _playerControls.Player.Movement.ReadValue<Vector2>();
        transform.position += (Vector3) t_input * (_playerSettings.Speed * Time.deltaTime);
    }
    
    private void Shoot(InputAction.CallbackContext pContext) => Debug.Log($"<size=22><color=white>Shoot!</color></size><size=13><color=orange>{pContext}</color></size>");

    private void RaisePauseMenuEvent(InputAction.CallbackContext pContext)
    {
        Debug.Log($"<size=22><color=white>Open Pause Menu!</color></size><size=13><color=orange>{pContext}</color></size>");
        OnOpenPauseMenuCalled?.Invoke();
        
        _playerControls.Player.Disable();
        _playerControls.UI.Enable();
    }

    private void PauseMenuClosed()
    {
        _playerControls.UI.Disable();
        _playerControls.Player.Enable();
    }
}
