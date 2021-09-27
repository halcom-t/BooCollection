using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
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
    /// ÉuÅ[ÇUFOÇ…ãzÇ¢çûÇﬁ
    /// </summary>
    public void InhaleBoos()
    {
        foreach (BooData boo in boosManager.activeBoos)
        {
            boo.controller.Inhale(new Vector3(0f, 3.5f, 0f));
        }
    }

}
