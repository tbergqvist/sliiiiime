using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float levelChangeRateSeconds = 10f;
    public List<GameObject> levels;
    private Vector3 targetCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("onLevelChanged", levelChangeRateSeconds, levelChangeRateSeconds);
        targetCameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x >= targetCameraPosition.x)
        {
            Camera.main.transform.position = targetCameraPosition;
        }
        else
        {
            Camera.main.transform.position += Vector3.right * 5f * Time.deltaTime;
        }
    }

    void onLevelChanged()
    {
        int levelIndex = Random.Range(0, levels.Count);
        targetCameraPosition += new Vector3(24, 0, 0);
        var level = Instantiate(levels[levelIndex], targetCameraPosition, Quaternion.identity);
        Destroy(level, levelChangeRateSeconds * 4);
    }
}
