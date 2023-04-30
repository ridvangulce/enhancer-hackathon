using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _panel.SetActive(false);
            StartCoroutine(ComputerErrorMusic());
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

        IEnumerator ComputerErrorMusic()
        {
            yield return new WaitForSeconds(3f);
            SoundManager.Instance.StopLoop();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.computingSound);
            yield return new WaitForSeconds(6f);
            SoundManager.Instance.StopOneShot();
            SoundManager.Instance.oneShotAudioSource.clip = SoundManager.Instance.classicLoopSound;
            SoundManager.Instance.loopAudioSource.clip = SoundManager.Instance.classicLoopSound;
        }
    }
}