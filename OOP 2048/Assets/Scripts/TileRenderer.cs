using UnityEngine;

namespace DefaultNamespace
{
    public class TileRenderer : MonoBehaviour
    {
        public Tile tile { get; private set; }
        
        public TileState[] tileStates;
        
        private void Awake()
        {
            tile = GetComponent<Tile>();
            tile.OnStateChanged += HandleStateChange;
        }

        public void HandleStateChange(int pValue)
        {
            // Clear old representation...
            if (tile.transform.childCount != 0) {
                var oldRepresentation = tile.transform.GetChild(0);
                Destroy(oldRepresentation.gameObject);
            }
            
            // ...and create new one based on state prefab
            var index = Mathf.Clamp((int)Mathf.Log(pValue, 2) - 1, 0, tileStates.Length - 1);
            var nextState = tileStates[index];
            Instantiate(nextState.prefab, parent: tile.transform);
        }
    }
}