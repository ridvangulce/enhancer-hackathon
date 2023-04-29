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
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}