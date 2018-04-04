using System;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Input.Controls;
using UnityEngine.Experimental.Input.LowLevel;
using UnityEngine.Experimental.Input.Utilities;
using Unity.Collections.LowLevel.Unsafe;

////TODO: IME support

namespace UnityEngine.Experimental.Input.LowLevel
{
    /// <summary>
    /// Default state layout for keyboards.
    /// </summary>
    // NOTE: This layout has to match the KeyboardInputState layout used in native!
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct KeyboardState : IInputStateTypeInfo
    {
        public static FourCC kFormat
        {
            get { return new FourCC('K', 'E', 'Y', 'S'); }
        }

        // Number of keys rounded up to nearest size of 4.
        private const int kSizeInBytesUnrounded = ((int)Key.Count) / 8 + (((int)Key.Count) % 8 > 0 ? 1 : 0);
        public const int kSizeInBytes = kSizeInBytesUnrounded + (4 - kSizeInBytesUnrounded % 4);
        public const int kSizeInBits = kSizeInBytes * 8;

        [InputControl(name = "AnyKey", layout = "AnyKey", sizeInBits = kSizeInBits)]
        [InputControl(name = "Escape", layout = "Key", usages = new[] {"Back", "Cancel"}, bit = (int)Key.Escape)]
        [InputControl(name = "Space", layout = "Key", bit = (int)Key.Space)]
        [InputControl(name = "Enter", layout = "Key", usage = "Accept", bit = (int)Key.Enter)]
        [InputControl(name = "Tab", layout = "Key", bit = (int)Key.Tab)]
        [InputControl(name = "Backquote", layout = "Key", bit = (int)Key.Backquote)]
        [InputControl(name = "Quote", layout = "Key", bit = (int)Key.Quote)]
        [InputControl(name = "Semicolon", layout = "Key", bit = (int)Key.Semicolon)]
        [InputControl(name = "Comma", layout = "Key", bit = (int)Key.Comma)]
        [InputControl(name = "Period", layout = "Key", bit = (int)Key.Period)]
        [InputControl(name = "Slash", layout = "Key", bit = (int)Key.Slash)]
        [InputControl(name = "Backslash", layout = "Key", bit = (int)Key.Backslash)]
        [InputControl(name = "LeftBracket", layout = "Key", bit = (int)Key.LeftBracket)]
        [InputControl(name = "RightBracket", layout = "Key", bit = (int)Key.RightBracket)]
        [InputControl(name = "Minus", layout = "Key", bit = (int)Key.Minus)]
        [InputControl(name = "Equals", layout = "Key", bit = (int)Key.Equals)]
        [InputControl(name = "UpArrow", layout = "Key", bit = (int)Key.UpArrow)]
        [InputControl(name = "DownArrow", layout = "Key", bit = (int)Key.DownArrow)]
        [InputControl(name = "LeftArrow", layout = "Key", bit = (int)Key.LeftArrow)]
        [InputControl(name = "RightArrow", layout = "Key", bit = (int)Key.RightArrow)]
        [InputControl(name = "A", layout = "Key", bit = (int)Key.A)]
        [InputControl(name = "B", layout = "Key", bit = (int)Key.B)]
        [InputControl(name = "C", layout = "Key", bit = (int)Key.C)]
        [InputControl(name = "D", layout = "Key", bit = (int)Key.D)]
        [InputControl(name = "E", layout = "Key", bit = (int)Key.E)]
        [InputControl(name = "F", layout = "Key", bit = (int)Key.F)]
        [InputControl(name = "G", layout = "Key", bit = (int)Key.G)]
        [InputControl(name = "H", layout = "Key", bit = (int)Key.H)]
        [InputControl(name = "I", layout = "Key", bit = (int)Key.I)]
        [InputControl(name = "J", layout = "Key", bit = (int)Key.J)]
        [InputControl(name = "K", layout = "Key", bit = (int)Key.K)]
        [InputControl(name = "L", layout = "Key", bit = (int)Key.L)]
        [InputControl(name = "M", layout = "Key", bit = (int)Key.M)]
        [InputControl(name = "N", layout = "Key", bit = (int)Key.N)]
        [InputControl(name = "O", layout = "Key", bit = (int)Key.O)]
        [InputControl(name = "P", layout = "Key", bit = (int)Key.P)]
        [InputControl(name = "Q", layout = "Key", bit = (int)Key.Q)]
        [InputControl(name = "R", layout = "Key", bit = (int)Key.R)]
        [InputControl(name = "S", layout = "Key", bit = (int)Key.S)]
        [InputControl(name = "T", layout = "Key", bit = (int)Key.T)]
        [InputControl(name = "U", layout = "Key", bit = (int)Key.U)]
        [InputControl(name = "V", layout = "Key", bit = (int)Key.V)]
        [InputControl(name = "W", layout = "Key", bit = (int)Key.W)]
        [InputControl(name = "X", layout = "Key", bit = (int)Key.X)]
        [InputControl(name = "Y", layout = "Key", bit = (int)Key.Y)]
        [InputControl(name = "Z", layout = "Key", bit = (int)Key.Z)]
        [InputControl(name = "1", layout = "Key", bit = (int)Key.Digit1)]
        [InputControl(name = "2", layout = "Key", bit = (int)Key.Digit2)]
        [InputControl(name = "3", layout = "Key", bit = (int)Key.Digit3)]
        [InputControl(name = "4", layout = "Key", bit = (int)Key.Digit4)]
        [InputControl(name = "5", layout = "Key", bit = (int)Key.Digit5)]
        [InputControl(name = "6", layout = "Key", bit = (int)Key.Digit6)]
        [InputControl(name = "7", layout = "Key", bit = (int)Key.Digit7)]
        [InputControl(name = "8", layout = "Key", bit = (int)Key.Digit8)]
        [InputControl(name = "9", layout = "Key", bit = (int)Key.Digit9)]
        [InputControl(name = "0", layout = "Key", bit = (int)Key.Digit0)]
        [InputControl(name = "LeftShift", layout = "Key", usage = "Modifier", bit = (int)Key.LeftShift)]
        [InputControl(name = "RightShift", layout = "Key", usage = "Modifier", bit = (int)Key.RightShift)]
        [InputControl(name = "LeftAlt", layout = "Key", usage = "Modifier", bit = (int)Key.LeftAlt)]
        [InputControl(name = "RightAlt", layout = "Key", usage = "Modifier", bit = (int)Key.RightAlt, alias = "AltGr")]
        [InputControl(name = "LeftCtrl", layout = "Key", usage = "Modifier", bit = (int)Key.LeftCtrl)]
        [InputControl(name = "RightCtrl", layout = "Key", usage = "Modifier", bit = (int)Key.RightCtrl)]
        [InputControl(name = "LeftMeta", layout = "Key", usage = "Modifier", bit = (int)Key.LeftMeta, aliases = new[] { "LeftWindows", "LeftApple", "LeftCommand" })]
        [InputControl(name = "RightMeta", layout = "Key", usage = "Modifier", bit = (int)Key.RightMeta, aliases = new[] { "RightWindows", "RightApple", "RightCommand" })]
        [InputControl(name = "ContextMenu", layout = "Key", usage = "Modifier", bit = (int)Key.ContextMenu)]
        [InputControl(name = "Backspace", layout = "Key", bit = (int)Key.Backspace)]
        [InputControl(name = "PageDown", layout = "Key", bit = (int)Key.PageDown)]
        [InputControl(name = "PageUp", layout = "Key", bit = (int)Key.PageUp)]
        [InputControl(name = "Home", layout = "Key", bit = (int)Key.Home)]
        [InputControl(name = "End", layout = "Key", bit = (int)Key.End)]
        [InputControl(name = "Insert", layout = "Key", bit = (int)Key.Insert)]
        [InputControl(name = "Delete", layout = "Key", bit = (int)Key.Delete)]
        [InputControl(name = "CapsLock", layout = "Key", bit = (int)Key.CapsLock)]
        [InputControl(name = "NumLock", layout = "Key", bit = (int)Key.NumLock)]
        [InputControl(name = "PrintScreen", layout = "Key", bit = (int)Key.PrintScreen)]
        [InputControl(name = "ScrollLock", layout = "Key", bit = (int)Key.ScrollLock)]
        [InputControl(name = "Pause", layout = "Key", bit = (int)Key.Pause)]
        [InputControl(name = "NumpadEnter", layout = "Key", bit = (int)Key.NumpadEnter)]
        [InputControl(name = "NumpadDivide", layout = "Key", bit = (int)Key.NumpadDivide)]
        [InputControl(name = "NumpadMultiply", layout = "Key", bit = (int)Key.NumpadMultiply)]
        [InputControl(name = "NumpadPlus", layout = "Key", bit = (int)Key.NumpadPlus)]
        [InputControl(name = "NumpadMinus", layout = "Key", bit = (int)Key.NumpadMinus)]
        [InputControl(name = "NumpadPeriod", layout = "Key", bit = (int)Key.NumpadPeriod)]
        [InputControl(name = "NumpadEquals", layout = "Key", bit = (int)Key.NumpadEquals)]
        [InputControl(name = "Numpad1", layout = "Key", bit = (int)Key.Numpad1)]
        [InputControl(name = "Numpad2", layout = "Key", bit = (int)Key.Numpad2)]
        [InputControl(name = "Numpad3", layout = "Key", bit = (int)Key.Numpad3)]
        [InputControl(name = "Numpad4", layout = "Key", bit = (int)Key.Numpad4)]
        [InputControl(name = "Numpad5", layout = "Key", bit = (int)Key.Numpad5)]
        [InputControl(name = "Numpad6", layout = "Key", bit = (int)Key.Numpad6)]
        [InputControl(name = "Numpad7", layout = "Key", bit = (int)Key.Numpad7)]
        [InputControl(name = "Numpad8", layout = "Key", bit = (int)Key.Numpad8)]
        [InputControl(name = "Numpad9", layout = "Key", bit = (int)Key.Numpad9)]
        [InputControl(name = "Numpad0", layout = "Key", bit = (int)Key.Numpad0)]
        [InputControl(name = "F1", layout = "Key", bit = (int)Key.F1)]
        [InputControl(name = "F2", layout = "Key", bit = (int)Key.F2)]
        [InputControl(name = "F3", layout = "Key", bit = (int)Key.F3)]
        [InputControl(name = "F4", layout = "Key", bit = (int)Key.F4)]
        [InputControl(name = "F5", layout = "Key", bit = (int)Key.F5)]
        [InputControl(name = "F6", layout = "Key", bit = (int)Key.F6)]
        [InputControl(name = "F7", layout = "Key", bit = (int)Key.F7)]
        [InputControl(name = "F8", layout = "Key", bit = (int)Key.F8)]
        [InputControl(name = "F9", layout = "Key", bit = (int)Key.F9)]
        [InputControl(name = "F10", layout = "Key", bit = (int)Key.F10)]
        [InputControl(name = "F11", layout = "Key", bit = (int)Key.F11)]
        [InputControl(name = "F12", layout = "Key", bit = (int)Key.F12)]
        [InputControl(name = "OEM1", layout = "Key", bit = (int)Key.OEM1)]
        [InputControl(name = "OEM2", layout = "Key", bit = (int)Key.OEM2)]
        [InputControl(name = "OEM3", layout = "Key", bit = (int)Key.OEM3)]
        [InputControl(name = "OEM4", layout = "Key", bit = (int)Key.OEM4)]
        [InputControl(name = "OEM5", layout = "Key", bit = (int)Key.OEM5)]
        public fixed byte keys[kSizeInBytes];

        public KeyboardState(params Key[] pressedKeys)
        {
            fixed(byte* keysPtr = keys)
            {
                UnsafeUtility.MemClear(keysPtr, kSizeInBytes);
                for (var i = 0; i < pressedKeys.Length; ++i)
                {
                    MemoryHelpers.WriteSingleBit(new IntPtr(keysPtr), (uint)pressedKeys[i], true);
                }
            }
        }

        public FourCC GetFormat()
        {
            return kFormat;
        }
    }
}

