using UnityEngine;

public class LabObject : MonoBehaviour
{
    [SerializeField] private LabObjectSO labObjectSO;

    public LabObjectSO GetLabObjectSO() {
        return labObjectSO;
    }
}
