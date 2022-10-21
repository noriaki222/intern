using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaussianBlur : MonoBehaviour
{
    // �k���{��
    public enum DimScale
    {
        /// <summary>���{</summary>
        x1 = 1,
        /// <summary>2����1</summary>
        x2 = 2,
        /// <summary>4����1</summary>
        x4 = 4,
        /// <summary>8����1</summary>
        x8 = 8,
        /// <summary>16����1</summary>
        x16 = 16,
    }

    private const int BLUR_MIN = 1;
    private const int BLUR_MAX = 30;

    [SerializeField] private RenderTexture _cameraTexture = null;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Material _material2;
    [SerializeField] private Material _material3;

    // RenderTexture�̏k���{���A�傫�������̕����ᕉ��
    [SerializeField] DimScale _horizaontalScale = DimScale.x4;
    [SerializeField] DimScale _verticalScale = DimScale.x4;
    // �`����X�L�b�v����t���[����
    // e.g. 2�̏ꍇ�A2�t���[����1�񂾂��`�悷��
    [SerializeField, Range(1, 10)] uint _drawSkipFrame = 2;
    // �u���[�̋���
    [SerializeField, Range(BLUR_MIN, BLUR_MAX)] uint _blurStrength = 1;

    // ���ԃo�b�t�@�[
    private RenderTexture _rt2;
    private RenderTexture _rt3;
    private RenderTexture _rt4;
    // �O���RawImage�̑傫��
    Rect _previousRect;
    // �t���[���X�L�b�v��
    int _skipFrameCount = 9999;

    public uint BlurStrength
    {
        get => _blurStrength;
        set
        {
            if (value < BLUR_MIN)
            {
                value = BLUR_MIN;
            }
            else if (value > BLUR_MAX)
            {
                value = BLUR_MAX;
            }

            if (_material2 != null)
            {
                _material2.SetFloat("_Blur", value);
            }
            if (_material3 != null)
            {
                _material3.SetFloat("_Blur", value);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        BlurStrength = _blurStrength;
    }
#endif

    public void OnDestroy()
    {
        if (_rt2) RenderTexture.ReleaseTemporary(_rt2);
        if (_rt3) RenderTexture.ReleaseTemporary(_rt3);
        if (_rt4) RenderTexture.ReleaseTemporary(_rt4);
    }

    private void Awake()
    {
        _rawImage.color = Color.white;
        BlurStrength = _blurStrength;
    }

    public void Update()
    {
        Draw();
    }

    // �u���[��������̈�̑傫�����ύX���ꂽ�Ƃ� or ����ɍ�Ɨ̈���쐬����
    private void SetupRenderTexture(ref Rect currentRect)
    {
        currentRect.width = Mathf.Round(currentRect.width);
        currentRect.height = Mathf.Round(currentRect.height);
        if (currentRect.width != _previousRect.width ||
            currentRect.height != _previousRect.height)
        {
            if (_rt2) RenderTexture.ReleaseTemporary(_rt2);
            if (_rt3) RenderTexture.ReleaseTemporary(_rt3);
            if (_rt4) RenderTexture.ReleaseTemporary(_rt4);

            float w = Screen.width;
            float h = Screen.height;
            _rt2 = RenderTexture.GetTemporary(
                (int)currentRect.width, (int)currentRect.height);
            _rt3 = RenderTexture.GetTemporary(
                (int)currentRect.width / (int)_horizaontalScale,
                (int)currentRect.height / (int)_horizaontalScale);

            _rt4 = RenderTexture.GetTemporary(
                (int)currentRect.width / (int)_verticalScale,
                (int)currentRect.height / (int)_verticalScale);

            _previousRect = currentRect;

            Debug.Log("Create RenderTexture.");
        }
    }

    // �̈�Ƀu���[��������
    public void Draw()
    {
        if (_skipFrameCount <= _drawSkipFrame) // �t���[���X�L�b�v
        {
            _skipFrameCount++;
            return;
        }
        _skipFrameCount = 0;

        Rect r = _rawImage.GetScreenRect();
        SetupRenderTexture(ref r);

        // ��ʂ��ʂ����
        //Graphics.Blit(_cameratexture, _rt1, _material1);

        // RawImage �Ɠ����ʒu�ƃT�C�Y��؂蔲��
        Graphics.CopyTexture(
            _cameraTexture, 0, 0, (int)r.x, (int)r.y, (int)r.width, (int)r.height, _rt2, 0, 0, 0, 0);

        Graphics.Blit(_rt3, _rt4, _material3); // �c����
        Graphics.Blit(_rt2, _rt3, _material2); // ������

        // �؂��������̂�UI�ɒ���t��
        _rawImage.texture = _rt4;
    }
}

public static class GraphicExtension
{
    public static Rect GetScreenRect(this Graphic self)
    {
        var _corners = new Vector3[4];
        self.rectTransform.GetWorldCorners(_corners);

        if (self.canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            var cam = self.canvas.worldCamera;
            _corners[0] = RectTransformUtility.WorldToScreenPoint(cam, _corners[0]);
            _corners[2] = RectTransformUtility.WorldToScreenPoint(cam, _corners[2]);
        }

        return new Rect(_corners[0].x,
                        _corners[0].y,
                        _corners[2].x - _corners[0].x,
                        _corners[2].y - _corners[0].y);
    }
}