namespace UnityEngine.Experimental.Input
{
    /// <summary>
    /// Enumeration of key codes.
    /// </summary>
    /// <remarks>
    /// Named according to the US keyboard layout which is our reference layout.
    /// </remarks>
    // NOTE: Has to match up with 'KeyboardInputState::KeyCode' in native.
    // NOTE: In the keyboard code, we depend on the order of the keys in the various keyboard blocks.
    public enum Key
    {
        None,

        // Printable keys.
        Space,
        Enter,
        Tab,
        Backquote,
        Quote,
        Semicolon,
        Comma,
        Period,
        Slash,
        Backslash,
        LeftBracket,
        RightBracket,
        Minus,
        Equals,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        Digit1,
        Digit2,
        Digit3,
        Digit4,
        Digit5,
        Digit6,
        Digit7,
        Digit8,
        Digit9,
        Digit0,

        // Non-printable keys.
        LeftShift,
        RightShift,
        LeftAlt,
        RightAlt,
        AltGr = RightAlt,
        LeftCtrl,
        RightCtrl,
        LeftMeta,
        RightMeta,
        LeftWindows = LeftMeta,
        RightWindows = RightMeta,
        LeftApple = LeftMeta,
        RightApple = RightMeta,
        LeftCommand = LeftMeta,
        RightCommand = RightMeta,
        ContextMenu,
        Escape,
        LeftArrow,
        RightArrow,
        UpArrow,
        DownArrow,
        Backspace,
        PageDown,
        PageUp,
        Home,
        End,
        Insert,
        Delete,
        CapsLock,
        NumLock,
        PrintScreen,
        ScrollLock,
        Pause,

