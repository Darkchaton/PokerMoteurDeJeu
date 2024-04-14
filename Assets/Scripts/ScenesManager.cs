using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Source : https://www.youtube.com/watch?v=jrPTpD2eAMw&t=431s

public class ScenesManager : MonoBehaviour
{
    public ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        SampleScene, //game scene
        Banque,
        MagasinItems
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(Scene.SampleScene.ToString());
    } 
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadBanque()
    {
        SceneManager.LoadScene(Scene.Banque.ToString());
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMagasinItem()
    {
        SceneManager.LoadScene(Scene.MagasinItems.ToString());
    }
      
}
