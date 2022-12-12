using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WakuGraphic : Graphic
{
    [Serializable]
    private struct VertexPair
    {
        public Vector2 outer;
        public Vector2 inner;
    }

    public Sprite sprite;

    [SerializeField]
    private bool _isClosed = false;

    [SerializeField]
    private float _antialiasWidth = 1f;

    [SerializeField]
    private VertexPair[] _vertices;

    public override Texture mainTexture
    {
        get
        {
            if (sprite != null)
            {
                return sprite.texture;
            }
            else
            {
                return base.mainTexture;
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (_vertices.Length < 3)
        {
            return;
        }

        var uv = new Vector2();
        var dstSizeDelta = rectTransform.sizeDelta;

        if (sprite != null)
        {
            float offsetX = 0f;
            float offsetY = 0f;
            if (sprite.packed)
            {
                offsetX = sprite.textureRect.x;
                offsetY = sprite.textureRect.y;
            }

            // 適当な座標を設定しておく
            uv.x = (offsetX + sprite.rect.width / 2f) / sprite.texture.width;
            uv.y = (offsetY + sprite.rect.height / 2f) / sprite.texture.height;
        }

        var outerColor = color;
        outerColor.a = 0f;

        // 外積を使って頂点が時計回りに並んでいるのか反時計回りに並んでいるのか判定する
        bool clockwize = Cross(_vertices[1].outer - _vertices[0].outer, _vertices[2].outer - _vertices[1].outer) > 0f;

        var pos = new Vector3();

        // 座標を計算する
        for (int i = 0; i < _vertices.Length; ++i)
        {
            int prevIndex = (i != 0 ? i - 1 : _vertices.Length - 1);
            int nextIndex = (i != _vertices.Length - 1 ? i + 1 : 0);

            var v = _vertices[i];
            var outward = CalcOutwardVector(
                clockwize,
                ref v.outer,
                ref _vertices[prevIndex].outer,
                ref _vertices[nextIndex].outer);
            pos = v.outer;
            pos.x *= dstSizeDelta.x;
            pos.y *= dstSizeDelta.y;
            outward.x += pos.x;
            outward.y += pos.y;
            vh.AddVert(pos, color, uv);
            vh.AddVert(outward, outerColor, uv);

            outward = CalcOutwardVector(
                clockwize,
                ref v.inner,
                ref _vertices[prevIndex].inner,
                ref _vertices[nextIndex].inner);
            pos = v.inner;
            pos.x *= dstSizeDelta.x;
            pos.y *= dstSizeDelta.y;
            outward.x += pos.x;
            outward.y += pos.y;
            vh.AddVert(pos, color, uv);
            vh.AddVert(outward, outerColor, uv);
        }

        // 枠の辺を描く
        for (int i = 0; i < _vertices.Length - 1; ++i)
        {
            vh.AddTriangle(i * 4, i * 4 + 2, (i + 1) * 4);
            vh.AddTriangle(i * 4 + 2, (i + 1) * 4, (i + 1) * 4 + 2);
            vh.AddTriangle(i * 4, i * 4 + 1, (i + 1) * 4);
            vh.AddTriangle(i * 4 + 1, (i + 1) * 4, (i + 1) * 4 + 1);
            vh.AddTriangle(i * 4 + 2, i * 4 + 3, (i + 1) * 4 + 2);
            vh.AddTriangle(i * 4 + 3, (i + 1) * 4 + 2, (i + 1) * 4 + 3);
        }

        if (_isClosed)
        {
            var i = _vertices.Length - 1;
            vh.AddTriangle(0, 2, i * 4);
            vh.AddTriangle(2, i * 4 + 2, i * 4);
            vh.AddTriangle(0, i * 4, i * 4 + 1);
            vh.AddTriangle(0, 1, i * 4 + 1);
            vh.AddTriangle(2, i * 4 + 2, i * 4 + 3);
            vh.AddTriangle(2, 3, i * 4 + 3);
        }
    }

    private Vector2 CalcOutwardVector(bool clockwize, ref Vector2 refVert, ref Vector2 prevVert, ref Vector2 nextVert)
    {
        var v1 = refVert - prevVert;
        var v2 = nextVert - refVert;
        v1.Normalize();
        v2.Normalize();

        var v1_ = v1;
        Rotate(ref v1_);

        var v2_ = v2;
        Rotate(ref v2_);

        float a = Mathf.Sqrt(Vector2.SqrMagnitude(v2_ - v1_) / Vector2.SqrMagnitude(v2 + v1));

        if (!clockwize)
        {
            a = -a;
        }

        return _antialiasWidth * (v1_ - v1 * a);
    }

    // 反時計回りに90度に回す
    private void Rotate(ref Vector2 v)
    {
        float a = v.x;
        v.x = -v.y;
        v.y = a;
    }

    // いわゆる外積
    private float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }

    // ここから下が新しく増えた

#if UNITY_EDITOR
    [CustomEditor(typeof(WakuGraphic), true)]
    public class WakuGraphicInspector : Editor
    {
        private void OnSceneGUI()
        {
            Tools.current = Tool.None;

            var component = target as WakuGraphic;
            var vertices = component._vertices;
            if (vertices != null && vertices.Length >= 2)
            {
                var rectTransform = component.rectTransform;
                var sizeDelta = rectTransform.sizeDelta;
                var pivot = rectTransform.pivot;
                var mat = rectTransform.localToWorldMatrix;
                var inv = rectTransform.worldToLocalMatrix;

                for (int i = 0; i < vertices.Length; ++i)
                {
                    var v = vertices[i].outer;

                    // [0, 1]に正規化した座標からローカル座標を計算する
                    v.x *= sizeDelta.x;
                    v.y *= sizeDelta.y;

                    // ローカル座標からワールド座標に変換する
                    var currentPosition = mat.MultiplyPoint(v);
                    PositionHandle(ref vertices[i].outer, ref inv, ref currentPosition, ref sizeDelta);

                    v = vertices[i].inner;

                    // ローカル座標を計算する
                    v.x *= sizeDelta.x;
                    v.y *= sizeDelta.y;

                    // ローカル座標からワールド座標に変換する
                    currentPosition = mat.MultiplyPoint(v);
                    PositionHandle(ref vertices[i].inner, ref inv, ref currentPosition, ref sizeDelta);
                }

                component.SetVerticesDirty();
            }
        }

        void PositionHandle(ref Vector2 targetPoint, ref Matrix4x4 inverse, ref Vector3 position, ref Vector2 sizeDelta)
        {
            var handleSize = HandleUtility.GetHandleSize(position) * 0.2f;
            var newWorldPosition = Handles.FreeMoveHandle(position, Quaternion.identity, handleSize, new Vector3(1f, 1f, 0f), Handles.CircleHandleCap);

            // ワールド座標からローカル座標に戻す
            var newPosition = inverse.MultiplyPoint3x4(newWorldPosition);

            // [0, 1]に正規化した座標に戻す
            newPosition.x /= sizeDelta.x;
            newPosition.y /= sizeDelta.y;

            if (Mathf.Abs(newPosition.x - targetPoint.x) > 1e-5f)
            {
                targetPoint.x = newPosition.x;
            }
            if (Mathf.Abs(newPosition.y - targetPoint.y) > 1e-5f)
            {
                targetPoint.y = newPosition.y;
            }
        }
    }
#endif
}