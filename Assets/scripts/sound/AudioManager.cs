using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour {
    public int width;
    public int height;
    public Color backgroundColor = Color.black;
    public Color waveformColor = Color.white;
    public Color cursorColor = Color.red;
    public int size = 2048;

    Color[] blank;
    Texture2D texture;
    float[] samples;
    AudioSource audioSource;
    AudioClip clip;
    public GameObject image;
    private RawImage img;
	private string musicName;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        samples = new float[size];

        // create the texture and assign to the guiTexture:
        img = (RawImage) image.GetComponent<RawImage>();
        width = (int) GetComponent<RectTransform>().rect.width;
        height = (int)GetComponent<RectTransform>().rect.height;

        texture = new Texture2D(width, height);
        img.texture = texture;
       
        // create a 'blank screen' image
        blank = new Color[width * height];

        for (var i = 0; i < blank.Length; i++)
        {
            blank[i] = backgroundColor;
        }

    }

	public void setMusicName(string name){
		musicName = name;
	}

	public void play(){
		if (musicName != null) {
			clip = Resources.Load ("Musics/" + musicName, typeof(AudioClip)) as AudioClip;
			audioSource.clip = clip;

			// refresh the display each 100mS
			GetWaveForm ();
			audioSource.Play ();
			StartCoroutine (UpdateWaveForm ());
		}
	}

    IEnumerator UpdateWaveForm()
    {
        int sizeWaveform = audioSource.clip.samples;
        int secondes = sizeWaveform / audioSource.clip.frequency;
        int pas = width / secondes;
        int i,j;
        int count = 0;
        while (true)
        {
            for (i = 1; i < height; i++) //sizeWaveform
            {
				for (j = pas * (count-1); j <= pas * count; j++) //sizeWaveform
				{
                texture.SetPixel(j, i, cursorColor);
				}
            }
            texture.Apply();
            count = count + 1;
            //GetCurWave();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void GetWaveForm()
    {
        int sizeWaveform = audioSource.clip.samples;
        float[] samplesWaveForm = new float[sizeWaveform+1];
        //Debug.Log("sizeWaveForm:" + sizeWaveform);
        audioSource.clip.GetData(samplesWaveForm, sizeWaveform/4);

        //Debug.Log("fq:"+audioSource.clip.frequency+" / sp:"+ sizeWaveform + "="+ sizeWaveform / audioSource.clip.frequency+"s calculées au début");

        texture.SetPixels(blank, 0);
        int i;
        for (i=  1; i< sizeWaveform; i++) //sizeWaveform
        {
            //texture.SetPixel((width * i) / sizeWaveform, (int)(height * (samplesWaveForm[i] + 1f) / 2), waveformColor);
            texture.SetPixel((width * i)/i, (int)(height * (samplesWaveForm[i] + 1f) / 2), waveformColor);
        }
        texture.Apply();
    }

    void GetCurWave()
    {
        // clear the texture
        texture.SetPixels(blank, 0);

        // get samples from channel 0 (left)
        audioSource.GetOutputData(samples, 0);

        // draw the waveform
        for (var i = 0; i < size; i++)
        {
            texture.SetPixel((int)((width * i) / size), (int)(height * (samples[i] + 1f) / 2), waveformColor);
        }
        // upload to the graphics card
        texture.Apply();
    }
    // Update is called once per frame
    void Update () {
	
	}
}
