public sealed class Combination!
{
#region Singleton
private static Combination! _instance;
private static object _lock = new object();

private Combination!() { }

public static Combination! Instance
{
get
{
if (null == _instance)
{
lock (_lock)
{
if (null == _instance)
{
_instance = new Combination!();
}
}
}
return _instance;
}
}
#endregion

public AnimationStateMachine asm;

public void Combination!(int subFrame)
{
switch(subFrame)
{
case 0:
asm.SetPart(0, 0, -0.375f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.1875f, false);
asm.SetPart(3, 53, -0.5f, 1f, false);
asm.SetPart(4, 82, -0.4375f, 1.625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.4375f, -1f, false);
break;
case 1:
asm.SetPart(0, 0, -0.375f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.125f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 82, -0.4375f, 1.6875f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.375f, -1f, false);
break;
case 2:
asm.SetPart(0, 0, -0.4375f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.0625f, false);
asm.SetPart(3, 53, -0.5f, 1.125f, false);
asm.SetPart(4, 82, -0.4375f, 1.75f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.3125f, -0.9375f, false);
break;
case 3:
asm.SetPart(0, 0, -0.4375f, -0.90625f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.0625f, false);
asm.SetPart(3, 53, -0.5f, 1.125f, false);
asm.SetPart(4, 82, -0.4375f, 1.75f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.3125f, -0.9375f, false);
break;
case 4:
asm.SetPart(0, 0, -0.4375f, -0.90625f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.125f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 82, -0.4375f, 1.6875f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.375f, -0.9375f, false);
break;
case 5:
asm.SetPart(0, 0, -0.4375f, -0.90625f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.125f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 82, -0.4375f, 1.6875f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.375f, -1f, false);
break;
case 6:
asm.SetPart(0, 0, -0.4375f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.1875f, false);
asm.SetPart(3, 53, -0.5f, 1f, false);
asm.SetPart(4, 82, -0.4375f, 1.625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.4375f, -1f, false);
break;
case 7:
asm.SetPart(0, 0, -0.375f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.25f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.4375f, 1.5625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5f, -1f, false);
break;
case 8:
asm.SetPart(0, 0, -0.3125f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.25f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.4375f, 1.5625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5f, -1f, false);
break;
case 9:
asm.SetPart(0, 0, -0.3125f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.3125f, false);
asm.SetPart(3, 53, -0.5f, 0.875f, false);
asm.SetPart(4, 82, -0.4375f, 1.5f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5625f, -1f, false);
break;
case 10:
asm.SetPart(0, 0, -0.25f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.3125f, false);
asm.SetPart(3, 53, -0.5f, 0.875f, false);
asm.SetPart(4, 82, -0.4375f, 1.5f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5625f, -1f, false);
break;
case 11:
asm.SetPart(0, 0, -0.25f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.25f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.4375f, 1.5625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5625f, -1f, false);
break;
case 12:
asm.SetPart(0, 0, -0.25f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 45, 0f, -0.25f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.4375f, 1.5625f, false);
asm.SetPart(5, 104, 0.25f, -1.65625f, false);
asm.SetPart(6, 117, 0.5f, -1f, false);
break;
case 13:
asm.SetPart(0, 2, -0.125f, -0.90625f, false);
asm.SetPart(1, 16, -0.4375f, -1.09375f, false);
asm.SetPart(2, 49, -0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 83, -0.5f, 1.75f, false);
asm.SetPart(5, 108, 0.1875f, -1.625f, false);
asm.SetPart(6, 117, -0.0625f, -1f, false);
break;
case 14:
asm.SetPart(0, 2, 0.125f, -0.90625f, false);
asm.SetPart(1, 16, -0.5625f, -1.15625f, false);
asm.SetPart(2, 49, -0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 83, -0.5f, 1.75f, false);
asm.SetPart(5, 108, 0.3125f, -1.625f, false);
asm.SetPart(6, 117, -0.375f, -1f, false);
break;
case 15:
asm.SetPart(0, 2, 0.3125f, -0.90625f, false);
asm.SetPart(1, 16, -0.6875f, -1.21875f, false);
asm.SetPart(2, 49, -0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 83, -0.5f, 1.75f, false);
asm.SetPart(5, 108, 0.4375f, -1.625f, false);
asm.SetPart(6, 118, -0.625f, -0.96875f, false);
break;
case 16:
asm.SetPart(0, 2, 0.5625f, -0.78125f, false);
asm.SetPart(1, 16, -0.875f, -1.21875f, false);
asm.SetPart(2, 50, 0f, -0.03125f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 84, -0.375f, 1.78125f, false);
asm.SetPart(5, 108, 0.5625f, -1.625f, false);
asm.SetPart(6, 118, -0.9375f, -0.84375f, false);
break;
case 17:
asm.SetPart(0, 2, 0.625f, -0.78125f, false);
asm.SetPart(1, 16, -1.0625f, -1.34375f, false);
asm.SetPart(2, 50, -0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5f, 1f, false);
asm.SetPart(4, 84, -0.375f, 1.71875f, false);
asm.SetPart(5, 109, 0.6875f, -1.53125f, false);
asm.SetPart(6, 112, -1.0625f, -0.75f, false);
break;
case 18:
asm.SetPart(0, 2, 0.75f, -0.71875f, false);
asm.SetPart(1, 28, -1.1875f, -1.625f, false);
asm.SetPart(2, 50, -0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 84, -0.375f, 1.65625f, false);
asm.SetPart(5, 109, 0.8125f, -1.53125f, false);
asm.SetPart(6, 112, -1.1875f, -0.625f, false);
break;
case 19:
asm.SetPart(0, 2, 0.8125f, -0.71875f, false);
asm.SetPart(1, 28, -1.0625f, -1.625f, false);
asm.SetPart(2, 50, -0.0625f, -0.21875f, false);
asm.SetPart(3, 53, -0.5f, 0.8125f, false);
asm.SetPart(4, 84, -0.375f, 1.53125f, false);
asm.SetPart(5, 109, 0.9375f, -1.53125f, false);
asm.SetPart(6, 112, -1.25f, -0.625f, false);
break;
case 20:
asm.SetPart(0, 2, 0.9375f, -0.59375f, false);
asm.SetPart(1, 28, -0.9375f, -1.625f, false);
asm.SetPart(2, 43, 0.0625f, -0.21875f, false);
asm.SetPart(3, 53, -0.5f, 0.8125f, false);
asm.SetPart(4, 85, -0.4375f, 1.5f, false);
asm.SetPart(5, 109, 1.0625f, -1.53125f, false);
asm.SetPart(6, 112, -1.3125f, -0.4375f, false);
break;
case 21:
asm.SetPart(0, 2, 0.9375f, -0.59375f, false);
asm.SetPart(1, 28, -0.8125f, -1.625f, false);
asm.SetPart(2, 43, 0.0625f, -0.21875f, false);
asm.SetPart(3, 53, -0.5f, 0.8125f, false);
asm.SetPart(4, 85, -0.4375f, 1.5f, false);
asm.SetPart(5, 107, 0.9375f, -1.375f, false);
asm.SetPart(6, 112, -1.1875f, -0.5f, false);
break;
case 22:
asm.SetPart(0, 2, 0.875f, -0.65625f, false);
asm.SetPart(1, 28, -0.6875f, -1.625f, false);
asm.SetPart(2, 43, 0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5f, 0.875f, false);
asm.SetPart(4, 85, -0.4375f, 1.5625f, false);
asm.SetPart(5, 107, 0.6875f, -1.3125f, false);
asm.SetPart(6, 112, -1.0625f, -0.625f, false);
break;
case 23:
asm.SetPart(0, 2, 0.75f, -0.71875f, false);
asm.SetPart(1, 28, -0.5625f, -1.625f, false);
asm.SetPart(2, 43, 0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5f, 0.875f, false);
asm.SetPart(4, 85, -0.4375f, 1.5625f, false);
asm.SetPart(5, 107, 0.5625f, -1.25f, false);
asm.SetPart(6, 112, -0.9375f, -0.6875f, false);
break;
case 24:
asm.SetPart(0, 2, 0.5625f, -0.84375f, false);
asm.SetPart(1, 28, -0.4375f, -1.625f, false);
asm.SetPart(2, 44, 0.0625f, -0.21875f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.5f, 1.5625f, false);
asm.SetPart(5, 107, 0.4375f, -1.25f, false);
asm.SetPart(6, 112, -0.75f, -0.8125f, false);
break;
case 25:
asm.SetPart(0, 2, 0.375f, -0.84375f, false);
asm.SetPart(1, 28, -0.3125f, -1.625f, false);
asm.SetPart(2, 44, 0.0625f, -0.21875f, false);
asm.SetPart(3, 53, -0.5f, 0.9375f, false);
asm.SetPart(4, 82, -0.5f, 1.5625f, false);
asm.SetPart(5, 107, 0.3125f, -1.3125f, false);
asm.SetPart(6, 118, -0.5625f, -0.84375f, false);
break;
case 26:
asm.SetPart(0, 2, 0.1875f, -0.84375f, false);
asm.SetPart(1, 28, -0.1875f, -1.625f, false);
asm.SetPart(2, 44, 0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5f, 1f, false);
asm.SetPart(4, 82, -0.5f, 1.625f, false);
asm.SetPart(5, 107, 0.1875f, -1.25f, false);
asm.SetPart(6, 118, -0.4375f, -0.84375f, false);
break;
case 27:
asm.SetPart(0, 2, 0f, -0.78125f, false);
asm.SetPart(1, 28, -0.0625f, -1.625f, false);
asm.SetPart(2, 44, 0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5f, 1.0625f, false);
asm.SetPart(4, 82, -0.5f, 1.6875f, false);
asm.SetPart(5, 107, 0.0625f, -1.25f, false);
asm.SetPart(6, 117, -0.25f, -0.8125f, false);
break;
case 28:
asm.SetPart(0, 2, -0.25f, -0.65625f, false);
asm.SetPart(1, 28, 0.0625f, -1.625f, false);
asm.SetPart(2, 45, -0.0625f, 0f, false);
asm.SetPart(3, 53, -0.5f, 1.1875f, false);
asm.SetPart(4, 83, -0.5f, 1.875f, false);
asm.SetPart(5, 107, -0.0625f, -1.1875f, false);
asm.SetPart(6, 117, -0.0625f, -0.8125f, false);
break;
case 29:
asm.SetPart(0, 0, -0.4375f, -0.59375f, false);
asm.SetPart(1, 28, 0.1875f, -1.625f, false);
asm.SetPart(2, 45, -0.0625f, 0.0625f, false);
asm.SetPart(3, 53, -0.5f, 1.25f, false);
asm.SetPart(4, 83, -0.5f, 1.9375f, false);
asm.SetPart(5, 110, -0.375f, -1.1875f, false);
asm.SetPart(6, 117, 0.125f, -0.6875f, false);
break;
case 30:
asm.SetPart(0, 0, -0.625f, -0.59375f, false);
asm.SetPart(1, 28, 0.3125f, -1.625f, false);
asm.SetPart(2, 45, -0.0625f, 0f, false);
asm.SetPart(3, 53, -0.5f, 1.1875f, false);
asm.SetPart(4, 83, -0.5f, 1.875f, false);
asm.SetPart(5, 110, -0.5625f, -1.1875f, false);
asm.SetPart(6, 117, 0.3125f, -0.75f, false);
break;
case 31:
asm.SetPart(0, 0, -0.8125f, -0.59375f, false);
asm.SetPart(1, 28, 0.4375f, -1.625f, false);
asm.SetPart(2, 45, -0.0625f, -0.0625f, false);
asm.SetPart(3, 53, -0.5f, 1.125f, false);
asm.SetPart(4, 83, -0.5f, 1.8125f, false);
asm.SetPart(5, 110, -0.75f, -1.1875f, false);
asm.SetPart(6, 117, 0.5f, -0.8125f, false);
break;
case 32:
asm.SetPart(0, 0, -1f, -0.53125f, false);
asm.SetPart(1, 28, 0.5625f, -1.625f, false);
asm.SetPart(2, 46, -0.0625f, -0.03125f, false);
asm.SetPart(3, 53, -0.5625f, 1f, false);
asm.SetPart(4, 84, -0.4375f, 1.71875f, false);
asm.SetPart(5, 110, -0.9375f, -1.1875f, false);
asm.SetPart(6, 136, 0.75f, -0.71875f, false);
break;
case 33:
asm.SetPart(0, 1, -1.0625f, -0.53125f, false);
asm.SetPart(1, 17, 0.5625f, -1.4375f, false);
asm.SetPart(2, 46, -0.0625f, -0.09375f, false);
asm.SetPart(3, 53, -0.5625f, 0.9375f, false);
asm.SetPart(4, 84, -0.4375f, 1.65625f, false);
asm.SetPart(5, 110, -1.125f, -1.3125f, false);
asm.SetPart(6, 136, 0.75f, -0.71875f, false);
break;
case 34:
asm.SetPart(0, 1, -1.1875f, -0.53125f, false);
asm.SetPart(1, 17, 0.6875f, -1.4375f, false);
asm.SetPart(2, 46, -0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5625f, 0.875f, false);
asm.SetPart(4, 84, -0.4375f, 1.59375f, false);
asm.SetPart(5, 108, -1.1875f, -1.625f, false);
asm.SetPart(6, 136, 0.8125f, -0.65625f, false);
break;
case 35:
asm.SetPart(0, 1, -1.25f, -0.46875f, false);
asm.SetPart(1, 17, 0.8125f, -1.4375f, false);
asm.SetPart(2, 46, -0.0625f, -0.15625f, false);
asm.SetPart(3, 53, -0.5625f, 0.8125f, false);
asm.SetPart(4, 84, -0.4375f, 1.53125f, false);
asm.SetPart(5, 108, -1.0625f, -1.625f, false);
asm.SetPart(6, 136, 0.875f, -0.59375f, false);
break;
case 36:
asm.SetPart(0, 1, -1.3125f, -0.46875f, false);
asm.SetPart(1, 17, 0.9375f, -1.4375f, false);
asm.SetPart(2, 47, -0.125f, -0.21875f, false);
asm.SetPart(3, 53, -0.625f, 0.75f, false);
asm.SetPart(4, 85, -0.5625f, 1.4375f, false);
asm.SetPart(5, 108, -0.9375f, -1.625f, false);
asm.SetPart(6, 136, 0.9375f, -0.53125f, false);
break;
case 37:
asm.SetPart(0, 0, -1.25f, -0.53125f, false);
asm.SetPart(1, 25, 0.875f, -1.375f, false);
asm.SetPart(2, 47, -0.125f, -0.21875f, false);
asm.SetPart(3, 53, -0.625f, 0.75f, false);
asm.SetPart(4, 85, -0.5625f, 1.4375f, false);
asm.SetPart(5, 108, -0.8125f, -1.625f, false);
asm.SetPart(6, 136, 0.9375f, -0.59375f, false);
break;
case 38:
asm.SetPart(0, 0, -1.1875f, -0.53125f, false);
asm.SetPart(1, 25, 0.75f, -1.375f, false);
asm.SetPart(2, 47, -0.125f, -0.15625f, false);
asm.SetPart(3, 53, -0.625f, 0.8125f, false);
asm.SetPart(4, 85, -0.5625f, 1.5f, false);
asm.SetPart(5, 108, -0.6875f, -1.625f, false);
asm.SetPart(6, 136, 0.9375f, -0.53125f, false);
break;
case 39:
asm.SetPart(0, 0, -1.125f, -0.53125f, false);
asm.SetPart(1, 25, 0.6875f, -1.375f, false);
asm.SetPart(2, 47, -0.125f, -0.09375f, false);
asm.SetPart(3, 53, -0.625f, 0.875f, false);
asm.SetPart(4, 85, -0.5625f, 1.5625f, false);
asm.SetPart(5, 108, -0.5625f, -1.625f, false);
asm.SetPart(6, 136, 0.9375f, -0.53125f, false);
break;
case 40:
asm.SetPart(0, 0, -1f, -0.71875f, false);
asm.SetPart(1, 25, 0.5625f, -1.3125f, false);
asm.SetPart(2, 48, -0.125f, -0.15625f, false);
asm.SetPart(3, 53, -0.5625f, 0.9375f, false);
asm.SetPart(4, 82, -0.5625f, 1.5625f, false);
asm.SetPart(5, 108, -0.4375f, -1.625f, false);
asm.SetPart(6, 136, 0.875f, -0.71875f, false);
break;
case 41:
asm.SetPart(0, 0, -0.875f, -0.71875f, false);
asm.SetPart(1, 25, 0.4375f, -1.25f, false);
asm.SetPart(2, 48, -0.125f, -0.15625f, false);
asm.SetPart(3, 53, -0.5625f, 0.9375f, false);
asm.SetPart(4, 82, -0.5625f, 1.5625f, false);
asm.SetPart(5, 108, -0.3125f, -1.625f, false);
asm.SetPart(6, 136, 0.75f, -0.71875f, false);
break;
case 42:
asm.SetPart(0, 0, -0.6875f, -0.78125f, false);
asm.SetPart(1, 25, 0.3125f, -1.1875f, false);
asm.SetPart(2, 48, -0.125f, -0.09375f, false);
asm.SetPart(3, 53, -0.5625f, 1f, false);
asm.SetPart(4, 82, -0.5625f, 1.625f, false);
asm.SetPart(5, 108, -0.1875f, -1.625f, false);
asm.SetPart(6, 136, 0.5625f, -0.78125f, false);
break;
case 43:
asm.SetPart(0, 0, -0.5f, -0.78125f, false);
asm.SetPart(1, 25, 0.125f, -1.125f, false);
asm.SetPart(2, 48, -0.125f, -0.09375f, false);
asm.SetPart(3, 53, -0.5625f, 1f, false);
asm.SetPart(4, 82, -0.5625f, 1.625f, false);
asm.SetPart(5, 108, -0.0625f, -1.625f, false);
asm.SetPart(6, 117, 0.375f, -0.875f, false);
break;
case 44:
asm.SetPart(0, 8, -0.8125f, -0.6875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, 0.0625f, -0.3125f, false);
asm.SetPart(3, 67, -0.625f, 0.8125f, false);
asm.SetPart(4, 82, -0.625f, 1.4375f, false);
asm.SetPart(5, 109, 0.6875f, -1.46875f, false);
asm.SetPart(6, 116, 0.5f, -0.65625f, false);
break;
case 45:
asm.SetPart(0, 8, -0.8125f, -0.6875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, 0.0625f, -0.3125f, false);
asm.SetPart(3, 67, -0.625f, 0.75f, false);
asm.SetPart(4, 84, -0.5625f, 1.53125f, false);
asm.SetPart(5, 109, 0.6875f, -1.46875f, false);
asm.SetPart(6, 116, 0.5f, -0.71875f, false);
break;
case 46:
asm.SetPart(0, 8, -0.8125f, -0.75f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, 0.0625f, -0.375f, false);
asm.SetPart(3, 67, -0.625f, 0.6875f, false);
asm.SetPart(4, 84, -0.5625f, 1.46875f, false);
asm.SetPart(5, 109, 0.625f, -1.46875f, false);
asm.SetPart(6, 116, 0.5f, -0.78125f, false);
break;
case 47:
asm.SetPart(0, 8, -0.8125f, -0.75f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, 0.0625f, -0.375f, false);
asm.SetPart(3, 67, -0.625f, 0.625f, false);
asm.SetPart(4, 84, -0.5625f, 1.40625f, false);
asm.SetPart(5, 109, 0.625f, -1.46875f, false);
asm.SetPart(6, 116, 0.5f, -0.84375f, false);
break;
case 48:
asm.SetPart(0, 8, -0.8125f, -0.8125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, 0.0625f, -0.4375f, false);
asm.SetPart(3, 67, -0.625f, 0.5f, false);
asm.SetPart(4, 84, -0.5625f, 1.28125f, false);
asm.SetPart(5, 109, 0.5625f, -1.46875f, false);
asm.SetPart(6, 116, 0.4375f, -0.90625f, false);
break;
case 49:
asm.SetPart(0, 9, -0.75f, -0.9375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 32, 0.0625f, -0.46875f, false);
asm.SetPart(3, 68, -0.6875f, 0.40625f, false);
asm.SetPart(4, 89, -0.8125f, 1.125f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 116, 0.375f, -1.03125f, false);
break;
case 50:
asm.SetPart(0, 9, -0.6875f, -0.875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 32, 0.0625f, -0.34375f, false);
asm.SetPart(3, 68, -0.75f, 0.28125f, false);
asm.SetPart(4, 89, -0.9375f, 1f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 116, 0.3125f, -1.09375f, false);
break;
case 51:
asm.SetPart(0, 9, -0.6875f, -0.875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 32, 0.0625f, -0.34375f, false);
asm.SetPart(3, 68, -0.75f, 0.21875f, false);
asm.SetPart(4, 89, -0.9375f, 0.9375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 116, 0.25f, -1.09375f, false);
break;
case 52:
asm.SetPart(0, 9, -0.6875f, -0.875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 32, 0.0625f, -0.40625f, false);
asm.SetPart(3, 68, -0.75f, 0.03125f, false);
asm.SetPart(4, 89, -0.9375f, 0.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 116, 0.1875f, -1.15625f, false);
break;
case 53:
asm.SetPart(0, 9, -0.75f, -0.8125f, false);
asm.SetPart(1, 22, -0.5625f, -1.46875f, false);
asm.SetPart(2, 32, 0.125f, -0.34375f, false);
asm.SetPart(3, 68, -0.625f, 0.09375f, false);
asm.SetPart(4, 89, -0.8125f, 0.8125f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.5625f, -0.8125f, false);
break;
case 54:
asm.SetPart(0, 9, -0.8125f, -0.6875f, false);
asm.SetPart(1, 22, -0.5625f, -1.34375f, false);
asm.SetPart(2, 32, 0.1875f, -0.21875f, false);
asm.SetPart(3, 68, -0.5f, 0.40625f, false);
asm.SetPart(4, 89, -0.625f, 1.125f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.75f, -0.6875f, false);
break;
case 55:
asm.SetPart(0, 11, -0.9375f, -0.34375f, false);
asm.SetPart(1, 22, -0.6875f, -1.21875f, false);
asm.SetPart(2, 37, 0.1875f, -0.34375f, false);
asm.SetPart(3, 65, -0.25f, 0.625f, false);
asm.SetPart(4, 82, -0.375f, 1.1875f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, -0.3125f, false);
break;
case 56:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.625f, 0.3125f, false);
break;
case 57:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.4375f, 0.4375f, false);
break;
case 58:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, 0.3125f, false);
break;
case 59:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, 0.125f, false);
break;
case 60:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.8125f, -0.1875f, false);
break;
case 61:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, -0.4375f, false);
break;
case 62:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.25f, -0.4375f, false);
break;
case 63:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.5625f, -0.25f, false);
break;
case 64:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, -0.0625f, false);
break;
case 65:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, 0.125f, false);
break;
case 66:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 82, -0.0625f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.625f, 0.3125f, false);
break;
case 67:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 82, -0.0625f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.4375f, 0.4375f, false);
break;
case 68:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 82, -0.0625f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, 0.3125f, false);
break;
case 69:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.84375f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 82, -0.0625f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, 0.125f, false);
break;
case 70:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.9375f, false);
asm.SetPart(4, 82, 0f, 1.5f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.8125f, -0.1875f, false);
break;
case 71:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.9375f, false);
asm.SetPart(4, 82, 0f, 1.5f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, -0.4375f, false);
break;
case 72:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.9375f, false);
asm.SetPart(4, 82, 0f, 1.5f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.25f, -0.4375f, false);
break;
case 73:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.125f, 1f, false);
asm.SetPart(4, 82, 0f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.5625f, -0.25f, false);
break;
case 74:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.125f, 1f, false);
asm.SetPart(4, 82, 0f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, -0.0625f, false);
break;
case 75:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.0625f, -0.71875f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.125f, 1f, false);
asm.SetPart(4, 82, 0f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, 0.125f, false);
break;
case 76:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.1875f, 1f, false);
asm.SetPart(4, 82, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.625f, 0.3125f, false);
break;
case 77:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.1875f, 1f, false);
asm.SetPart(4, 82, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.4375f, 0.4375f, false);
break;
case 78:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.1875f, 1f, false);
asm.SetPart(4, 82, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, 0.3125f, false);
break;
case 79:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 82, 0.125f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, 0.125f, false);
break;
case 80:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 82, 0.125f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.8125f, -0.1875f, false);
break;
case 81:
asm.SetPart(0, 11, -0.9375f, 0.21875f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 82, 0.125f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, -0.4375f, false);
break;
case 82:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 82, 0.125f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.25f, -0.4375f, false);
break;
case 83:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 82, 0.125f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.5625f, -0.25f, false);
break;
case 84:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.3125f, 1.0625f, false);
asm.SetPart(4, 83, 0.1875f, 1.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, -0.0625f, false);
break;
case 85:
asm.SetPart(0, 11, -0.9375f, 0.15625f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.3125f, 1.0625f, false);
asm.SetPart(4, 83, 0.1875f, 1.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, 0.125f, false);
break;
case 86:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.3125f, 1.0625f, false);
asm.SetPart(4, 83, 0.1875f, 1.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.625f, 0.3125f, false);
break;
case 87:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.375f, 1.0625f, false);
asm.SetPart(4, 83, 0.25f, 1.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.4375f, 0.4375f, false);
break;
case 88:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.375f, 1.0625f, false);
asm.SetPart(4, 83, 0.25f, 1.75f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, 0.3125f, false);
break;
case 89:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.15625f, false);
asm.SetPart(3, 65, 0.375f, 1.125f, false);
asm.SetPart(4, 83, 0.25f, 1.8125f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, 0.125f, false);
break;
case 90:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.25f, -0.53125f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.375f, 1.0625f, false);
asm.SetPart(4, 84, 0.375f, 1.78125f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.8125f, -0.1875f, false);
break;
case 91:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.375f, 1f, false);
asm.SetPart(4, 84, 0.375f, 1.71875f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, -0.4375f, false);
break;
case 92:
asm.SetPart(0, 11, -1f, 0.09375f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.3125f, 1f, false);
asm.SetPart(4, 84, 0.3125f, 1.71875f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.25f, -0.4375f, false);
break;
case 93:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 1f, false);
asm.SetPart(4, 84, 0.25f, 1.71875f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.5625f, -0.25f, false);
break;
case 94:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.1875f, -0.59375f, false);
asm.SetPart(2, 37, 0.25f, -0.21875f, false);
asm.SetPart(3, 65, 0.25f, 0.9375f, false);
asm.SetPart(4, 84, 0.25f, 1.65625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, -0.0625f, false);
break;
case 95:
asm.SetPart(0, 11, -1f, 0.03125f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.25f, 0.9375f, false);
asm.SetPart(4, 84, 0.25f, 1.65625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, 0.125f, false);
break;
case 96:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.1875f, 0.9375f, false);
asm.SetPart(4, 84, 0.1875f, 1.65625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.625f, 0.3125f, false);
break;
case 97:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.1875f, 0.9375f, false);
asm.SetPart(4, 85, 0.125f, 1.625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.4375f, 0.4375f, false);
break;
case 98:
asm.SetPart(0, 11, -1.0625f, -0.03125f, false);
asm.SetPart(1, 22, -1.125f, -0.65625f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.875f, false);
asm.SetPart(4, 85, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.1875f, 0.3125f, false);
break;
case 99:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.71875f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.875f, false);
asm.SetPart(4, 85, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, 0.125f, false);
break;
case 100:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.71875f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.125f, 0.875f, false);
asm.SetPart(4, 85, 0.0625f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.8125f, -0.1875f, false);
break;
case 101:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.71875f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 85, 0f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 0.875f, -0.4375f, false);
break;
case 102:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.71875f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 85, 0f, 1.5625f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.25f, -0.4375f, false);
break;
case 103:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0.0625f, 0.875f, false);
asm.SetPart(4, 82, -0.0625f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.5625f, -0.25f, false);
break;
case 104:
asm.SetPart(0, 11, -1.0625f, -0.09375f, false);
asm.SetPart(1, 22, -1.0625f, -0.78125f, false);
asm.SetPart(2, 37, 0.25f, -0.28125f, false);
asm.SetPart(3, 65, 0f, 0.875f, false);
asm.SetPart(4, 82, -0.125f, 1.4375f, false);
asm.SetPart(5, 108, 0.5f, -1.5625f, false);
asm.SetPart(6, 111, 1.6875f, -0.0625f, false);
break;
case 105:
asm.SetPart(0, 0, -1.0625f, -0.34375f, false);
asm.SetPart(1, 22, -0.9375f, -1.09375f, false);
asm.SetPart(2, 38, -0.125f, -0.3125f, false);
asm.SetPart(3, 66, -1f, 0.65625f, false);
asm.SetPart(4, 90, -0.1875f, 1.3125f, false);
asm.SetPart(5, 109, 0.375f, -1.46875f, false);
asm.SetPart(6, 111, 0.75f, 0.25f, false);
break;
case 106:
asm.SetPart(0, 0, -1f, -0.53125f, false);
asm.SetPart(1, 22, -0.875f, -1.21875f, false);
asm.SetPart(2, 38, -0.25f, -0.3125f, false);
asm.SetPart(3, 66, -1.25f, 0.65625f, false);
asm.SetPart(4, 90, -0.4375f, 1.25f, false);
asm.SetPart(5, 109, 0.375f, -1.46875f, false);
asm.SetPart(6, 111, -0.0625f, 0.25f, false);
break;
case 107:
asm.SetPart(0, 0, -0.75f, -0.84375f, false);
asm.SetPart(1, 22, -0.75f, -1.40625f, false);
asm.SetPart(2, 38, -0.4375f, -0.375f, false);
asm.SetPart(3, 66, -1.5f, 0.53125f, false);
asm.SetPart(4, 90, -0.75f, 1.125f, false);
asm.SetPart(5, 109, 0.375f, -1.46875f, false);
asm.SetPart(6, 111, -0.9375f, 0.1875f, false);
break;
case 108:
asm.SetPart(0, 0, -0.3125f, -1.09375f, false);
asm.SetPart(1, 17, -0.6875f, -1.4375f, false);
asm.SetPart(2, 38, -0.5f, -0.375f, false);
asm.SetPart(3, 66, -1.5625f, 0.53125f, false);
asm.SetPart(4, 90, -0.8125f, 1.125f, false);
asm.SetPart(5, 107, 0.375f, -1.5f, false);
asm.SetPart(6, 111, -1.6875f, 0.1875f, false);
break;
case 109:
asm.SetPart(0, 0, 0.1875f, -1.03125f, false);
asm.SetPart(1, 17, -0.625f, -1.4375f, false);
asm.SetPart(2, 38, -0.5625f, -0.3125f, false);
asm.SetPart(3, 66, -1.625f, 0.59375f, false);
asm.SetPart(4, 90, -0.875f, 1.1875f, false);
asm.SetPart(5, 107, 0.3125f, -1.4375f, false);
asm.SetPart(6, 111, -2.125f, 0.25f, false);
break;
case 110:
asm.SetPart(0, 2, 0.375f, -0.96875f, false);
asm.SetPart(1, 28, -0.375f, -1.5625f, false);
asm.SetPart(2, 38, -0.75f, -0.375f, false);
asm.SetPart(3, 66, -1.8125f, 0.53125f, false);
asm.SetPart(4, 90, -1.09375f, 1.125f, false);
asm.SetPart(5, 107, 0.0625f, -1.3125f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 111:
asm.SetPart(0, 2, 0.4375f, -0.84375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 38, -0.875f, -0.375f, false);
asm.SetPart(3, 66, -1.9375f, 0.53125f, false);
asm.SetPart(4, 90, -1.1875f, 1.1875f, false);
asm.SetPart(5, 107, -0.1875f, -1.1875f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 112:
asm.SetPart(0, 2, 0.5625f, -0.65625f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 36, -0.875f, -0.25f, false);
asm.SetPart(3, 66, -1.9375f, 0.28125f, false);
asm.SetPart(4, 84, -1.6875f, 1.03125f, false);
asm.SetPart(5, 108, -0.3125f, -1.25f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 113:
asm.SetPart(0, 2, 0.5625f, -0.65625f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 36, -0.875f, -0.25f, false);
asm.SetPart(3, 66, -1.9375f, 0.28125f, false);
asm.SetPart(4, 84, -1.6875f, 1.03125f, false);
asm.SetPart(5, 108, -0.3125f, -1.25f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 114:
asm.SetPart(0, 2, 0.5625f, -0.53125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 36, -0.875f, -0.375f, false);
asm.SetPart(3, 66, -1.9375f, 0.03125f, false);
asm.SetPart(4, 84, -1.875f, 0.78125f, false);
asm.SetPart(5, 108, -0.625f, -1.25f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 115:
asm.SetPart(0, 2, 0.5625f, -0.53125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 36, -0.875f, -0.375f, false);
asm.SetPart(3, 66, -1.9375f, 0.03125f, false);
asm.SetPart(4, 84, -1.875f, 0.78125f, false);
asm.SetPart(5, 108, -0.75f, -1.25f, false);
asm.SetPart(6, -1, 0f, 0f, false);
break;
case 116:
asm.SetPart(0, 2, 0.5625f, -0.53125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.5625f, -0.5625f, false);
asm.SetPart(3, 68, -1.6875f, -0.03125f, false);
asm.SetPart(4, 89, -1.75f, 0.75f, false);
asm.SetPart(5, 110, -1.0625f, -1.1875f, false);
asm.SetPart(6, 111, -2.1875f, -0.4375f, false);
break;
case 117:
asm.SetPart(0, 2, 0.5625f, -0.53125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.5625f, -0.5625f, false);
asm.SetPart(3, 68, -1.6875f, -0.03125f, false);
asm.SetPart(4, 89, -1.75f, 0.75f, false);
asm.SetPart(5, 110, -1.125f, -1.1875f, false);
asm.SetPart(6, 111, -1.9375f, -0.6875f, false);
break;
case 118:
asm.SetPart(0, 2, 0.375f, -0.84375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.5f, -0.5625f, false);
asm.SetPart(3, 68, -1.375f, -0.03125f, false);
asm.SetPart(4, 89, -1.5f, 0.75f, false);
asm.SetPart(5, 110, -1.0625f, -1.375f, false);
asm.SetPart(6, 111, -1.75f, -0.875f, false);
break;
case 119:
asm.SetPart(0, 2, 0.375f, -0.84375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.5f, -0.5625f, false);
asm.SetPart(3, 68, -1.375f, -0.03125f, false);
asm.SetPart(4, 89, -1.5f, 0.75f, false);
asm.SetPart(5, 110, -1f, -1.375f, false);
asm.SetPart(6, 111, -1.6875f, -1f, false);
break;
case 120:
asm.SetPart(0, 2, 0.25f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.4375f, -0.5625f, false);
asm.SetPart(3, 68, -1.1875f, 0.03125f, false);
asm.SetPart(4, 89, -1.3125f, 0.8125f, false);
asm.SetPart(5, 108, -0.6875f, -1.5625f, false);
asm.SetPart(6, 111, -1.25f, -1.0625f, false);
break;
case 121:
asm.SetPart(0, 2, 0.25f, -0.96875f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.4375f, -0.5625f, false);
asm.SetPart(3, 68, -1.1875f, 0.03125f, false);
asm.SetPart(4, 89, -1.3125f, 0.8125f, false);
asm.SetPart(5, 108, -0.6875f, -1.5625f, false);
asm.SetPart(6, 111, -1f, -1.1875f, false);
break;
case 122:
asm.SetPart(0, 2, 0f, -1.03125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.3125f, -0.5625f, false);
asm.SetPart(3, 68, -0.9375f, 0.15625f, false);
asm.SetPart(4, 89, -1.0625f, 0.875f, false);
asm.SetPart(5, 108, -0.5625f, -1.5f, false);
asm.SetPart(6, 111, -0.625f, -1.3125f, false);
break;
case 123:
asm.SetPart(0, 2, 0f, -1.03125f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.3125f, -0.5625f, false);
asm.SetPart(3, 68, -0.9375f, 0.15625f, false);
asm.SetPart(4, 89, -1.0625f, 0.875f, false);
asm.SetPart(5, 108, -0.5625f, -1.5f, false);
asm.SetPart(6, 111, -0.375f, -1.3125f, false);
break;
case 124:
asm.SetPart(0, 2, -0.1875f, -1.09375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.25f, -0.4375f, false);
asm.SetPart(3, 67, -0.875f, 0.4375f, false);
asm.SetPart(4, 89, -1f, 1.125f, false);
asm.SetPart(5, 108, -0.375f, -1.5f, false);
asm.SetPart(6, 111, -0.25f, -1.375f, false);
break;
case 125:
asm.SetPart(0, 2, -0.1875f, -1.09375f, false);
asm.SetPart(1, 28, -0.3125f, -1.5625f, false);
asm.SetPart(2, 39, -0.25f, -0.4375f, false);
asm.SetPart(3, 67, -0.875f, 0.4375f, false);
asm.SetPart(4, 89, -1f, 1.125f, false);
asm.SetPart(5, 108, -0.375f, -1.5f, false);
asm.SetPart(6, 111, 0.125f, -1.25f, false);
break;
}
}
}