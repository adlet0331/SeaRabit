using Cinemachine;
using NonDestroyObject;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalCutSceneObject : MonoBehaviour
{
    [SerializeField] private float minPressTime;
    [SerializeField] private Sprite[] Scenes;
    [SerializeField] private Transform pressSpaceIcon;
    private int currentIndex;
    private float afterSpacePressed;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        SoundManager.Instance.SwitchBGM(Bgm.bgm2);
        currentIndex = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        afterSpacePressed = 0.0f;
    }

    private void Update()
    {
        afterSpacePressed += Time.deltaTime;

        if (_spriteRenderer.color.a < minPressTime)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a + Time.deltaTime);
        }

        if (afterSpacePressed > minPressTime)
        {
            pressSpaceIcon.gameObject.SetActive(true);
        }
    }

    private void OnKeySpace(InputValue value)
    {
        if (afterSpacePressed < minPressTime) return;
        pressSpaceIcon.gameObject.SetActive(false);
        afterSpacePressed = 0.0f;
        currentIndex += 1;

        switch (currentIndex)
        {
            case 0:
                SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.Intro4CutManga1);
                break;
            case 1:
                SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.Intro4CutManga1);
                break;
            case 2:
                SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.Intro4CutManga1);
                break;
            case 3:
                SoundManager.Instance.GenerateAudioSourceAndPlay(transform, AudioClipEnum.Intro4CutManga1);
                break;
            case 4:
                Application.Quit();
                return;
        }
        _spriteRenderer.sprite = Scenes[currentIndex];
    }
}
