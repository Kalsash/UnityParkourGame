using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource musicSource;
    private HashSet<int> PlayedMusicSet = new HashSet<int>();
    private int LastPlayed = -1;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        Sounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying) 
        {
            musicSource.Stop();
            Sounds();
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonDown(2) || scroll != 0) 
        {
            musicSource.Stop();
            Sounds();
        }
    }
    void Sounds()
    {
        if (PlayedMusicSet.Count >= sounds.Length)
        {
            PlayedMusicSet.Clear();
        }
        var r = UnityEngine.Random.Range(0, sounds.Length);
        while (PlayedMusicSet.Contains(r) || LastPlayed == r) 
        {
            r = UnityEngine.Random.Range(0, sounds.Length);  
        }
        LastPlayed = r;
        PlayedMusicSet.Add(r);
        musicSource.PlayOneShot(sounds[r]);
    }
}
