using UnityEngine;

public class JoystickButtonMenu{
	public enum JoyAxis{
		Horizontal = 0,
		Vertical = 1
	}
	
	public JoyAxis JoystickAxis = JoyAxis.Vertical;
	
	public string joystickInputNamePrefix = "", joystickInputName = "";
	public bool enabled = true;
	
	private int numberOfButtons;
	private JoystickButton[] buttons;
	public bool isCheckingJoy;
	public int currentFocus;
	private string actionButton;
	
	
	public JoystickButtonMenu(int numOfButtons,Rect[] rectangles,string[] labels, string inputActionButton,JoyAxis axis){
		if(axis == JoyAxis.Horizontal){
			joystickInputName = "Horizontal";
		}else if(axis == JoyAxis.Vertical){
			joystickInputName = "Vertical";
		}
		
		numberOfButtons = numOfButtons;
		actionButton = inputActionButton;
		
		buttons = new JoystickButton[numOfButtons];
		for (int i = 0; i<numOfButtons; i++){
			buttons[i] = new JoystickButton(rectangles[i],labels[i]);
		}
		
		buttons[0].Focus();
		currentFocus = 0;
	}
	
	public bool CheckJoystickAxis(){
		if(Mathf.Abs(Input.GetAxis(joystickInputNamePrefix+joystickInputName)) == 1 && !isCheckingJoy && enabled){
			if(Input.GetAxis(joystickInputNamePrefix+joystickInputName) > .1f){
				SetFocus(1);
			}
			if(Input.GetAxis(joystickInputNamePrefix+joystickInputName) < -.1f){
				SetFocus(-1);
			}
			isCheckingJoy = true;
			return true;
		}
		return false;
	}
	
	public int CheckJoystickButton(){
		int pressedButton = -1;
		if(enabled){
			if(Input.GetButtonDown(actionButton)){
				for(int i = 0; i<numberOfButtons; i++){
					if(buttons[i].Click()){
						pressedButton = i;
					}
				}
			}
			if(Input.GetButtonUp(actionButton)){
				foreach (JoystickButton butt in buttons){
					butt.UnClick();
				}
			}
		}
		return pressedButton;
	}
	
	public void SetFocus(int change){
		if(enabled){
			if(change == -1){
				currentFocus ++;
				if(currentFocus == numberOfButtons){
					currentFocus = 0;
				}
			}else if(change == 1){
				currentFocus --;
				if(currentFocus == -1){
					currentFocus = numberOfButtons-1;
				}
			}
			
			for(int i = 0;i<numberOfButtons;i++){
				buttons[i].Focus(false);
				if(currentFocus == i){
					buttons[i].Focus(true);
				}
			}
		}
	}
	
	public void DisplayButtons(){
		if(enabled){
			foreach(JoystickButton butt in buttons){
				butt.Display();
			}
		}
	}
}