        // Numpad.
        // NOTE: Numpad layout follows the 18-key numpad layout. Some PC keyboards
        //       have a 17-key numpad layout where the plus key is an elongated key
        //       like the numpad enter key. Be aware that in those layouts the positions
        //       of some of the operator keys are also different. However, we stay
        //       layout neutral here, too, and always use the 18-key blueprint.
        NumpadEnter,
        NumpadDivide,
        NumpadMultiply,
        NumpadPlus,
        NumpadMinus,
        NumpadPeriod,
        NumpadEquals,
        Numpad0,
        Numpad1,
        Numpad2,
        Numpad3,
        Numpad4,
        Numpad5,
        Numpad6,
        Numpad7,
        Numpad8,
        Numpad9,

        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,

        // Extra keys that a keyboard may have. We make no guarantees about where
        // they end up on the keyboard (if they are present).
        OEM1,
        OEM2,
        OEM3,
        OEM4,
        OEM5,

        Count
    }

    /// <summary>
    /// A keyboard input device.
    /// </summary>
    /// <remarks>
    /// Keyboards allow for both individual button input as well as text input.
    ///
    /// Be aware that identification of keys in the system is layout-agnostic. For example, the
    /// key referred to as the "a" key is always the key to the right of Caps Lock regardless of
    /// where the current keyboard layout actually puts the "a" character (if it even has any).
    /// To find what is actually behind a key according to the current keyboard layout, use
    /// <see cref="InputControl.displayName"/>, <see cref="KeyControl.shiftDisplayName"/>, and
    /// <see cref="KeyControl.altDisplayName"/>.
    /// </remarks>
    [InputControlLayout(stateType = typeof(KeyboardState))]
    public class Keyboard : InputDevice
    {
        /// <summary>
        /// Event that is fired for every single character entered on the keyboard.
        /// </summary>
        public event Action<char> onTextInput
        {
            add { m_TextInputListeners.Append(value); }
            remove { m_TextInputListeners.Remove(value); }
        }

