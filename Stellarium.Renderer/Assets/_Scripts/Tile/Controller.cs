using System;
using Stellarium.Renderer.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stellarium.Renderer
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;
        private IEnumerator _mainCoroutine;

        public float speed = 1.0f;

        private void Start()
        {
            _mainCoroutine = CoMainMovement();
            StartCoroutine(_mainCoroutine);
        }

        private void OnDestroy()
        {
            StopCoroutine(_mainCoroutine);
        }

        private IEnumerator CoMainMovement()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var selectedTile =
                        HexagonTile.GetTile(camera.ScreenToWorldPoint(Input.mousePosition));
                    Debug.Log($"Clicked Pos: {camera.ScreenToWorldPoint(Input.mousePosition)}");
                    Debug.Log($"Calculated tile Pos: {selectedTile}");
                    transform.position = selectedTile.GetPosition();
                }
                yield return null;
            }
        }
    }
}
