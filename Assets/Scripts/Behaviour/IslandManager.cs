using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;

namespace Behaviour
{

    public class IslandManager : MonoBehaviour
    {
        private List<GameObject> islands;
        [SerializeField] private Transform[] positionsIslands;
        [SerializeField] private bool[] occupiedIslands;
        [SerializeField] private GameObject islandPrefab;
        [SerializeField] private Transform parentTransform;

        private void Start()
        {
            islands = new List<GameObject>();
        }

        public void CreateIsland(Role role)
        {
            int positionKey = 0;
            bool found = false;
            do
            {
                positionKey = Random.Range(0, positionsIslands.Length);
                if (!occupiedIslands[positionKey])
                {
                    occupiedIslands[positionKey] = true;
                    found = true;
                }
            } while (!found);
            Vector3 finalPosition = positionsIslands[positionKey].position;
            GameObject instanciatedObject = Instantiate(islandPrefab, finalPosition, Quaternion.identity, parentTransform);
        
            instanciatedObject.GetComponent<Island>().InicializeIsland(role, positionKey);
            islands.Add(instanciatedObject);
        }

        public void ResetIslands()
        {
            foreach (var island in islands)
            {
                island.GetComponent<Island>().ResetIsland();
            }
        }
    }
}

