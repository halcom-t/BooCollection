using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    /// <summary>
    /// UFOEffect�I�u�W�F�N�g���A�N�e�B�u��Ԃ�
    /// </summary>
    [System.NonSerialized] public bool isUFOActive = false;

    [SerializeField] GameObject camera;
    BoosManager boosManager;


    // Start is called before the first frame update
    void Start()
    {
        boosManager = camera.GetComponent<BoosManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �A�N�e�B�u�ɂȂ�����
    /// </summary>
    void OnEnable()
    {
        isUFOActive = true;
    }

    /// <summary>
    /// ��A�N�e�B�u�ɂȂ�����
    /// </summary>
    void OnDisable()
    {
        isUFOActive = false;
    }

    /// <summary>
    /// �u�[��UFO�ɋz������
    /// </summary>
    public void AnimEventInhaleBoos()
    {
        foreach (BooData boo in boosManager.activeBoos)
        {
            boo.controller.Inhale(new Vector3(0f, 3.5f, 0f));
        }
    }


}
