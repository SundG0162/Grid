using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial6 : Tutorial
{
    public override bool Excute()
    {
        return Time.timeScale != 1;
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
        _tutorialText.text = "���� ũ����Ż�� �μ��� ���� ��� óġ�˴ϴ�.";
        for(int i = 0; i < 7; i++) 
        {
            EnemySpawner.Instance.EnemySpawn(7);
        }
        EnemySpawner.Instance.EnemySpawn(6);
    }
}