        /// <summary>
        /// The name of the layout currently used by the keyboard.
        /// </summary>
        /// <remarks>
        /// Note that keyboard layout names are platform-specific.
        ///
        /// The value of this property reflects the currently used layout and thus changes
        /// whenever the layout of the system or the one for the application is changed.
        ///
        /// To determine what a key represents in the current layout, use <see cref="InputControl.displayName"/>,
        /// <see cref="KeyControl.shiftDisplayName"/>, and <see cref="KeyControl.altDisplayName"/>.
        /// </remarks>
        public string keyboardLayout
        {
            get
            {
                RefreshConfigurationIfNeeded();
                return m_KeyboardLayoutName;
            }
            protected set { m_KeyboardLayoutName = value; }
        }

        /// <summary>
        /// A synthetic button control that is considered pressed if any key on the keyboard is pressed.
        /// </summary>
        public AnyKeyControl anyKey { get; private set; }

        /// <summary>
        /// The space bar key.
        /// </summary>
        public KeyControl spaceKey { get; private set; }

        /// <summary>
        /// The enter/return key in the main key block.
        /// </summary>
        /// <remarks>
        /// This key is distinct from the enter key on the numpad which is <see cref="numpadEnterKey"/>.
        /// </remarks>
        public KeyControl enterKey { get; private set; }

        /// <summary>
        /// The tab key.
        /// </summary>
        public KeyControl tabKey { get; private set; }

        /// <summary>
        /// The ` key. The leftmost key in the row of digits. Directly above tab.
        /// </summary>
        public KeyControl backquoteKey { get; private set; }

        /// <summary>
        /// The ' key. The key immediately to the left of the enter/return key.
        /// </summary>
        public KeyControl quoteKey { get; private set; }

        /// <summary>
        /// The ';' key. The key immediately to the left of the quote key.
        /// </summary>
        public KeyControl semicolonKey { get; private set; }

        /// <summary>
        /// The ',' key. Third key to the left of the right shift key.
        /// </summary>
        public KeyControl commaKey { get; private set; }
        public KeyControl periodKey { get; private set; }
        public KeyControl slashKey { get; private set; }
        public KeyControl backslashKey { get; private set; }
        public KeyControl leftBracketKey { get; private set; }
        public KeyControl rightBracketKey { get; private set; }
        public KeyControl minusKey { get; private set; }

        /// <summary>
        /// The '=' key in the main key block. Key immediately to the left of the backspace key.
        /// </summary>
        public KeyControl equalsKey { get; private set; }

