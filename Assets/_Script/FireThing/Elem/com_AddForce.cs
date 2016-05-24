using UnityEngine;
using System.Collections;

public class com_AddForce : MonoBehaviour {
    public enum AddDir
    {
        Up,
        Down,
        Left,
        Right,
    }
    [System.Serializable]
    public class AddForceData
    {
        public AddDir _eDir;
        public float _fForce;
    }

    public AddForceData[] _addForces;
    public bool _addOnStart = false;
    ArrayList _addForceList2;

    Rigidbody rigid = null;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.Log("Warning: Obj Not Have Rigidbody !!!!!!!!!!!!!!!!!!!!!!");
            return;
        }
        if (_addOnStart)
        {
            for (int i = 0; i < _addForces.Length; ++i)
            {
                AddForceByData(_addForces[i]);
            }
        }
    }

    public void AddForceByData(AddDir eDir, float fForce)
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
            if (rigid == null)
            {
                return;
            }
        }
        switch (eDir)
        {
            case AddDir.Down:
                rigid.AddForce(Vector3.down * fForce);
                break;
            case AddDir.Left:
                rigid.AddForce(Vector3.left * fForce);
                break;
            case AddDir.Right:
                rigid.AddForce(Vector3.right * fForce);
                break;
            case AddDir.Up:
                rigid.AddForce(Vector3.up * fForce);
                break;
        }
    }

    void AddForceByData(AddForceData data)
    {
        AddForceByData(data._eDir, data._fForce);
    }
	
}
