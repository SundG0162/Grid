using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial5 : Tutorial
{
    float _time = 0;
    public override bool Excute()
    {
        _time += Time.deltaTime;
        return _time >= 3f;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "���� ũ��� ũ����Ż�� �μ������� �ٲ�ϴ�.";
    }
}
