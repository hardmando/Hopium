using UnityEngine;

namespace Behaviour
{
    public class BoxBehaviour : MonoBehaviour, IPickable
    {
        public void OnFocus()
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * .5f);
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

        public void OutOfFocus()
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

        public void PickUp()
        {
            
        }
    }
}
