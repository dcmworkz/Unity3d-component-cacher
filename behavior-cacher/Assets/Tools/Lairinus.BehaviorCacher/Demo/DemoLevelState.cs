using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class DemoLevelState : MonoBehaviour
{
    public static int componentsReadFromCache = 0;
    public static int totalCachedObjects = 0;
    public static float totalCalculationTime = 0;
    public static int totalCollisions = 0;
    public static float totalOnTriggerEnters = 0;
    public static bool useDemoCache = false;
    public Text textCachingDisabled = null;
    public Text textCollisionsPerSecond = null;
    public Text textTotalTime = null;
    public Button toggleCaching = null;
    public bool useCache = false;

    private void Awake()
    {
        totalOnTriggerEnters = 0;
        totalCachedObjects = 0;
        componentsReadFromCache = 0;
        toggleCaching.onClick.AddListener(() => OnClick_ToggleCaching());
    }

    private void OnClick_ToggleCaching()
    {
        useDemoCache = !useDemoCache;
        textCachingDisabled.text = "Cache enabled: " + useDemoCache.ToString();
    }

    private IEnumerator Start()
    {
        useDemoCache = useCache;

        while (true)
        {
            yield return new WaitForSeconds(1);
            textTotalTime.text = "Average Calculation Ticks: " + ((float)totalCalculationTime / (float)totalOnTriggerEnters).ToString();
            textCollisionsPerSecond.text = "Collisions per second: " + ((int)(totalCollisions / 1)).ToString();
            totalCalculationTime = 0;
            totalOnTriggerEnters = 0;
            totalCollisions = 0;
        }
    }
}