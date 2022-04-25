using System;
using Sources;
using Sources.Components;
using Sources.StateManagement;
using Sources.UnitStates;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private InputHandler _button;
    [SerializeField] private AnimationCurve _speed;

    private Unit _model;
    private UnitView _prev;
    private UnitView _next;
    private StateMachine _stateMachine;

    public void Construct(Unit model, UnitConfig config)
    {
        _model = model;
        _renderer.color = config.Color;
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new Idle(_stateMachine, _model, _button, this, GetPrev, GetNext));
        _stateMachine.AddState(new Fall(_stateMachine, _model, _speed, this, GetNext));
        _stateMachine.Enter<Fall>();
        _model.OnDestroy += HandleDestroy;
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    public void SetPrev(UnitView value)
    {
        _prev = value;
    }

    public void SetNext(UnitView value)
    {
        _next = value;
    }

    public UnitView GetPrev()
    {
        return _prev;
    }

    public UnitView GetNext()
    {
        return _next;
    }

    public void AddClickListener(ClickAction action)
    {
        _button.OnClick += action;
    }
    
    public void AddDragListener(DragAction action)
    {
        _button.OnDrag += action;
    }

    private void HandleDestroy(Unit obj)
    {
        if(_prev) _prev.SetNext(_next);
        if(_next) _next.SetPrev(_prev);
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(_prev) Gizmos.DrawRay(transform.position, (_prev.transform.position - transform.position) / 2);
        Gizmos.color = Color.red;
        if(_next) Gizmos.DrawRay(transform.position, (_next.transform.position - transform.position) / 2);
    }
}