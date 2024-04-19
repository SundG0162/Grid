using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoSingleton<GridManager>
{
    public readonly Vector3 offset = new Vector3(0.5f, 0.5f, 0);
    /// <summary>
    /// 10x10�� �׸��� ����
    /// 0,0 ���� 9,9 ���� �޾ƿ� �� ����
    /// </summary>
    public Vector3Int[,] grid = new Vector3Int[10, 10];
    /// <summary>
    /// �׸����� ��(����)
    /// </summary>
    public int row;
    /// <summary>
    /// �׸����� ��(����)
    /// </summary>
    public int column;
    public Vector2 gridSize;
    [SerializeField]
    Transform _cameraTrm;
    public TileBase tileBase;
    public Tilemap tilemap;
    public GameObject warningObj;
    bool _isCameraMoved = false;
    public bool isChanging = false;
    public bool isFirst = true;
    public void Init()
    {
        row = 9;
        column = 9;
        gridSize = new Vector2(9, 9);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                grid[i, j] = new Vector3Int(i - 5, j - 5);
            }
        }
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {

        //StartCoroutine(Test());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            EnemySpawner.Instance.EnemySpawn(6);
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5f);
        ChangeGrid(6, 4);
        yield return new WaitForSeconds(30f);
        ChangeGrid(5, 5);
        yield return new WaitForSeconds(30f);
        ChangeGrid(3, 2);
        yield return new WaitForSeconds(30f);
        ChangeGrid(7, 5);
    }

    public int[] GetGrid(Vector3Int pos)
    {
        for (int r = 0; r < 10; r++)
        {
            for (int c = 0; c < 10; c++)
            {
                if (grid[c, r] == pos)
                {
                    return new int[2] { c, r };
                }
            }
        }
        return new int[0];
    }

    /// <summary>
    /// ���ϴ� ��� ���� ������ �״�� �ٲ�
    /// </summary>
    /// <param name="row">��</param>
    /// <param name="column">��</param>
    public void ChangeGrid(int row, int column)
    {
        isChanging = true;
        _isCameraMoved = false;
        if (row < 2) row = 2;
        if (column < 2) column = 2;
        if (row > 10) row = 10;
        if (column > 10) column = 10;
        row--;
        column--;

        if (row == this.row && column == this.column)
        {
            row = 5; column = 5;
            if (this.row == 5 && this.column == 5)
            {
                row = 6;
            }
        }
        if (this.row < row) // ���� �þ����
        {
            int count = row - this.row; // ���� count��ŭ �þ
            for (int r = this.row; r <= this.row + count; r++)
            {
                for (int c = 0; c <= this.column; c++)
                {
                    StartCoroutine(ChangeTile(grid[c, r], tileBase));
                }
            }
            this.row = row;
        }
        if (this.column < column)
        {
            int count = column - this.column;
            for (int c = this.column; c <= this.column + count; c++)
            {
                for (int r = 0; r <= this.row; r++)
                {
                    StartCoroutine(ChangeTile(grid[c, r], tileBase));
                }
            }
            this.column = column;
        }
        if (this.row > row) // ���� �پ������
        {
            int count = this.row - row; // ���� count��ŭ �پ��
            for (int r = this.row; r > this.row - count; r--)
            {
                for (int c = 0; c <= this.column; c++)
                {
                    StartCoroutine(ChangeTile(grid[c, r]));
                }
            }
        }
        if (this.column > column)
        {
            int count = this.column - column;
            for (int c = this.column; c > this.column - count; c--)
            {
                for (int r = 0; r <= this.row; r++)
                {
                    StartCoroutine(ChangeTile(grid[c, r]));
                }
            }
        }
        this.row = row;
        this.column = column;
        foreach (Enemy e in EnemySpawner.Instance.enemyList)
        {
            e.Dead();
        }

    }

    IEnumerator ChangeTile(Vector3Int vec, TileBase tileBase = null)
    {
        if (tileBase == null)
        {
            EventSquare ev = PoolManager.Get(InGameManager.Instance.eventSquare, vec + offset, Quaternion.identity).GetComponent<EventSquare>();
            ev.Delete(2f);
        }
        else
        {
            if (tilemap.GetTile(vec) == null)
            {
                EventSquare ev = PoolManager.Get(InGameManager.Instance.eventSquare, vec + offset, Quaternion.identity).GetComponent<EventSquare>();
                ev.Create(2f);
            }
        }
        yield return new WaitForSeconds(2f);
        if (!_isCameraMoved)
        {
            EnemySpawner.Instance.availableGridList.Clear();
            for (int i = 0; i <= row; i++)
            {
                for (int j = 0; j <= column; j++)
                {
                    EnemySpawner.Instance.availableGridList.Add(new int[] { j, i });
                }
            }
            CameraManager.Instance.CameraShake(8, 0.6f);
            SoundManager.Instance.PlaySFX(6);
            gridSize = new Vector2(column, row);
            PlayerMovement.Instance.UpdateHitbox();
            float r = (row + 1) / 2 - 5;
            float c = (column + 1) / 2 - 5;
            if ((row + 1) % 2 == 1)
            {
                r += 0.5f;
            }
            if ((column + 1) % 2 == 1)
            {
                c += 0.5f;
            }
            _cameraTrm.position = new Vector3(c, r);
            CameraManager.Instance.MoveCamera(_cameraTrm);
            _isCameraMoved = true;
            isChanging = false;
        }

        if (tileBase == null)
        {
            tilemap.SetTile(vec, tileBase);
            if (grid[PlayerMovement.Instance.posX, PlayerMovement.Instance.posY] == vec)
            {
                Player.Instance.Hit(999);
            }
        }
        else
            tilemap.SetTile(vec, this.tileBase);
    }
}
