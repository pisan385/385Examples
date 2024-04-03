using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour
{
    private int maxPlanes = 5;
    private int numberOfPlanes = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (numberOfPlanes < maxPlanes)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();
            Assert.IsTrue(s != null);

            GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject); // Prefab MUST BE locaed in Resources/Prefab folder!
            Vector3 pos;
            pos.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x;
            pos.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y;
            pos.z = 0;
            e.transform.localPosition = pos;
            ++numberOfPlanes;
        }
    }

    public void EnemyDestroyed() {
        --numberOfPlanes;
    } 
}
