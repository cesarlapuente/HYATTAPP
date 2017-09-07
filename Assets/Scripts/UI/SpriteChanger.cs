using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use this to easily switch between sprites if an Image component is attached to this object
/// </summary>
public class SpriteChanger : MonoBehaviour
{
    private Image _imageComponent;
    public Sprite[] _sprites;
    public bool _spriteEnabled = true;

    private void Awake()
    {
        _imageComponent = transform.GetComponent<Image>();
        if (_imageComponent == null)
        {
            Debug.LogError("There is no image component attached to this GameObject");
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
            _imageComponent.color = new Color(_imageComponent.color.r, _imageComponent.color.g, _imageComponent.color.b, 1.0f);
        }

        if (spriteIndex < _sprites.Length)
        {
            _imageComponent.sprite = _sprites[spriteIndex];
        }
        else
        {
            Debug.LogWarning("Incorrect spriteIndex, the sprite was not changed");
        }
    }

    public void DisableSprite()
    {
        _spriteEnabled = false;
        _imageComponent.sprite = null;
        _imageComponent.color = new Color(_imageComponent.color.r, _imageComponent.color.g, _imageComponent.color.b, 0.0f);
    }
}