using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject unitScreen;
    public UnitButton unitButtonPrefab;

    private GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        ResourceLoader res = manager.resourceLoader;
        for(int i = 0; i < res.NumberUnit; i++)
        {
            Unit unit = res.GetUnit(i);
            var btn = Instantiate(unitButtonPrefab, unitScreen.transform);
            btn.SetClick(i, unit.data.title, manager);
        }

        unitScreen.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GoFindWord()
    {
        unitScreen.SetActive(true);
    }

    public void GoParagraph()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
