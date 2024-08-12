using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DoingPause : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Paneloptions;
    bool visible = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = Panel.activeInHierarchy;
            visible = visible ? false : true;
            Panel.SetActive(visible);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Paneloptions.SetActive(false);

        }
    }
}
