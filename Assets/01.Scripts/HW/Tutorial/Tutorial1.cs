using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : Tutorial
{
    public override bool Excute()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "����Ű�� ���� �÷��̾��� ���ݹ����� ������ �� �ֽ��ϴ�.";
    }
}
