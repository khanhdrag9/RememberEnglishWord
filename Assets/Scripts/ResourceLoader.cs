using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public class Unit
{
    public UnitData data;
    public Dictionary<string, string> words = new Dictionary<string, string>();

    public void LoadData()
    {
        words.Clear();
        var wordText = Resources.Load<TextAsset>(data.fileWords).text;
        string[] pair = wordText.Split('\n');
        foreach(var e in pair)
        {
            string[] p = e.Trim().Split(':');
            words.Add(p[0].Trim(), p[1].Trim());
        }
    }

    static System.Random r = new System.Random();
    public void Shuffer()
    {
        words = words.OrderBy(x => r.Next()).ToDictionary(item => item.Key, item => item.Value);
    }
}

public class ResourceLoader
{
    private List<Unit> units = new List<Unit>();

    public int NumberUnit => units.Count;

    public ResourceLoader()
    {
        UnitData[] unitDatas = Resources.LoadAll<UnitData>("All");

        for(int i = 0; i < unitDatas.Length; i++)
        {
            Unit unit = new Unit { data = unitDatas[i] };
            unit.LoadData();
            units.Add(unit);
        }
    }

    public Unit GetUnit(int index)
    {
        index = Mathf.Clamp(index, 0, units.Count - 1);
        return units[index];
    }
}
