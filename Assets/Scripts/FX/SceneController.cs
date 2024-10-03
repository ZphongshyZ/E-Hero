using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MyMonobehaviour
{
    //Singleton
    private static SceneController instance;
    public static SceneController Instance { get => instance; }

    [SerializeField] protected Animator transAnm;

    protected override void Awake()
    {
        base.Awake();
        if (SceneController.instance != null) Debug.LogError("Only one SceneController allowed to exist");
        SceneController.instance = this;
    }

     public IEnumerator LoadSceneTransition(string nameScene, Vector3 pos)
    {
        transAnm.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nameScene);
        GameControler.Instance.SetPosPlayer(pos);
        transAnm.SetTrigger("End");
    }    
}
