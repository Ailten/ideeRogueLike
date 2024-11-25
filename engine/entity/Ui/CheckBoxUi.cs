
public class CheckBoxUi : LittleButtonUi
{

    private bool _isOn = false;
    public bool isOn
    {
        get { return _isOn; }
    }


    public CheckBoxUi(int idLayer) : base(idLayer)
    {
        updateStateIsOn(); //update state to false.
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if(getBaseType() == SpriteType.ButtonUi_Disabled)
            return;

        if(isLeftClick && !isClickDown)
            switchIsOn(); //switch state checkbox.
            
        base.eventMouseClick(isLeftClick, isClickDown);
    }


    //switch state is on of checkbox.
    public void switchIsOn()
    {
        _isOn = !isOn;
        updateStateIsOn();
    }

    //set manualy the bool isOn.
    public void setIsOn(bool isOn)
    {
        _isOn = isOn;
        updateStateIsOn();
    }

    //update the state of checkbox, depend of isOn.
    private void updateStateIsOn()
    {
        if(isOn){
            colorText = Raylib_cs.Color.Green;
            text = "V";
        }else{
            colorText = Raylib_cs.Color.Red;
            text = "X";
        }
    }

}