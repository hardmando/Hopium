using UnityEngine;

namespace Behaviour
{ 
    public interface IPickable
    {
        public void PickUp();
        public void OnFocus();
        public void OutOfFocus();
    }
}