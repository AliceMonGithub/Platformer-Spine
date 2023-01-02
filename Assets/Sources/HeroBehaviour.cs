using UnityEngine;

public class HeroBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _speed;

    [Header("Component")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _debug;

    private bool _controllable = true;
    private bool _onGround = true;

    public bool Walking => Input.GetAxis("Horizontal") != 0;
    public float InputAxis => Input.GetAxisRaw("Horizontal");

    public bool Controllable => _controllable;
    public bool OnGround => _onGround;

    private void Awake()
    {
        _debug.material.color = Color.red;
    }

    private void Update()
    {
        if(_rigidbody.velocity.magnitude <= 0.02f)
        {
            _controllable = true;
        }

        if (_controllable == false) return;
        if (_onGround == false) return;

        float input = Input.GetAxis("Horizontal");

        _rigidbody.velocity = new Vector3(_speed * input * Time.deltaTime, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.eulerAngles.z == 315)
        {
            print("Enter");

            _debug.material.color = Color.green;

            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y);

            _controllable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _onGround = false;

        if (collision.transform.eulerAngles.z == 315)
        {
            print("Exit");

            _debug.material.color = Color.red;

            _controllable = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _onGround = true;
    }
}
