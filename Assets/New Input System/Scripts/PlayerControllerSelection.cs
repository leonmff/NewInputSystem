using UnityEngine;

public class PlayerControllerSelection : MonoBehaviour
{
    private enum PlayerControllerType { Events, Interfaces }

    [SerializeField] private PlayerControllerType _controlType = PlayerControllerType.Events;

    private PlayerController_Events _controlEvents;
    private PlayerController_Interfaces _controlInterfaces;

    private void Awake()
    {
        _controlEvents = GetComponent<PlayerController_Events>();
        _controlInterfaces = GetComponent<PlayerController_Interfaces>();
    }

    private void OnEnable()
    {
        if (_controlType == PlayerControllerType.Events)
            _controlEvents.enabled = true;
        else if (_controlType == PlayerControllerType.Interfaces)
            _controlInterfaces.enabled = true;
    }
}
