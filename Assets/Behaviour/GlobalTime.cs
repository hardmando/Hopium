using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    public class GlobalTime : MonoBehaviour
    {
        private static GlobalTime _instance;
        public static GlobalTime Instance => _instance;
        
        private static int _currentHour;
        private static int _currentMinute;
        private static float _currentSecond;
        private static bool _isActive = false;
        private static bool _isArrival = false; 

        private Text _text;
        [SerializeField] private float timeMultiplier = 1f;
        private void Awake()
        {
            _instance = this;
            _text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            if (_isActive)
            {
                UpdateTime();
                _text.text = _currentHour + ":" + _currentMinute + ":" + _currentSecond;
            }
        }

        [ContextMenu("Start Timeflow")]
        public void StartTime()
        {
            _isArrival = false;
            _currentHour = 8;
            _currentMinute = 0;
            _currentSecond = 0;
            _isActive = true;
        }

        private void UpdateTime()
        {
            _currentSecond += Time.deltaTime * timeMultiplier;
            if (_currentSecond >= 60)
            {
                _currentSecond = 0;
                _currentMinute++;
            }

            if (_currentMinute >= 60)
            {
                _currentMinute = 0;
                _currentHour++;
            }
            
            CheckEvents();
        }

        private void CheckEvents()
        {
            if (_currentHour == 8 && !_isArrival) StartFirstArrival();
            else if (_currentHour == 9 && _isArrival) StartFirstDispense();
            else if (_currentHour == 10 && !_isArrival) StartSecondArrival();
            else if (_currentHour == 12 && _isArrival) StartSecondDispense();
            else if (_currentHour == 14 && !_isArrival) StartThirdArrival();
            else if (_currentHour == 15 && _isArrival) StartThirdDispense();
            else if (_currentHour == 16 && !_isArrival) StartFreeTime();
            else if (_currentHour >= 24 && _isArrival) EndDay();
        }

        private void StartFirstArrival()
        {
            _isArrival = true;
            Debug.Log("Start First Arrival");
        }

        private void StartFirstDispense()
        {
            _isArrival = false;
            Debug.Log("Start First Dispense");
        }
        private void StartSecondArrival()
        {
            _isArrival = true;
            Debug.Log("Start Second Arrival");
        }

        private void StartSecondDispense()
        {
            _isArrival = false;
            Debug.Log("Start Second Dispense");
        }
        private void StartThirdArrival()
        {
            _isArrival = true;
            Debug.Log("Start Third Arrival");
        }

        private void StartThirdDispense()
        {
            _isArrival = false;
            Debug.Log("Start Third Dispense");
        }

        private void StartFreeTime()
        {
            _isArrival = true;
            Debug.Log("Start Free Time");
        }

        private void EndDay()
        {
            Debug.Log("End Day");
            _isArrival = false;
        }
    }
}