        /// <summary>
        /// The 'a' key. Key immediately to the right of the caps lock key.
        /// </summary>
        public KeyControl aKey { get; private set; }
        public KeyControl bKey { get; private set; }
        public KeyControl cKey { get; private set; }
        public KeyControl dKey { get; private set; }
        public KeyControl eKey { get; private set; }
        public KeyControl fKey { get; private set; }
        public KeyControl gKey { get; private set; }
        public KeyControl hKey { get; private set; }
        public KeyControl iKey { get; private set; }
        public KeyControl jKey { get; private set; }
        public KeyControl kKey { get; private set; }
        public KeyControl lKey { get; private set; }
        public KeyControl mKey { get; private set; }
        public KeyControl nKey { get; private set; }
        public KeyControl oKey { get; private set; }
        public KeyControl pKey { get; private set; }
        public KeyControl qKey { get; private set; }
        public KeyControl rKey { get; private set; }
        public KeyControl sKey { get; private set; }
        public KeyControl tKey { get; private set; }
        public KeyControl uKey { get; private set; }
        public KeyControl vKey { get; private set; }
        public KeyControl wKey { get; private set; }
        public KeyControl xKey { get; private set; }
        public KeyControl yKey { get; private set; }
        public KeyControl zKey { get; private set; }
        public KeyControl digit1Key { get; private set; }
        public KeyControl digit2Key { get; private set; }
        public KeyControl digit3Key { get; private set; }
        public KeyControl digit4Key { get; private set; }
        public KeyControl digit5Key { get; private set; }
        public KeyControl digit6Key { get; private set; }
        public KeyControl digit7Key { get; private set; }
        public KeyControl digit8Key { get; private set; }
        public KeyControl digit9Key { get; private set; }
        public KeyControl digit0Key { get; private set; }
        public KeyControl leftShiftKey { get; private set; }
        public KeyControl rightShiftKey { get; private set; }
        public KeyControl leftAltKey { get; private set; }
        public KeyControl rightAltKey { get; private set; }
        public KeyControl leftCtrlKey { get; private set; }
        public KeyControl rightCtrlKey { get; private set; }
        public KeyControl leftMetaKey { get; private set; }
        public KeyControl rightMetaKey { get; private set; }
        public KeyControl leftWindowsKey { get; private set; }
        public KeyControl rightWindowsKey { get; private set; }
        public KeyControl leftAppleKey { get; private set; }
        public KeyControl rightAppleKey { get; private set; }
        public KeyControl leftCommandKey { get; private set; }
        public KeyControl rightCommandKey { get; private set; }
        public KeyControl contextMenuKey { get; private set; }
        public KeyControl escapeKey { get; private set; }
        public KeyControl leftArrowKey { get; private set; }
        public KeyControl rightArrowKey { get; private set; }
        public KeyControl upArrowKey { get; private set; }
        public KeyControl downArrowKey { get; private set; }
        public KeyControl backspaceKey { get; private set; }
        public KeyControl pageDownKey { get; private set; }
        public KeyControl pageUpKey { get; private set; }
        public KeyControl homeKey { get; private set; }
        public KeyControl endKey { get; private set; }
        public KeyControl insertKey { get; private set; }
        public KeyControl deleteKey { get; private set; }
        public KeyControl capsLockKey { get; private set; }
        public KeyControl scrollLockKey { get; private set; }
        public KeyControl numLockKey { get; private set; }
        public KeyControl printScreenKey { get; private set; }
        public KeyControl pauseKey { get; private set; }
        public KeyControl numpadEnterKey { get; private set; }
        public KeyControl numpadDivideKey { get; private set; }
        public KeyControl numpadMultiplyKey { get; private set; }
        public KeyControl numpadMinusKey { get; private set; }
        public KeyControl numpadPlusKey { get; private set; }
        public KeyControl numpadPeriodKey { get; private set; }
        public KeyControl numpadEqualsKey { get; private set; }
        public KeyControl numpad0Key { get; private set; }
        public KeyControl numpad1Key { get; private set; }
        public KeyControl numpad2Key { get; private set; }
        public KeyControl numpad3Key { get; private set; }
        public KeyControl numpad4Key { get; private set; }
        public KeyControl numpad5Key { get; private set; }
        public KeyControl numpad6Key { get; private set; }
        public KeyControl numpad7Key { get; private set; }
        public KeyControl numpad8Key { get; private set; }
        public KeyControl numpad9Key { get; private set; }
        public KeyControl f1Key { get; private set; }
        public KeyControl f2Key { get; private set; }
        public KeyControl f3Key { get; private set; }
        public KeyControl f4Key { get; private set; }
        public KeyControl f5Key { get; private set; }
        public KeyControl f6Key { get; private set; }
        public KeyControl f7Key { get; private set; }
        public KeyControl f8Key { get; private set; }
        public KeyControl f9Key { get; private set; }
        public KeyControl f10Key { get; private set; }
        public KeyControl f11Key { get; private set; }
        public KeyControl f12Key { get; private set; }
        public KeyControl oem1Key { get; private set; }
        public KeyControl oem2Key { get; private set; }
        public KeyControl oem3Key { get; private set; }
        public KeyControl oem4Key { get; private set; }
        public KeyControl oem5Key { get; private set; }

        public static Keyboard current { get; internal set; }

