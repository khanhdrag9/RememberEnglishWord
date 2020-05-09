using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public void SetClick(int index, string title, GameManager manager)
    {
        GetComponentInChildren<Text>().text = $"Unit {index + 1}: {title}";
        GetComponent<Button>().onClick.AddListener(()=>
        {
            manager.unit = index;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        });
    }
}
