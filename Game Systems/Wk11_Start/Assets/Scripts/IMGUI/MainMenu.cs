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
	public AudioSource audioSource;
	public Light sunLight;
	public bool tempBool;
	void Start()
	{
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		sunLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
	}
	#region OnGUI - Renderer for IMGUI	
	//OnGUI - Render IMGUI code and Events
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
				showOptionsPanel = true;
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
	#endregion
	#region Options
	void Options()
	{
		if(	showOptionsPanel)
		{
			//Panel Background
			GUI.Box(new Rect(screenScale.x,screenScale.y,screenScale.x*14,screenScale.y*7),"");
			//Title - Box
			//GUI.Box(new Rect(screenScale.x,screenScale.y,screenScale.x,screenScale.y),"Options");
			//Audio - sliders
			GUI.Box(new Rect(screenScale.x,screenScale.y,screenScale.x*3,screenScale.y*1.5f),"Audio");
			audioSource.volume = GUI.HorizontalSlider(new Rect(1.25f*screenScale.x,1.5f*screenScale.y,2.5f*screenScale.x,0.25f*screenScale.y),audioSource.volume,0,1);
			//Brightness - sliders
			GUI.Box(new Rect(screenScale.x,screenScale.y*2.5f,screenScale.x*3,screenScale.y*1.5f),"Brightness");
			sunLight.intensity = GUI.HorizontalSlider(new Rect(1.25f*screenScale.x,3f*screenScale.y,2.5f*screenScale.x,0.25f*screenScale.y),sunLight.intensity,0,1);
			//Quality - dropdown
			GUI.Box(new Rect(screenScale.x,screenScale.y*4,screenScale.x*3,screenScale.y*1.5f),"Quality");
			//Resolutions - dropdown
			GUI.Box(new Rect(screenScale.x,screenScale.y*5.5f,screenScale.x*3f,screenScale.y*1.5f),"Resolution");
			//Fullscreen Toggle - toggle
			GUI.Box(new Rect(screenScale.x,screenScale.y*7,screenScale.x*3,screenScale.y*.5f),"Fullscreen");
			//Screen.fullScreen =  GUI.Toggle(new Rect(screenScale.x,screenScale.y*7.5f,screenScale.x*1,screenScale.y*.25f),Screen.fullScreen,"");//https://docs.unity3d.com/Manual/gui-Controls.html
			tempBool =  GUI.Toggle(new Rect(screenScale.x,screenScale.y*7.25f,screenScale.x*3,screenScale.y*.25f),tempBool,"Full Screen Toggle");
			//Keybinds - big
			GUI.Box(new Rect(4*screenScale.x,screenScale.y,screenScale.x*11,screenScale.y*7),"Keybinds");
			//Back Button
			if(GUI.Button(new Rect(screenScale.x,screenScale.y*7.5f,screenScale.x*3,screenScale.y*0.5f),"Back"))
			{
				//to go back
				showMenuPanel = true;
				showOptionsPanel = false;
			}
		}
	}
	#endregion
}
