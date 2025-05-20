using UnityEngine;
using UnityEngine.UI;

namespace Behaviour
{
    public class GlobalTime : MonoBehaviour
    {
        public static GlobalTime Instance {get; private set;}
        
        private static int _currentHour;
        private static int _currentMinute;
        private static float _currentSecond;
        private static int _currentDay = 1;
        private static int _currentMonth = 0;
        
        private static bool _isActive = false;
        private static bool _isArrival = false;

        private static readonly int[] Days =  {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly string[] Months = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

        private Text _text;
        [SerializeField] private float timeMultiplier = 1f;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            if (_isActive)
            {
                UpdateTime();
                _text.text = _currentHour + ":" + _currentMinute + ":" + (int)_currentSecond + " " + _currentDay + " / " + Months[_currentMonth];
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
            switch (_currentHour)
            {
                case 8 when !_isArrival:
                    StartFirstArrival();
                    break;
                case 9 when _isArrival:
                    StartFirstDispense();
                    break;
                case 10 when !_isArrival:
                    StartSecondArrival();
                    break;
                case 12 when _isArrival:
                    StartSecondDispense();
                    break;
                case 14 when !_isArrival:
                    StartThirdArrival();
                    break;
                case 15 when _isArrival:
                    StartThirdDispense();
                    break;
                case 16 when !_isArrival:
                    StartFreeTime();
                    break;
                case >= 24 when _isArrival:
                    EndDay();
                    break;
            }
        }

        private static void StartFirstArrival()
        {
            _isArrival = true;
            Debug.Log("Start First Arrival");
        }

        private static void StartFirstDispense()
        {
            _isArrival = false;
            Debug.Log("Start First Dispense");
        }
        private static void StartSecondArrival()
        {
            _isArrival = true;
            Debug.Log("Start Second Arrival");
        }

        private static void StartSecondDispense()
        {
            _isArrival = false;
            Debug.Log("Start Second Dispense");
        }
        private static void StartThirdArrival()
        {
            _isArrival = true;
            Debug.Log("Start Third Arrival");
        }

        private static void StartThirdDispense()
        {
            _isArrival = false;
            Debug.Log("Start Third Dispense");
        }

        private void StartFreeTime()
        {
            _isArrival = true;
            Debug.Log("Start Free Time");
        }

        private static void EndDay()
        {
            Debug.Log("End Day");
            _isArrival = false;
            _isActive = false;
            UpdateDate();
        }

        private static void UpdateDate()
        {
            _currentDay++;
            if (_currentDay <= Days[_currentMonth]) return;
            _currentDay = 1;
            _currentMonth++;
        }

        public string GetTime()
        {
            return _currentHour + ":" + _currentMinute + ":" + (int)_currentSecond + " " + _currentDay + " / " +
                   Months[_currentMonth];
        } 
    }
}
