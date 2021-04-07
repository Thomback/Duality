using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToNextLevel : MonoBehaviour
{

    GameObject currentEnnemyObject;
    BattleStats currentEnnemyStats;

    bool canBeLoaded, isLoaded;

    AsyncOperation async = null;

    // Start is called before the first frame update
    void Start()
    {
        currentEnnemyObject = GameObject.FindWithTag("Enemy");
        currentEnnemyStats = currentEnnemyObject.GetComponent<BattleStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pas sur que ça soit propre
        canBeLoaded = currentEnnemyObject == null;

        if (async != null && async.isDone)
        {
            async = null;
            Debug.Log("Chargé !");
        }
    }

    public void LoadNextScene()
    {
        if (!canBeLoaded) return;
        Debug.Log("Commence le chargement");
        async = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }
}
