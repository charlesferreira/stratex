using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public BlockInfo info;

    public void Init(BlockInfo info)
    {
        this.info = info;
    }

	// Use this for initialization
	void Start () {
        Instantiate(info.sprite, transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
