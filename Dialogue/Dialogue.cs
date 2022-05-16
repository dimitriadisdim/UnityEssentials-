using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string node;
    private DialogueRunner _runner;
    
    // Start is called before the first frame update
    void Start()
    {
        var d = GameObject.Find("Dialogue System");
        if (d == null) {
            d = new GameObject();
            d.AddComponent<DialogueRunner>();
        }
        
        _runner = d.GetComponent<DialogueRunner>();
    }

    public void StartDialogue()
    {
        _runner.StartDialogue(node);
    }
}
