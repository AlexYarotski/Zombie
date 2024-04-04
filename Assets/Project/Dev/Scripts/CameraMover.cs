using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    
    private Player _player = null;

    private Vector3 _playerPosition = Vector3.zero;
    
    private void Start()
    {
        _player = Player.Instance;
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            _playerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,
                transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position, _playerPosition, Time.deltaTime * _speed);
        }
    }
}
