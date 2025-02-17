using UnityEngine;

public class DragWithMouse : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Rigidbody _rb;
    private Vector3 _nextPosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        _rb.isKinematic = true;
        _nextPosition = Vector3.zero;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        _nextPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(_nextPosition, Vector3.zero) > 0.01f)
        {
            _rb.position = _nextPosition;
            //_rb.MovePosition(_nextPosition);
        }
    }
    private void OnMouseUpAsButton() //Called only if Mouse UP collider is the same of Mouse Down collider
    {
        _rb.isKinematic = false;
        _nextPosition = Vector3.zero;
    }
}
