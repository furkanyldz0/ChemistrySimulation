using UnityEngine;

[CreateAssetMenu()]
public class LabObjectSO : ScriptableObject
{
    [Header("Malzeme Özellikleri")]
    public string objectName;
    public Transform prefab;
    public bool isLiquid;
    public bool hasMultipleMeshes;

    public Color color;
}
