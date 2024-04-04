using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField]
    private float _moveSpeed = 10;

    [SerializeField]
    private HealthBar _healthBar = null;
    
    private Rigidbody2D _rigidbody2D = null;
    private FixedJoystick _joystick = null;
    
    protected override void SingleAwake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_rigidbody2D == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Component Rigidbody2D not found!");
#endif
        }
    }

    private void FixedUpdate()
    {
        Move();

        Flip();
    }

    private void SetController()
    {
        if (_joystick == null)
        {
            var gameWindow = (GameWindow)WindowSwitcher.Instance.GetWindow<GameWindow>();

            _joystick = gameWindow.Controller;
        }
    }

    private void Move()
    {
        SetController();
        
        _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
    }

    private void Flip()
    {
        if (_joystick.Horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _healthBar.transform.rotation = Quaternion.Euler(0, -transform.rotation.y, 0);
        }
        else if (_joystick.Horizontal > 0)
        {           
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _healthBar.transform.rotation = Quaternion.Euler(0, -transform.rotation.y, 0);
        }
    }
}
