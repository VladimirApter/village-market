using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForMenu : Sounds
{
    private void Start()
    {
        Play(sounds[0], loopFlag: true);
    }
}
