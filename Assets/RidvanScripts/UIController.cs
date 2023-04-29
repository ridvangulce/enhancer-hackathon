using System.Collections;
using UnityEngine;

namespace Assets.RidvanScripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _creditsPanel;
        [SerializeField] private Transform _creditsTextContainer;

        public void StartGame()
        {
            _panel.SetActive(false);
            StartCoroutine(_cameraController.Timer());
        }

        public void Credits()
        {
            StartCoroutine(ActiveCredits());
        }

        IEnumerator ActiveCredits()
        {
            _creditsPanel.SetActive(true);
            Animator anim = _creditsTextContainer.GetComponent<Animator>();
            anim.SetBool("isCredit", true);
            yield return new WaitForSeconds(11f);
            _creditsPanel.SetActive(false);

            
        }
    }
}