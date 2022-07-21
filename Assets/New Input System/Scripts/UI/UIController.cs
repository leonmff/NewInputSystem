using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private EventSystem _eventSystem;
    
    public static UnityAction OnPauseMenuClosed;

    private void Awake() => _eventSystem = EventSystem.current;

    private void OnEnable()
    {
        PlayerController_Events.OnOpenPauseMenuCalled += EnablePauseMenu;
        PlayerController_Interfaces.OnOpenPauseMenuCalled += EnablePauseMenu;
    }

    private void OnDisable()
    {
        PlayerController_Events.OnOpenPauseMenuCalled -= EnablePauseMenu;
        PlayerController_Interfaces.OnOpenPauseMenuCalled -= EnablePauseMenu;
    }

    private void EnablePauseMenu()
    {
        _eventSystem.SetSelectedGameObject(_eventSystem.firstSelectedGameObject);
        _pauseMenu.SetActive(true);
    }

    private void DisablePauseMenu()
    {
        OnPauseMenuClosed?.Invoke();
        _pauseMenu.SetActive(false);
    }

    public void Resume() => DisablePauseMenu();

    public void Settings() => DisablePauseMenu();

    public void Quit() => DisablePauseMenu();
}
