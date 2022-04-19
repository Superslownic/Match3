using System;
using UnityEngine;

namespace Sources.Components
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WorldSpaceButton : MonoBehaviour
    {
        public event Action OnClick;
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButton(0))
                OnClick?.Invoke();
        }
        
        // private void OnMouseDown()
        // {
        //     OnClick?.Invoke();
        // }
    }
}