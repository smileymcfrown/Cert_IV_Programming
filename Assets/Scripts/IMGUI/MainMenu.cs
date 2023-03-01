using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
	//screen Scale will hold our x and y float values
	public Vector2 screenScale;
	//this is a true or false toggle allowing us to show/hide the grid
	public bool toggleGrid,toggleLabel;
	public bool showMenuPanel, showOptionsPanel;
	[Space(10), Header("Slider Values"),Range(0,1)]
	public AudioSource audioSource;
	public Light sunLight;
	[Range(0, 1)]
	public bool toggleFullScreen;

	private void Start()
	{
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		sunLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
		if (audioSource == null)
		{
			Debug.Log("Nope!");
		}
	}
    #region OnGUI - Render IMGUI code and Events
    void OnGUI()
    {
		//Create a ratio of 1:1 unit that will be used on our 16:9 screen
        screenScale.x = Screen.width / 16;
        screenScale.y = Screen.height / 9;
		//Grid to display 16:9 aspect ratio
		Grid();
		Menu();
		Options();
    }
    #endregion
    #region Grid Reference
    void Grid()
	{
		//if the toggleGrid is set to true
		if(toggleGrid)
		{
			for(int x = 0; x < 16; x++)
			{
				for(int y = 0; y < 9; y++)
				{
					GUI.Box(new Rect(x*screenScale.x,y * screenScale.y,screenScale.x,screenScale.y),"");
					if(toggleLabel)
					{
						GUI.Label(new Rect(x*screenScale.x,y * screenScale.y,screenScale.x,screenScale.y),x+":"+y);	
					}
				}
			}
		}
	}
    #endregion
    #region Menu
    void Menu()
	{
		if(showMenuPanel)
		{
			//Panel
		   GUI.Box(new Rect(2f*screenScale.x,0.5f*screenScale.y,12f*screenScale.x,8f*screenScale.y),"");
		   //Title
		   GUI.Box(new Rect(4f*screenScale.x,1f*screenScale.y,8f*screenScale.x,2f*screenScale.y),"TITLE");
			//Play
			if(GUI.Button(new Rect(5f*screenScale.x,4.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Play"))
			{
				SceneManager.LoadScene(1);
			}
			//Options
			if(GUI.Button(new Rect(5f*screenScale.x,5.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Options"))
			{
				showMenuPanel = false;
			}
			//Exit
			if(GUI.Button(new Rect(5f*screenScale.x,6.5f*screenScale.y,6f*screenScale.x,0.75f*screenScale.y),"Exit"))
			{
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#endif
				Application.Quit();
			}
		}

	}
	void Options()
    {
        if (!showMenuPanel)
        {
			//Panel Background
			GUI.Box(new Rect(.2f* screenScale.x, 1.2f * screenScale.y, 15.55f * screenScale.x, 7.6f * screenScale.y), "");
			
			// Title - Box
			GUI.Box(new Rect(4f * screenScale.x, 0f * screenScale.y, 8f * screenScale.x, 1f * screenScale.y), "Title");
			
			// Audio Options
			GUI.Box(new Rect(0.5f * screenScale.x, 1.5f * screenScale.y, 2.5f * screenScale.x, 2f * screenScale.y), "Audio");
			// Music - Slider
				//GUI.Box(new Rect(0.75f * screenScale.x, 1.9f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "Music");
			audioSource.volume = GUI.HorizontalSlider(new Rect(.75f * screenScale.x,2.05f*screenScale.y,2f * screenScale.x, .5f * screenScale.y),audioSource.volume,0f,1f);
			// SFX - Slider
			GUI.Box(new Rect(0.75f * screenScale.x, 2.7f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "SFX");
			
			// Graphics Options
			GUI.Box(new Rect(0.5f * screenScale.x, 3.6f * screenScale.y, 2.5f * screenScale.x, 4f * screenScale.y), "Graphics");
			// Brightness - Slider
			//GUI.Box(new Rect(0.75f * screenScale.x, 4.4f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "Brightness");
			sunLight.intensity = GUI.HorizontalSlider(new Rect(.75f * screenScale.x, 4.4f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), sunLight.intensity, 0f, 1f);
			// Quality - Dropdown
			GUI.Box(new Rect(0.75f * screenScale.x, 5.2f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "Quality");
			// Resolutions - Dropdown
			GUI.Box(new Rect(0.75f * screenScale.x, 6f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "Resolution");
			// Fullscreen Toggle - Toggle
			GUI.Box(new Rect(0.75f * screenScale.x, 6.8f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y), "Fullscreen");
			Screen.fullScreen = GUI.Toggle(new Rect(0.75f * screenScale.x, 6.8f * screenScale.y, 2f * screenScale.x, .5f * screenScale.y),Screen.fullScreen,"Toggle");
			// Keybinds - Big
			GUI.Box(new Rect(3.5f * screenScale.x, 1.5f * screenScale.y, 12f * screenScale.x, 7f * screenScale.y), "Key Bindings");

			if (GUI.Button(new Rect(.5f * screenScale.x, 7.7f * screenScale.y, 2.5f * screenScale.x, .8f * screenScale.y), "Back"))
            {
				showMenuPanel = true;
            }

        }
    }
	#endregion
}

