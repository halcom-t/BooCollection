using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    /// <summary>
    /// UFOEffectオブジェクトがアクティブ状態か
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
    /// アクティブになった時
    /// </summary>
    void OnEnable()
    {
        isUFOActive = true;
    }

    /// <summary>
    /// 非アクティブになった時
    /// </summary>
    void OnDisable()
    {
        isUFOActive = false;
    }

    /// <summary>
    /// ブーをUFOに吸い込む
    /// </summary>
    public void AnimEventInhaleBoos()
    {
        foreach (BooData boo in boosManager.activeBoos)
        {
            boo.controller.Inhale(new Vector3(0f, 3.5f, 0f));
        }
    }


}
