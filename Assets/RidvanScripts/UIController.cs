using UnityEngine;

namespace Assets.RidvanScripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameObject _panel;

        public void StartGame()
        {
            _panel.SetActive(false);
            StartCoroutine(_cameraController.Timer());
        }
    }
}