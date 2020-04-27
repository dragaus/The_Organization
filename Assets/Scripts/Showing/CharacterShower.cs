using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var anim = GetComponent<Animator>();
        anim.SetFloat("Offset", Random.Range(0f, 1f));
        anim.SetBool("IsWorking", true);
    }
}
