using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : Tutorial
{
    float _time = 0;
    public override bool Excute()
    {
        _time += Time.deltaTime;
        return _time >= 4f;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "���� �����ϴ� ĭ�� ���������� ���ϸ� �����ð� ���� ������ ���ϸ� �������� �Խ��ϴ�.";
    }
}
