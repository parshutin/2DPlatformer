using UnityEngine;

namespace Assets.Scripts
{
    public class Cleaner : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == Tags.Player)
            {
                GameObject.FindGameObjectWithTag(Tags.PlayerCamera).GetComponent<CameraFollow>().enabled = false;
                if (GameObject.FindGameObjectWithTag(Tags.PlayerHealth).activeSelf)
                {
                    GameObject.FindGameObjectWithTag(Tags.PlayerHealth).SetActive(false);
                }

                Destroy(col.gameObject);
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
    }
}