using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �V���b�v����I��������̃A�j���[�V�����C�x���g
    /// </summary>
    public void AnimEventCloseShop()
    {
        anim.SetBool("IsClose", false);
        this.gameObject.SetActive(false);
    }

}
