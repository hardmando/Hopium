using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class CyclesManager : MonoBehaviour
    {
        private readonly string _pathToJson = Path.Combine(Environment.CurrentDirectory, "Assets/JSON/packages.json");
        public static ArrayList _packages = new ArrayList();
        [SerializeField] private int amount;
        private Transform _spawnPoint;

        public GameObject smallPrefab, medPrefab, bigPrefab, defaultPrefab;

        private void Awake()
        {
            _spawnPoint = GameObject.Find("SpawnPoint").transform;
            LoadPackages();
        }

        [ContextMenu("Load Packages")]
        public void LoadPackages()
        {
            using (var sr = new StreamReader(_pathToJson))
            {
                string json = sr.ReadLine();
                while (json != null)
                {
                    var p = JsonConvert.DeserializeObject<Package>(json);
                    _packages.Add(p);
                    json = sr.ReadLine();
                }
                sr.Close();
            }
            foreach(Package p in _packages) Debug.Log(p.Id);
        }
        
        [ContextMenu("Add boxes")]
        public void AddBoxes()
        {
            var mesh = defaultPrefab;
            
            for (int i = 0; i < amount; i++)
            {
                int seed = GeneratePackageSeed();
                Package package = new Package(seed);
                _packages.Add(package);
            }

            using (StreamWriter sw = new StreamWriter(_pathToJson))
            {
                for (int i = 0; i < _packages.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(_packages[i]);
                    sw.Write(json);
                    sw.WriteLine("\r");
                }
                sw.Close();
            }

            foreach (Package package in _packages)
            {
                mesh = package._size switch
                {
                    PackageSize.Small => Instantiate(smallPrefab, _spawnPoint),
                    PackageSize.Medium => Instantiate(medPrefab, _spawnPoint),
                    PackageSize.Big => Instantiate(bigPrefab, _spawnPoint),
                    _ => defaultPrefab
                };
                mesh.GetComponent<PackageData>().InsertData(package);
            }

        }

        private int GeneratePackageSeed()
        {
            // Size size, float weight, bool needsRef, bool fragile 100101
            string seed = "" + Random.Range(1, 4).ToString("D") + "" + Random.Range(001, 1000).ToString("D3") + "" + Random.Range(0, 2).ToString("D") + "" +
                          Random.Range(0, 2).ToString("D");
            return int.Parse(seed);
        }
    }
}