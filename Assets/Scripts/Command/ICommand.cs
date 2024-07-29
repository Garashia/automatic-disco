using UnityEngine;

public struct Parameter
{
    // 位置
    public Vector3 firstPosition;
    public Vector3 lastPosition;
    public Vector3 direction;

    public Vector3 firstRectPosition;
    public Vector3 lastRectPosition;
    public Vector3 RectDirection;

    // 回転
    public Quaternion firstRotation;
    public Quaternion lastRotation;
    public Quaternion addedRotation;

    // 文章
    public string word;

    // 時間
    public float time;

    // サイズ
    public Vector3 firstScale;
    public Vector3 lastScale;
    public Vector3 addedScale;

    // 操作するオブジェクト
    public GameObject gameObject;
    public Parameter(
        GameObject go,
        Vector3 fp = new Vector3(),
        Vector3 lp = new Vector3(),
        Vector3 d = new Vector3(),
        Vector3 frp = new Vector3(),
        Vector3 lrp = new Vector3(),
        Vector3 rd = new Vector3(),
        Quaternion fr = new Quaternion(),
        Quaternion lr = new Quaternion(),
        Quaternion ad = new Quaternion(),
        string w = "",
        float t = new float(),
        Vector3 fs = new Vector3(),
        Vector3 ls = new Vector3(),
        Vector3 aScale = new Vector3()

        )
    {
        gameObject = go;
        firstPosition = fp;
        lastPosition = lp;
        direction = d;
        firstRectPosition = frp;
        lastRectPosition = lrp;
        RectDirection = rd;
        firstRotation = fr;
        lastRotation = lr;
        addedRotation = ad;
        addedScale = aScale;
        word = w;
        time = t;
        firstScale = fs;
        lastScale = ls;
    }
}



public interface ICommand
{
    // パラメータを取得する
    abstract public Parameter GetParameter();
    // パラメータを設定する
    abstract public void SetParameter(Parameter parameter);
    // 実行する
    abstract public void Execute();

    // abstract public IEnumerator Enumerator();

    abstract public bool Enable();

}