        /// <summary>
        /// Look up a key control by its key code.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException">The given <see cref="key"/> is not valid.</exception>
        public KeyControl this[Key key]
        {
            get
            {
                if (key >= Key.A && key <= Key.Z)
                {
                    switch (key)
                    {
                        case Key.A: return aKey;
                        case Key.B: return bKey;
                        case Key.C: return cKey;
                        case Key.D: return dKey;
                        case Key.E: return eKey;
                        case Key.F: return fKey;
                        case Key.G: return gKey;
                        case Key.H: return hKey;
                        case Key.I: return iKey;
                        case Key.J: return jKey;
                        case Key.K: return kKey;
                        case Key.L: return lKey;
                        case Key.M: return mKey;
                        case Key.N: return nKey;
                        case Key.O: return oKey;
                        case Key.P: return pKey;
                        case Key.Q: return qKey;
                        case Key.R: return rKey;
                        case Key.S: return sKey;
                        case Key.T: return tKey;
                        case Key.U: return uKey;
                        case Key.V: return vKey;
                        case Key.W: return wKey;
                        case Key.X: return xKey;
                        case Key.Y: return yKey;
                        case Key.Z: return zKey;
                    }
                }

                if (key >= Key.Digit1 && key <= Key.Digit0)
                {
                    switch (key)
                    {
                        case Key.Digit1: return digit1Key;
                        case Key.Digit2: return digit2Key;
                        case Key.Digit3: return digit3Key;
                        case Key.Digit4: return digit4Key;
                        case Key.Digit5: return digit5Key;
                        case Key.Digit6: return digit6Key;
                        case Key.Digit7: return digit7Key;
                        case Key.Digit8: return digit8Key;
                        case Key.Digit9: return digit9Key;
                        case Key.Digit0: return digit0Key;
                    }
                }

                if (key >= Key.F1 && key <= Key.F12)
                {
                    switch (key)
                    {
                        case Key.F1: return f1Key;
                        case Key.F2: return f2Key;
                        case Key.F3: return f3Key;
                        case Key.F4: return f4Key;
                        case Key.F5: return f5Key;
                        case Key.F6: return f6Key;
                        case Key.F7: return f7Key;
                        case Key.F8: return f8Key;
                        case Key.F9: return f9Key;
                        case Key.F10: return f10Key;
                        case Key.F11: return f11Key;
                        case Key.F12: return f12Key;
                    }
                }

                if (key >= Key.NumpadEnter && key <= Key.Numpad9)
                {
                    switch (key)
                    {
                        case Key.NumpadEnter: return numpadEnterKey;
                        case Key.NumpadDivide: return numpadDivideKey;
                        case Key.NumpadMultiply: return numpadMultiplyKey;
                        case Key.NumpadPlus: return numpadPlusKey;
                        case Key.NumpadMinus: return numpadMinusKey;
                        case Key.NumpadPeriod: return numpadPeriodKey;
                        case Key.NumpadEquals: return numpadEqualsKey;
                        case Key.Numpad0: return numpad0Key;
                        case Key.Numpad1: return numpad1Key;
                        case Key.Numpad2: return numpad2Key;
                        case Key.Numpad3: return numpad3Key;
                        case Key.Numpad4: return numpad4Key;
                        case Key.Numpad5: return numpad5Key;
                        case Key.Numpad6: return numpad6Key;
                        case Key.Numpad7: return numpad7Key;
                        case Key.Numpad8: return numpad8Key;
                        case Key.Numpad9: return numpad9Key;
                    }
                }

                switch (key)
                {
                    case Key.Space: return spaceKey;
                    case Key.Enter: return enterKey;
                    case Key.Tab: return tabKey;
                    case Key.Backquote: return backquoteKey;
                    case Key.Quote: return quoteKey;
                    case Key.Semicolon: return semicolonKey;
                    case Key.Comma: return commaKey;
                    case Key.Period: return periodKey;
                    case Key.Slash: return slashKey;
                    case Key.Backslash: return backslashKey;
                    case Key.LeftBracket: return leftBracketKey;
                    case Key.RightBracket: return rightBracketKey;
                    case Key.Minus: return minusKey;
                    case Key.Equals: return equalsKey;
                    case Key.LeftShift: return leftShiftKey;
                    case Key.RightShift: return rightShiftKey;
                    case Key.LeftAlt: return leftAltKey;
                    case Key.RightAlt: return rightAltKey;
                    case Key.LeftCtrl: return leftCtrlKey;
                    case Key.RightCtrl: return rightCtrlKey;
                    case Key.LeftMeta: return leftMetaKey;
                    case Key.RightMeta: return rightMetaKey;
                    case Key.ContextMenu: return contextMenuKey;
                    case Key.Escape: return escapeKey;
                    case Key.LeftArrow: return leftArrowKey;
                    case Key.RightArrow: return rightArrowKey;
                    case Key.UpArrow: return upArrowKey;
                    case Key.DownArrow: return downArrowKey;
                    case Key.Backspace: return backspaceKey;
                    case Key.PageDown: return pageDownKey;
                    case Key.PageUp: return pageUpKey;
                    case Key.Home: return homeKey;
                    case Key.End: return endKey;
                    case Key.Insert: return insertKey;
                    case Key.Delete: return deleteKey;
                    case Key.CapsLock: return capsLockKey;
                    case Key.NumLock: return numLockKey;
                    case Key.PrintScreen: return printScreenKey;
                    case Key.ScrollLock: return scrollLockKey;
                    case Key.Pause: return pauseKey;
                    case Key.OEM1: return oem1Key;
                    case Key.OEM2: return oem2Key;
                    case Key.OEM3: return oem3Key;
                    case Key.OEM4: return oem4Key;
                    case Key.OEM5: return oem5Key;
                }

                throw new ArgumentException("key");
            }
        }

