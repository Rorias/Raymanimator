public sealed class IntroAnimation
{
#region Singleton
private static IntroAnimation _instance;
private static object _lock = new object();

private IntroAnimation() { }

public static IntroAnimation Instance
{
get
{
if (null == _instance)
{
lock (_lock)
{
if (null == _instance)
{
_instance = new IntroAnimation();
}
}
}
return _instance;
}
}
#endregion

public AnimationStateMachine asm;

public void IntroAnimation(int subFrame)
{
switch(subFrame)
{
case 0:
asm.SetPart(0, 2, 10.1875f, -4.5625f, false);
asm.SetPart(1, 16, 9.875f, -4.75f, false);
asm.SetPart(2, 49, 10.25f, -3.75f, false);
asm.SetPart(3, 53, 9.8125f, -2.59375f, false);
asm.SetPart(4, 83, 9.8125f, -1.90625f, false);
asm.SetPart(5, 108, 10.5f, -5.28125f, false);
asm.SetPart(6, 117, 10.25f, -4.65625f, false);
break;
case 1:
asm.SetPart(0, 2, 10.3125f, -4.5625f, false);
asm.SetPart(1, 16, 9.625f, -4.8125f, false);
asm.SetPart(2, 49, 10.125f, -3.75f, false);
asm.SetPart(3, 53, 9.6875f, -2.59375f, false);
asm.SetPart(4, 83, 9.6875f, -1.90625f, false);
asm.SetPart(5, 108, 10.5f, -5.28125f, false);
asm.SetPart(6, 117, 9.8125f, -4.65625f, false);
break;
case 2:
asm.SetPart(0, 2, 10.375f, -4.5625f, false);
asm.SetPart(1, 16, 9.375f, -4.875f, false);
asm.SetPart(2, 49, 10f, -3.75f, false);
asm.SetPart(3, 53, 9.5625f, -2.59375f, false);
asm.SetPart(4, 83, 9.5625f, -1.90625f, false);
asm.SetPart(5, 108, 10.5f, -5.28125f, false);
asm.SetPart(6, 118, 9.4375f, -4.625f, false);
break;
case 3:
asm.SetPart(0, 2, 10.5f, -4.4375f, false);
asm.SetPart(1, 16, 9.0625f, -4.875f, false);
asm.SetPart(2, 50, 9.9375f, -3.6875f, false);
asm.SetPart(3, 53, 9.4375f, -2.59375f, false);
asm.SetPart(4, 84, 9.5625f, -1.875f, false);
asm.SetPart(5, 108, 10.5f, -5.28125f, false);
asm.SetPart(6, 118, 9f, -4.5f, false);
break;
case 4:
asm.SetPart(0, 2, 10.4375f, -4.4375f, false);
asm.SetPart(1, 16, 8.75f, -5f, false);
asm.SetPart(2, 50, 9.75f, -3.75f, false);
asm.SetPart(3, 53, 9.3125f, -2.65625f, false);
asm.SetPart(4, 84, 9.4375f, -1.9375f, false);
asm.SetPart(5, 109, 10.5f, -5.1875f, false);
asm.SetPart(6, 112, 8.75f, -4.40625f, false);
break;
case 5:
asm.SetPart(0, 2, 10.4375f, -4.375f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 50, 9.625f, -3.8125f, false);
asm.SetPart(3, 53, 9.1875f, -2.71875f, false);
asm.SetPart(4, 84, 9.3125f, -2f, false);
asm.SetPart(5, 109, 10.5f, -5.1875f, false);
asm.SetPart(6, 112, 8.5f, -4.28125f, false);
break;
case 6:
asm.SetPart(0, 2, 10.375f, -4.375f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 50, 9.5f, -3.875f, false);
asm.SetPart(3, 53, 9.0625f, -2.84375f, false);
asm.SetPart(4, 84, 9.1875f, -2.125f, false);
asm.SetPart(5, 109, 10.5f, -5.1875f, false);
asm.SetPart(6, 112, 8.3125f, -4.28125f, false);
break;
case 7:
asm.SetPart(0, 2, 10.375f, -4.25f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 43, 9.5f, -3.875f, false);
asm.SetPart(3, 53, 8.9375f, -2.84375f, false);
asm.SetPart(4, 85, 9f, -2.15625f, false);
asm.SetPart(5, 109, 10.5f, -5.1875f, false);
asm.SetPart(6, 112, 8.125f, -4.09375f, false);
break;
case 8:
asm.SetPart(0, 2, 10.25f, -4.25f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 43, 9.375f, -3.875f, false);
asm.SetPart(3, 53, 8.8125f, -2.84375f, false);
asm.SetPart(4, 85, 8.875f, -2.15625f, false);
asm.SetPart(5, 107, 10.25f, -5.03125f, false);
asm.SetPart(6, 112, 8.125f, -4.15625f, false);
break;
case 9:
asm.SetPart(0, 2, 10.0625f, -4.3125f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 43, 9.25f, -3.8125f, false);
asm.SetPart(3, 53, 8.6875f, -2.78125f, false);
asm.SetPart(4, 85, 8.75f, -2.09375f, false);
asm.SetPart(5, 107, 9.875f, -4.96875f, false);
asm.SetPart(6, 112, 8.125f, -4.28125f, false);
break;
case 10:
asm.SetPart(0, 2, 9.8125f, -4.375f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 43, 9.125f, -3.8125f, false);
asm.SetPart(3, 53, 8.5625f, -2.78125f, false);
asm.SetPart(4, 85, 8.625f, -2.09375f, false);
asm.SetPart(5, 107, 9.625f, -4.90625f, false);
asm.SetPart(6, 112, 8.125f, -4.34375f, false);
break;
case 11:
asm.SetPart(0, 2, 9.5f, -4.5f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 44, 9f, -3.875f, false);
asm.SetPart(3, 53, 8.4375f, -2.71875f, false);
asm.SetPart(4, 82, 8.4375f, -2.09375f, false);
asm.SetPart(5, 107, 9.375f, -4.90625f, false);
asm.SetPart(6, 112, 8.1875f, -4.46875f, false);
break;
case 12:
asm.SetPart(0, 2, 9.1875f, -4.5f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 44, 8.875f, -3.875f, false);
asm.SetPart(3, 53, 8.3125f, -2.71875f, false);
asm.SetPart(4, 82, 8.3125f, -2.09375f, false);
asm.SetPart(5, 107, 9.125f, -4.96875f, false);
asm.SetPart(6, 118, 8.25f, -4.5f, false);
break;
case 13:
asm.SetPart(0, 2, 8.875f, -4.5f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 44, 8.75f, -3.8125f, false);
asm.SetPart(3, 53, 8.1875f, -2.65625f, false);
asm.SetPart(4, 82, 8.1875f, -2.03125f, false);
asm.SetPart(5, 107, 8.875f, -4.90625f, false);
asm.SetPart(6, 118, 8.25f, -4.5f, false);
break;
case 14:
asm.SetPart(0, 2, 8.5625f, -4.4375f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 44, 8.625f, -3.75f, false);
asm.SetPart(3, 53, 8.0625f, -2.59375f, false);
asm.SetPart(4, 82, 8.0625f, -1.96875f, false);
asm.SetPart(5, 107, 8.625f, -4.90625f, false);
asm.SetPart(6, 117, 8.3125f, -4.46875f, false);
break;
case 15:
asm.SetPart(0, 2, 8.1875f, -4.3125f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 45, 8.375f, -3.65625f, false);
asm.SetPart(3, 53, 7.9375f, -2.46875f, false);
asm.SetPart(4, 83, 7.9375f, -1.78125f, false);
asm.SetPart(5, 107, 8.375f, -4.84375f, false);
asm.SetPart(6, 117, 8.375f, -4.46875f, false);
break;
case 16:
asm.SetPart(0, 0, 7.875f, -4.25f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 45, 8.25f, -3.59375f, false);
asm.SetPart(3, 53, 7.8125f, -2.40625f, false);
asm.SetPart(4, 83, 7.8125f, -1.71875f, false);
asm.SetPart(5, 110, 7.9375f, -4.84375f, false);
asm.SetPart(6, 117, 8.4375f, -4.34375f, false);
break;
case 17:
asm.SetPart(0, 0, 7.5625f, -4.25f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 45, 8.125f, -3.65625f, false);
asm.SetPart(3, 53, 7.6875f, -2.46875f, false);
asm.SetPart(4, 83, 7.6875f, -1.78125f, false);
asm.SetPart(5, 110, 7.625f, -4.84375f, false);
asm.SetPart(6, 117, 8.5f, -4.40625f, false);
break;
case 18:
asm.SetPart(0, 0, 7.25f, -4.25f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 45, 8f, -3.71875f, false);
asm.SetPart(3, 53, 7.5625f, -2.53125f, false);
asm.SetPart(4, 83, 7.5625f, -1.84375f, false);
asm.SetPart(5, 110, 7.3125f, -4.84375f, false);
asm.SetPart(6, 117, 8.5625f, -4.46875f, false);
break;
case 19:
asm.SetPart(0, 0, 6.9375f, -4.1875f, false);
asm.SetPart(1, 28, 8.5f, -5.28125f, false);
asm.SetPart(2, 46, 7.875f, -3.6875f, false);
asm.SetPart(3, 53, 7.375f, -2.65625f, false);
asm.SetPart(4, 84, 7.5f, -1.9375f, false);
asm.SetPart(5, 110, 7f, -4.84375f, false);
asm.SetPart(6, 136, 8.6875f, -4.375f, false);
break;
case 20:
asm.SetPart(0, 1, 6.75f, -4.125f, false);
asm.SetPart(1, 17, 8.375f, -5.03125f, false);
asm.SetPart(2, 46, 7.75f, -3.6875f, false);
asm.SetPart(3, 53, 7.25f, -2.65625f, false);
asm.SetPart(4, 84, 7.375f, -1.9375f, false);
asm.SetPart(5, 110, 6.6875f, -4.90625f, false);
asm.SetPart(6, 136, 8.5625f, -4.3125f, false);
break;
case 21:
asm.SetPart(0, 1, 6.5f, -4.125f, false);
asm.SetPart(1, 17, 8.375f, -5.03125f, false);
asm.SetPart(2, 46, 7.625f, -3.75f, false);
asm.SetPart(3, 53, 7.125f, -2.71875f, false);
asm.SetPart(4, 84, 7.25f, -2f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8.5f, -4.25f, false);
break;
case 22:
asm.SetPart(0, 1, 6.3125f, -4.0625f, false);
asm.SetPart(1, 17, 8.375f, -5.03125f, false);
asm.SetPart(2, 46, 7.5f, -3.75f, false);
asm.SetPart(3, 53, 7f, -2.78125f, false);
asm.SetPart(4, 84, 7.125f, -2.0625f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8.4375f, -4.1875f, false);
break;
case 23:
asm.SetPart(0, 1, 6.125f, -4.0625f, false);
asm.SetPart(1, 17, 8.375f, -5.03125f, false);
asm.SetPart(2, 47, 7.3125f, -3.8125f, false);
asm.SetPart(3, 53, 6.8125f, -2.84375f, false);
asm.SetPart(4, 85, 6.875f, -2.15625f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8.375f, -4.125f, false);
break;
case 24:
asm.SetPart(0, 0, 6.0625f, -4.125f, false);
asm.SetPart(1, 25, 8.1875f, -4.96875f, false);
asm.SetPart(2, 47, 7.1875f, -3.8125f, false);
asm.SetPart(3, 53, 6.6875f, -2.84375f, false);
asm.SetPart(4, 85, 6.75f, -2.15625f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8.25f, -4.1875f, false);
break;
case 25:
asm.SetPart(0, 0, 6f, -4.125f, false);
asm.SetPart(1, 25, 7.9375f, -4.96875f, false);
asm.SetPart(2, 47, 7.0625f, -3.75f, false);
asm.SetPart(3, 53, 6.5625f, -2.78125f, false);
asm.SetPart(4, 85, 6.625f, -2.09375f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8.125f, -4.125f, false);
break;
case 26:
asm.SetPart(0, 0, 5.9375f, -4.125f, false);
asm.SetPart(1, 25, 7.75f, -4.96875f, false);
asm.SetPart(2, 47, 6.9375f, -3.6875f, false);
asm.SetPart(3, 53, 6.4375f, -2.71875f, false);
asm.SetPart(4, 85, 6.5f, -2.03125f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 8f, -4.125f, false);
break;
case 27:
asm.SetPart(0, 0, 5.9375f, -4.3125f, false);
asm.SetPart(1, 25, 7.5f, -4.90625f, false);
asm.SetPart(2, 48, 6.8125f, -3.75f, false);
asm.SetPart(3, 53, 6.375f, -2.65625f, false);
asm.SetPart(4, 82, 6.375f, -2.03125f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 7.8125f, -4.3125f, false);
break;
case 28:
asm.SetPart(0, 0, 5.9375f, -4.3125f, false);
asm.SetPart(1, 25, 7.25f, -4.84375f, false);
asm.SetPart(2, 48, 6.6875f, -3.75f, false);
asm.SetPart(3, 53, 6.25f, -2.65625f, false);
asm.SetPart(4, 82, 6.25f, -2.03125f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 7.5625f, -4.3125f, false);
break;
case 29:
asm.SetPart(0, 0, 6f, -4.375f, false);
asm.SetPart(1, 25, 7f, -4.78125f, false);
asm.SetPart(2, 48, 6.5625f, -3.6875f, false);
asm.SetPart(3, 53, 6.125f, -2.59375f, false);
asm.SetPart(4, 82, 6.125f, -1.96875f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 136, 7.25f, -4.375f, false);
break;
case 30:
asm.SetPart(0, 0, 6.0625f, -4.375f, false);
asm.SetPart(1, 25, 6.6875f, -4.71875f, false);
asm.SetPart(2, 48, 6.4375f, -3.6875f, false);
asm.SetPart(3, 53, 6f, -2.59375f, false);
asm.SetPart(4, 82, 6f, -1.96875f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 117, 6.9375f, -4.46875f, false);
break;
case 31:
asm.SetPart(0, 0, 5.8125f, -4.25f, false);
asm.SetPart(1, 28, 6f, -4.78125f, false);
asm.SetPart(2, 48, 6.4375f, -3.6875f, false);
asm.SetPart(3, 54, 5.9375f, -2.59375f, false);
asm.SetPart(4, 82, 6f, -1.96875f, false);
asm.SetPart(5, 108, 6.5f, -5.21875f, false);
asm.SetPart(6, 117, 6.875f, -4.53125f, false);
break;
case 32:
asm.SetPart(0, 118, 5.5f, -4.125f, false);
asm.SetPart(1, 17, 5.75f, -4.71875f, false);
asm.SetPart(2, 39, 6.5625f, -3.71875f, false);
asm.SetPart(3, 54, 6f, -2.59375f, false);
asm.SetPart(4, 82, 6.0625f, -1.96875f, false);
asm.SetPart(5, 104, 6.625f, -5.1875f, false);
asm.SetPart(6, 111, 6.625f, -4.5f, false);
break;
case 33:
asm.SetPart(0, 1, 5.3125f, -3.9375f, false);
asm.SetPart(1, 17, 5.5625f, -4.84375f, false);
asm.SetPart(2, 39, 6.75f, -3.71875f, false);
asm.SetPart(3, 54, 6.0625f, -2.59375f, false);
asm.SetPart(4, 82, 6.125f, -1.96875f, false);
asm.SetPart(5, 103, 6.5625f, -5.125f, false);
asm.SetPart(6, 118, 6.375f, -4.375f, false);
break;
case 34:
asm.SetPart(0, 1, 5.1875f, -3.75f, false);
asm.SetPart(1, 17, 5.4375f, -4.90625f, false);
asm.SetPart(2, 40, 6.75f, -3.6875f, false);
asm.SetPart(3, 69, 6.125f, -2.5f, false);
asm.SetPart(4, 82, 6.25f, -1.90625f, false);
asm.SetPart(5, 104, 6.5f, -5.1875f, true);
asm.SetPart(6, 118, 6.25f, -4.3125f, false);
break;
case 35:
asm.SetPart(0, 5, 5.0625f, -3.25f, false);
asm.SetPart(1, 17, 5.25f, -4.90625f, false);
asm.SetPart(2, 40, 6.875f, -3.6875f, false);
asm.SetPart(3, 69, 6.25f, -2.5f, false);
asm.SetPart(4, 82, 6.375f, -1.90625f, false);
asm.SetPart(5, 104, 6.75f, -5.1875f, true);
asm.SetPart(6, 124, 6.3125f, -4.15625f, false);
break;
case 36:
asm.SetPart(0, 5, 5f, -3.0625f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.9375f, -3.6875f, false);
asm.SetPart(3, 61, 6.625f, -2.8125f, false);
asm.SetPart(4, 70, 6.5625f, -2.15625f, false);
asm.SetPart(5, 104, 6.9375f, -5.1875f, true);
asm.SetPart(6, 124, 6.5625f, -4.03125f, false);
break;
case 37:
asm.SetPart(0, 5, 5.125f, -2.8125f, false);
asm.SetPart(1, 28, 5.125f, -5.09375f, false);
asm.SetPart(2, 40, 6.9375f, -3.75f, false);
asm.SetPart(3, 61, 6.8125f, -2.75f, false);
asm.SetPart(4, 70, 6.75f, -2.09375f, false);
asm.SetPart(5, 104, 7.1875f, -5.1875f, true);
asm.SetPart(6, 124, 6.75f, -3.96875f, false);
break;
case 38:
asm.SetPart(0, 5, 5.3125f, -2.6875f, false);
asm.SetPart(1, 28, 5.125f, -5.09375f, false);
asm.SetPart(2, 40, 6.875f, -3.875f, false);
asm.SetPart(3, 61, 6.875f, -2.8125f, false);
asm.SetPart(4, 70, 6.8125f, -2.15625f, false);
asm.SetPart(5, 104, 7.25f, -5.1875f, true);
asm.SetPart(6, 124, 7f, -3.84375f, false);
break;
case 39:
asm.SetPart(0, 5, 5.4375f, -2.5f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.8125f, -4f, false);
asm.SetPart(3, 61, 6.8125f, -2.875f, false);
asm.SetPart(4, 70, 6.75f, -2.21875f, false);
asm.SetPart(5, 104, 7.25f, -5.1875f, true);
asm.SetPart(6, 120, 7.5f, -3.59375f, false);
break;
case 40:
asm.SetPart(0, 5, 5.625f, -2.4375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.6875f, -4f, false);
asm.SetPart(3, 61, 6.8125f, -2.9375f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.1875f, -5.1875f, true);
asm.SetPart(6, 120, 7.75f, -3.40625f, false);
break;
case 41:
asm.SetPart(0, 5, 5.625f, -2.5f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -3.9375f, false);
asm.SetPart(3, 61, 6.8125f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.125f, -5.1875f, true);
asm.SetPart(6, 121, 7.9375f, -2.9375f, false);
break;
case 42:
asm.SetPart(0, 5, 5.5625f, -2.625f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -3.9375f, false);
asm.SetPart(3, 62, 6.75f, -3.03125f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 8f, -3.0625f, false);
break;
case 43:
asm.SetPart(0, 5, 5.5f, -2.75f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -3.9375f, false);
asm.SetPart(3, 62, 6.75f, -3.09375f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 8f, -3.1875f, false);
break;
case 44:
asm.SetPart(0, 5, 5.5625f, -2.875f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4f, false);
asm.SetPart(3, 62, 6.75f, -3.09375f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.9375f, -3.3125f, false);
break;
case 45:
asm.SetPart(0, 11, 5.625f, -3.1875f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4f, false);
asm.SetPart(3, 62, 6.75f, -3.03125f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.4375f, false);
break;
case 46:
asm.SetPart(0, 11, 5.6875f, -3.375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4f, false);
asm.SetPart(3, 64, 6.8125f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 47:
asm.SetPart(0, 9, 5.75f, -3.78125f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4f, false);
asm.SetPart(3, 62, 6.75f, -2.96875f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 48:
asm.SetPart(0, 9, 5.8125f, -3.96875f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4f, false);
asm.SetPart(3, 61, 6.8125f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 49:
asm.SetPart(0, 9, 5.9375f, -4.09375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4.0625f, false);
asm.SetPart(3, 80, 7f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 50:
asm.SetPart(0, 9, 5.9375f, -4.09375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4.0625f, false);
asm.SetPart(3, 80, 7f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 51:
asm.SetPart(0, 9, 5.9375f, -4.09375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4.0625f, false);
asm.SetPart(3, 80, 7f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 52:
asm.SetPart(0, 9, 5.9375f, -4.09375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4.0625f, false);
asm.SetPart(3, 80, 7f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
case 53:
asm.SetPart(0, 9, 5.9375f, -4.09375f, false);
asm.SetPart(1, 28, 5.1875f, -5.09375f, false);
asm.SetPart(2, 40, 6.5625f, -4.0625f, false);
asm.SetPart(3, 80, 7f, -3f, false);
asm.SetPart(4, 70, 6.75f, -2.28125f, false);
asm.SetPart(5, 104, 7.0625f, -5.1875f, true);
asm.SetPart(6, 121, 7.875f, -3.5f, false);
break;
}
}
}