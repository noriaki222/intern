using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonThunderParam : MonoBehaviour
{
    public GameObject scaffold;
    public GameObject thunder;

    private SpriteRenderer scaffold_color;
    public float timer;

    public DragonThunder.STATE_THUNDER state = DragonThunder.STATE_THUNDER.NONE;
}
