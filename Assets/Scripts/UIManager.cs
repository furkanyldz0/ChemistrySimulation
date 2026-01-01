using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button lookAtMainExperimentTableButton;
    [SerializeField] private Button lookAtFireExperimentTableButton;
    [SerializeField] private Button LookAtShelfButton;
    [SerializeField] private Transform leftCorner1, leftCorner2, rightCorner1, rightCorner2;

    public void DisableLookAtShelfButton() {
        lookAtFireExperimentTableButton.gameObject.SetActive(true);
        lookAtMainExperimentTableButton.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(false);
        lookAtFireExperimentTableButton.transform.position = leftCorner1.position;
        lookAtMainExperimentTableButton.transform.position = leftCorner2.position;
    }
    public void DisableLookAtMainExperimentTableButton() {
        lookAtMainExperimentTableButton.gameObject.SetActive(false);
        lookAtFireExperimentTableButton.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(true);
        LookAtShelfButton.transform.position = rightCorner1.position;
        lookAtFireExperimentTableButton.transform.position = leftCorner1.position;
    }
    public void DisableLookAtFireExperimentTableButton() {
        lookAtFireExperimentTableButton.gameObject.SetActive(false);
        lookAtMainExperimentTableButton.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(true);
        LookAtShelfButton.transform.position = rightCorner1.position;
        lookAtMainExperimentTableButton.transform.position = rightCorner2.position;
    }
}
