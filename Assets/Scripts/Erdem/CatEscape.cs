using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEscape : MonoBehaviour
{
    public static CatEscape instance;

    public Rigidbody2D girlwithcatRB;
    [SerializeField] private float catEscapeSpeed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        girlwithcatRB = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatEscaping()
    {
        transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
    }
}
