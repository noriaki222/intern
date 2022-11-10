using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

public class DynamickMask : Graphic
{
    public Vector2 vert1;
    public Vector2 vert2;
    public Vector2 vert3;
    public Vector2 vert4;

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);

        toFill.Clear();

        // ���_����n��
        toFill.AddVert(vert1, color, new Vector2(0.0f, 0.0f));
        toFill.AddVert(vert2, color, new Vector2(0.0f, 1.0f));
        toFill.AddVert(vert3, color, new Vector2(1.0f, 1.0f));
        toFill.AddVert(vert4, color, new Vector2(1.0f, 0.0f));

        // ���_�̏���
        toFill.AddTriangle(0, 1, 2);
        toFill.AddTriangle(2, 3, 0);
    }

#if UNITY_EDITOR
    [ContextMenu("SetVerticesDirty")]
    private void MenuSetVerticesDirty()
    {
        SetVerticesDirty();
    }

#endif

    // �A�j���[�V�����ɂ��ύX���s��ꂽ�Ƃ��̃R�[���o�b�N
    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();

        SetVerticesDirty();
    }
}
