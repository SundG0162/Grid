using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial7 : Tutorial
{
    public override bool Excute()
    {
        return Time.timeScale > 0;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "���� óġ�� ����ġ�� ��� ������ �� �� �ֽ��ϴ�.";
    }
}
