using Newtonsoft.Json;
using UnityEngine;

namespace Behaviour
{
    public enum PackageSize
    {
        Big, 
        Medium,
        Small,
    }
    [System.Serializable]
    public class Package
    {
        private static int m_Counter = 0;
        public int Id { get; private set; }
        public string _arrivalTime { get; private set; }
        public PackageSize _size { get; private set; }
        public float _weight { get; private set; }
        public bool NeedsRef { get; private set; }
        public bool Fragile { get; private set; }

        [JsonConstructor]
        public Package(int id, string arrivalTime, PackageSize size, float weight, bool needsRef, bool fragile)
        {
            Id = id;
            _arrivalTime = arrivalTime;
            _size = size;
            _weight = weight;
            NeedsRef = needsRef;
            Fragile = fragile;
        }

        public Package(int seed)
        {
            Id = System.Threading.Interlocked.Increment(ref m_Counter);
            _arrivalTime = GlobalTime.Instance.GetTime();
            // seed = 100101
            _size = (seed / 100000) switch
            {
                1 => PackageSize.Small,
                2 => PackageSize.Medium,
                3 => PackageSize.Big,
                _ => _size
            };
            _weight = (seed / 100 - seed / 100000) / 10;
            NeedsRef = (seed / 10 - seed / 100) == 1;
            Fragile = (seed - seed / 10) == 1;
        }
    }
    
}