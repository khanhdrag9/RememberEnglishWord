using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Data/Unit")]
public class UnitData : ScriptableObject
{
    public string title;
    public string fileWords => $"Unit{name[name.Length - 1]}/Words";
}
