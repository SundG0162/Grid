using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial8 : Tutorial
{
    public override bool Excute()
    {
        return !GridManager.Instance.isChanging;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "�پ��� �� ���� ������ ����մϴ�.";
    }
}
