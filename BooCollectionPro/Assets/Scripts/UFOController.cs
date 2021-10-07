using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{

    [SerializeField] new GameObject camera;
    BoosManager boosManager;

    [System.NonSerialized] public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boosManager = camera.GetComponent<BoosManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    /// <summary>
    /// UFOを非アクティブにする
    /// </summary>
    public void AnimEventDisableUFO()
    {
        this.gameObject.SetActive(false);
    }


}
