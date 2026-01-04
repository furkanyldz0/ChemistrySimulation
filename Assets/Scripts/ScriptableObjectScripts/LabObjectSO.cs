using UnityEngine;

[CreateAssetMenu()]
public class LabObjectSO : ScriptableObject
{
    [Header("Malzeme Özellikleri")]
    public string objectName;
    public Vector3 deskPosition;
    public bool isLiquid;
    public bool isMetalStick;
    public bool hasMultipleMeshes;

    [Header("Sývý veya Çubuk Rengi")]
    public Color color;
}
