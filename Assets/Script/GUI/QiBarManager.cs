using UnityEngine;
using System.Collections;

public class QiBarManager : MonoBehaviour {

    public Boss boss=null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    public void QiBarOnMouseDown()
    {
        if (boss == null) boss = GameManager.instance.boss.GetComponent<Boss>();
        //if (!GameManager.instance.boss.GetComponent<Boss>().moveLocked) GameManager.instance.bossLock(true, false);

        if (!boss.hatarakeLocked)
        {
            //print("START CHARGE ");
            boss.charge = true;
            boss.tMemory.SetItem("charge", true);
            StartCoroutine(boss.Engueulade());
        }

    }
    void OnPreviewMouseRightButtonDown()
    {

    }
    public void QiBarOnMouseUp()
    {
        boss.charge = false;
        boss.tMemory.SetItem("charge", false);
        //GameManager.instance.bossLock(false, false);
    }
}
