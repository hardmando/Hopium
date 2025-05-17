using UnityEngine;

namespace Behaviour
{ 
    public interface IPickable
    {
        public void PickUp(Transform holdPoint);
        public void Drop();
        public void Throw(Vector3 throwVector);
        public void OnFocus();
        public void OutOfFocus();
        public bool IsPicked();
    }
}