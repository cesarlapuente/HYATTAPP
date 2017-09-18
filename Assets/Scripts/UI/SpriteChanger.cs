using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use this to easily switch between sprites if an Image component is attached to this object
/// </summary>
public class SpriteChanger : MonoBehaviour
{
    private Image _imageComponent;
    private SpriteRenderer _spriteRenderer;
    public Sprite[] _sprites;
    public bool _spriteEnabled = true;

    private void Awake()
    {
        _imageComponent = transform.GetComponent<Image>();
        if (_imageComponent == null)
        {
            _spriteRenderer = transform.GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                Debug.LogError("There is no Image or SpriteRenderer component attached to this GameObject");
            }
        }
        if (!_spriteEnabled)
        {
            DisableSprite();
        }
    }

    public void ChangeSprite(uint spriteIndex)
    {
        if (!_spriteEnabled)
        {
            _spriteEnabled = true;
            if (_imageComponent != null)
            {
                _imageComponent.color = new Color(_imageComponent.color.r, _imageComponent.color.g, _imageComponent.color.b, 1.0f);
            }
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1.0f);
            }
        }

        if (spriteIndex < _sprites.Length)
        {
            if (_imageComponent != null)
            {
                _imageComponent.sprite = _sprites[spriteIndex];
            }
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _sprites[spriteIndex];
            }
        }
        else
        {
            Debug.LogWarning("Incorrect spriteIndex, the sprite was not changed");
        }
    }

    public void DisableSprite()
    {
        _spriteEnabled = false;

        if (_imageComponent != null)
        {
            _imageComponent.sprite = null;
            _imageComponent.color = new Color(_imageComponent.color.r, _imageComponent.color.g, _imageComponent.color.b, 0.0f);
        }
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = null;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.0f);
        }

    }
}