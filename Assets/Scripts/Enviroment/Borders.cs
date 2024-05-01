using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enviroment
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _leftBorder;
        [SerializeField] private BoxCollider2D _rightBorder;
        [SerializeField] private BoxCollider2D _downBorder;
        [SerializeField] private BoxCollider2D _topBorder;

        private void Awake()
        {
            Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

            // Создание верхнего коллайдера
            _topBorder.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
            _topBorder.offset = new Vector2(_topBorder.size.x / 2f, _topBorder.size.y / 2f);
            _topBorder.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, topRightScreenPoint.y, 0f);

            // Создание нижнего коллайдера
            _downBorder.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
            _downBorder.offset = new Vector2(_downBorder.size.x / 2f, _downBorder.size.y / 2f);
            _downBorder.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - _downBorder.size.y, 0f);

            // Создание левого коллайдера
            _leftBorder.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
            _leftBorder.offset = new Vector2(_leftBorder.size.x / 2f, _leftBorder.size.y / 2f);
            _leftBorder.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - _leftBorder.size.x, bottomLeftScreenPoint.y, 0f);

            // Создание правого коллайдера
            _rightBorder.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
            _rightBorder.offset = new Vector2(_rightBorder.size.x / 2f, _rightBorder.size.y / 2f);
            _rightBorder.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);
        }
    }
}