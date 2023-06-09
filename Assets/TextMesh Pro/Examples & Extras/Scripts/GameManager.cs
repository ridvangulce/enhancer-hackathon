using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject destroyTextObj, enemyTurret;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public IEnumerator SendDestroyTexts()
    {
        Debug.Log("�al��t�m");
        yield return new WaitForSeconds(1f);
        GameObject go = Instantiate(destroyTextObj, enemyTurret.transform);
        go.transform.position = enemyTurret.transform.position;
        yield return new WaitForSeconds(2f);
        GameObject go2 = Instantiate(destroyTextObj, enemyTurret.transform);
        go2.transform.position = enemyTurret.transform.position;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.loopAudioSource.Play();
        SoundManager.Instance.loopAudioSource.volume = 1f;
    }
}