using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : Tutorial
{
    float _time = 0;
    public override bool Excute()
    {
        _time += Time.deltaTime;
        return _time >= 2f;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "������ ĭ�� ���� ��ȯ�Ǹ� �Ķ������� ���ǥ�� �˴ϴ�.";
        EnemySpawner.Instance.EnemySpawn(0);
    }
}
