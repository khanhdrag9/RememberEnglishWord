using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ResourceLoader resourceLoader { get; private set; }
    public int unit;

    void Awake()
    {
        var find = FindObjectsOfType<GameManager>();
        if (find.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        resourceLoader = new ResourceLoader();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Play()
    {

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Play();
        }
        else if (scene.buildIndex == 0)
        {
        }
    }
}
