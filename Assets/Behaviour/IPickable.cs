using UnityEngine;

namespace Behaviour
{ 
    public interface IPickable
    {
        public void PickUp(Transform holdPoint);
        public void Drop();
        public void Throw();
        public void OnFocus();
        public void OutOfFocus();
    }
}