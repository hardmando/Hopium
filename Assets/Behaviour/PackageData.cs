using Behaviour;
using UnityEngine;

public class PackageData : MonoBehaviour
{
    [SerializeField] private int Id;
    [SerializeField] private string _arrivalTime;
    [SerializeField] private PackageSize _size;
    [SerializeField] private float _weight;
    [SerializeField] private bool NeedsRef;
    [SerializeField] private bool Fragile;
    public void InsertData(Package package)
    {
        Id = package.Id;
        _arrivalTime = package._arrivalTime;
        _size = package._size;
        _weight = package._weight;
        NeedsRef = package.NeedsRef;
        Fragile = package.Fragile;
    }
}