        public override void MakeCurrent()
        {
            base.MakeCurrent();
            current = this;
        }

        protected override void FinishSetup(InputDeviceBuilder builder)
        {
            anyKey = builder.GetControl<AnyKeyControl>("AnyKey");
            spaceKey = builder.GetControl<KeyControl>("Space");
            enterKey = builder.GetControl<KeyControl>("Enter");
            tabKey = builder.GetControl<KeyControl>("Tab");
            backquoteKey = builder.GetControl<KeyControl>("Backquote");
            quoteKey = builder.GetControl<KeyControl>("Quote");
            semicolonKey = builder.GetControl<KeyControl>("Semicolon");
            commaKey = builder.GetControl<KeyControl>("Comma");
            periodKey = builder.GetControl<KeyControl>("Period");
            slashKey = builder.GetControl<KeyControl>("Slash");
            backslashKey = builder.GetControl<KeyControl>("Backslash");
            leftBracketKey = builder.GetControl<KeyControl>("LeftBracket");
            rightBracketKey = builder.GetControl<KeyControl>("RightBracket");
            minusKey = builder.GetControl<KeyControl>("Minus");
            equalsKey = builder.GetControl<KeyControl>("Equals");
            aKey = builder.GetControl<KeyControl>("A");
            bKey = builder.GetControl<KeyControl>("B");
            cKey = builder.GetControl<KeyControl>("C");
            dKey = builder.GetControl<KeyControl>("D");
            eKey = builder.GetControl<KeyControl>("E");
            fKey = builder.GetControl<KeyControl>("F");
            gKey = builder.GetControl<KeyControl>("G");
            hKey = builder.GetControl<KeyControl>("H");
            iKey = builder.GetControl<KeyControl>("I");
            jKey = builder.GetControl<KeyControl>("J");
            kKey = builder.GetControl<KeyControl>("K");
            lKey = builder.GetControl<KeyControl>("L");
            mKey = builder.GetControl<KeyControl>("M");
            nKey = builder.GetControl<KeyControl>("N");
            oKey = builder.GetControl<KeyControl>("O");
            pKey = builder.GetControl<KeyControl>("P");
            qKey = builder.GetControl<KeyControl>("Q");
            rKey = builder.GetControl<KeyControl>("R");
            sKey = builder.GetControl<KeyControl>("S");
            tKey = builder.GetControl<KeyControl>("T");
            uKey = builder.GetControl<KeyControl>("U");
            vKey = builder.GetControl<KeyControl>("V");
            wKey = builder.GetControl<KeyControl>("W");
            xKey = builder.GetControl<KeyControl>("X");
            yKey = builder.GetControl<KeyControl>("Y");
            zKey = builder.GetControl<KeyControl>("Z");
            digit1Key = builder.GetControl<KeyControl>("1");
            digit2Key = builder.GetControl<KeyControl>("2");
            digit3Key = builder.GetControl<KeyControl>("3");
            digit4Key = builder.GetControl<KeyControl>("4");
            digit5Key = builder.GetControl<KeyControl>("5");
            digit6Key = builder.GetControl<KeyControl>("6");
            digit7Key = builder.GetControl<KeyControl>("7");
            digit8Key = builder.GetControl<KeyControl>("8");
            digit9Key = builder.GetControl<KeyControl>("9");
            digit0Key = builder.GetControl<KeyControl>("0");
            leftShiftKey = builder.GetControl<KeyControl>("LeftShift");
            rightShiftKey = builder.GetControl<KeyControl>("RightShift");
            leftAltKey = builder.GetControl<KeyControl>("LeftAlt");
            rightAltKey = builder.GetControl<KeyControl>("RightAlt");
            leftCtrlKey = builder.GetControl<KeyControl>("LeftCtrl");
            rightCtrlKey = builder.GetControl<KeyControl>("RightCtrl");
            leftMetaKey = builder.GetControl<KeyControl>("LeftMeta");
            rightMetaKey = builder.GetControl<KeyControl>("RightMeta");
            leftWindowsKey = builder.GetControl<KeyControl>("LeftWindows");
            rightWindowsKey = builder.GetControl<KeyControl>("RightWindows");
            leftAppleKey = builder.GetControl<KeyControl>("LeftApple");
            rightAppleKey = builder.GetControl<KeyControl>("RightApple");
            leftCommandKey = builder.GetControl<KeyControl>("LeftCommand");
            rightCommandKey = builder.GetControl<KeyControl>("RightCommand");
            contextMenuKey = builder.GetControl<KeyControl>("ContextMenu");
            escapeKey = builder.GetControl<KeyControl>("Escape");
            leftArrowKey = builder.GetControl<KeyControl>("LeftArrow");
            rightArrowKey = builder.GetControl<KeyControl>("RightArrow");
            upArrowKey = builder.GetControl<KeyControl>("UpArrow");
            downArrowKey = builder.GetControl<KeyControl>("DownArrow");
            backspaceKey = builder.GetControl<KeyControl>("Backspace");
            pageDownKey = builder.GetControl<KeyControl>("PageDown");
            pageUpKey = builder.GetControl<KeyControl>("PageUp");
            homeKey = builder.GetControl<KeyControl>("Home");
            endKey = builder.GetControl<KeyControl>("End");
            insertKey = builder.GetControl<KeyControl>("Insert");
            deleteKey = builder.GetControl<KeyControl>("Delete");
            numpadEnterKey = builder.GetControl<KeyControl>("NumpadEnter");
            numpadDivideKey = builder.GetControl<KeyControl>("NumpadDivide");
            numpadMultiplyKey = builder.GetControl<KeyControl>("NumpadMultiply");
            numpadPlusKey = builder.GetControl<KeyControl>("NumpadPlus");
            numpadMinusKey = builder.GetControl<KeyControl>("NumpadMinus");
            numpadPeriodKey = builder.GetControl<KeyControl>("NumpadPeriod");
            numpadEqualsKey = builder.GetControl<KeyControl>("NumpadEquals");
            numpad0Key = builder.GetControl<KeyControl>("Numpad0");
            numpad1Key = builder.GetControl<KeyControl>("Numpad1");
            numpad2Key = builder.GetControl<KeyControl>("Numpad2");
            numpad3Key = builder.GetControl<KeyControl>("Numpad3");
            numpad4Key = builder.GetControl<KeyControl>("Numpad4");
            numpad5Key = builder.GetControl<KeyControl>("Numpad5");
            numpad6Key = builder.GetControl<KeyControl>("Numpad6");
            numpad7Key = builder.GetControl<KeyControl>("Numpad7");
            numpad8Key = builder.GetControl<KeyControl>("Numpad8");
            numpad9Key = builder.GetControl<KeyControl>("Numpad9");
            f1Key = builder.GetControl<KeyControl>("F1");
            f2Key = builder.GetControl<KeyControl>("F2");
            f3Key = builder.GetControl<KeyControl>("F3");
            f4Key = builder.GetControl<KeyControl>("F4");
            f5Key = builder.GetControl<KeyControl>("F5");
            f6Key = builder.GetControl<KeyControl>("F6");
            f7Key = builder.GetControl<KeyControl>("F7");
            f8Key = builder.GetControl<KeyControl>("F8");
            f9Key = builder.GetControl<KeyControl>("F9");
            f10Key = builder.GetControl<KeyControl>("F10");
            f11Key = builder.GetControl<KeyControl>("F11");
            f12Key = builder.GetControl<KeyControl>("F12");
            capsLockKey = builder.GetControl<KeyControl>("CapsLock");
            numLockKey = builder.GetControl<KeyControl>("NumLock");
            scrollLockKey = builder.GetControl<KeyControl>("ScrollLock");
            printScreenKey = builder.GetControl<KeyControl>("PrintScreen");
            pauseKey = builder.GetControl<KeyControl>("Pause");
            oem1Key = builder.GetControl<KeyControl>("OEM1");
            oem2Key = builder.GetControl<KeyControl>("OEM2");
            oem3Key = builder.GetControl<KeyControl>("OEM3");
            oem4Key = builder.GetControl<KeyControl>("OEM4");
            oem5Key = builder.GetControl<KeyControl>("OEM5");

            ////REVIEW: Ideally, we'd have a way to do this through layouts; this way nested key controls could work, too,
            ////        and it just seems somewhat dirty to jam the data into the control here

            // Assign key code to all keys.
            for (var key = 1; key < (int)Key.Count; ++key)
                this[(Key)key].keyCode = (Key)key;

            base.FinishSetup(builder);
        }

        protected override void RefreshConfiguration()
        {
            keyboardLayout = null;
            var command = QueryKeyboardLayoutCommand.Create();
            if (OnDeviceCommand(ref command) >= 0)
                keyboardLayout = command.ReadLayoutName();
        }

        public override void OnTextInput(char character)
        {
            for (var i = 0; i < m_TextInputListeners.Count; ++i)
                m_TextInputListeners[i](character);
        }

        internal InlinedArray<Action<char>> m_TextInputListeners;
        private string m_KeyboardLayoutName;
    }

    public class OnScreenKeyboard : Keyboard
    {
    }
}
