using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonThunderParam : MonoBehaviour
{
    public GameObject scaffold;
    public GameObject thunder;
    public SpriteRenderer scaffold_color;

    public float timer = 0.0f;

    public DragonThunder.STATE_THUNDER state = DragonThunder.STATE_THUNDER.NONE;
}
