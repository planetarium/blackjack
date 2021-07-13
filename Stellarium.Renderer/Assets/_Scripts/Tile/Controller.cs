using Stellarium.Renderer.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stellarium.Renderer
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private Camera camera = null;

        private void Awake()
        {
            
        }

        private void Update()
        {
            var position = camera.ScreenToWorldPoint(Input.mousePosition);
            var tile = HexagonTile.GetTile(position);
            Debug.Log($"{tile.Q} {tile.R} {tile.S}");
        }
    }
}
