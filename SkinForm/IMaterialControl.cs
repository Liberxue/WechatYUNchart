namespace YUNkefu
{
    interface IShisan13Control
    {
        int Depth { get; set; }
        shisan13Manager SkinManager { get; }
        MouseState MouseState { get; set; }

    }

    public enum MouseState
    {
        HOVER,
        DOWN,
        OUT
    }
}
