using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public bool onEnter, onEnable;
    public int sfxToPlay;

    public bool playRandom;
    public int fromIndex, toIndex;

    private void Start()
    {
        if (onEnter)
        {
            if (playRandom)
            {
                PlaySfx(Random.Range(fromIndex, toIndex));
                return;
            }
            PlaySfx(sfxToPlay);
        }
    }
    private void OnEnable()
    {
        if (onEnable)
        {
            if (playRandom)
            {
                PlaySfx(Random.Range(fromIndex, toIndex));
                return;
            }
            PlaySfx(sfxToPlay);
        }
    }
    public void PlaySfx(int sfx)
    {
        AudioManager.Ins.PlaySFX(sfx);
    }
    public void StopSfx(int sfx)
    {
        AudioManager.Ins.StopSFX(sfx);
    }
}
