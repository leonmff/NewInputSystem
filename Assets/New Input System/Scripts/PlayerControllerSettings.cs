using UnityEngine;

public class PlayerControllerSettings : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    public float Speed => _speed;
}
