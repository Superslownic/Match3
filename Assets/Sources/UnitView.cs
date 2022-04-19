using System.Collections;
using Sources;
using Sources.Extensions;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AnimationCurve _speed;
    
    private Unit _model;
    private int _targetWaypointIndex;
    private bool _isFalling;
    private bool _isCanFall;

    public float FallTime;
    public UnitView Prev;
    public UnitView Next;

    public void Construct(Unit model, UnitConfig config)
    {
        _model = model;
        _renderer.color = config.Color;
        _model.OnPositionChanged += HandlePositionChanged;
        FallTime = 0;
        _targetWaypointIndex = 0;
        StartCoroutine(WaitNext());
    }

    private void HandlePositionChanged()
    {
        if(_isFalling == false)
            StartCoroutine(Fall());
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            _model.Destroy();
            if(Prev) Prev.Next = Next;
            if(Next) Next.Prev = Prev;
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitNext()
    {
        while (Next != null && Vector3.Distance(transform.position, Next.transform.position) <= 1)
            yield return null;

        _isCanFall = true;
        if(Next) FallTime = Next.FallTime;
    }

    private IEnumerator Fall()
    {
        if(_targetWaypointIndex > _model.GetLastWaypointIndex())
        {
            FallTime = 0;
            _isFalling = false;
            yield break;
        }

        _isFalling = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = _model.GetWaypoint(_targetWaypointIndex).ToVector3();
        while (time < 1)
        {
            while (_isCanFall == false)
                yield return null;
            
            if(Next != null && Vector3.Distance(transform.position, Next.transform.position) <= 1)
                FallTime = Next.FallTime;
            
            FallTime += Time.deltaTime;
            time += _speed.Evaluate(FallTime) * Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition ,targetPosition, time);
            yield return null;
        }
        
        _targetWaypointIndex++;
        
        StartCoroutine(Fall());
    }
}