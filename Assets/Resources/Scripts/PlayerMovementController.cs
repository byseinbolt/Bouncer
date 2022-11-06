using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    public UnityEvent<int> IncreaseMovementCount;
    
    [SerializeField] 
    private float _force = 250;
    
    private readonly Vector3 _startPosition = Vector3.zero;
    private Rigidbody _rigidbody;
    private Vector3 _target;
    private int _movementCount;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                MoveTowardsSelectedPoint(hitInfo);
            }
        }
        
    }

    private void MoveTowardsSelectedPoint(RaycastHit hitInfo)
    {
        var direction = (hitInfo.point - transform.position).normalized;
        
        _rigidbody.AddForce(new Vector3(direction.x, 0, direction.z) * _force);

        _movementCount++;
        IncreaseMovementCount.Invoke(_movementCount);
    }

    public void MoveToStartPoint()
    {
        transform.position = _startPosition;
        
        _rigidbody.velocity = Vector3.zero;
    }
}