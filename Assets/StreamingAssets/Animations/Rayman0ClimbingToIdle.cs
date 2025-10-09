public sealed class Rayman0ClimbingToIdle
{
#region Singleton
private static Rayman0ClimbingToIdle _instance;
private static object _lock = new object();

private Rayman0ClimbingToIdle() { }

public static Rayman0ClimbingToIdle Instance
{
get
{
if (null == _instance)
{
lock (_lock)
{
if (null == _instance)
{
_instance = new Rayman0ClimbingToIdle();
}
}
}
return _instance;
}
}
#endregion

public AnimationStateMachine asm;

public void Rayman0ClimbingToIdle(int subFrame)
{
switch(subFrame)
{
case 0:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.9375f, false);
asm.SetPart(4, 96, 0.625f, 1.3125f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 1:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 1f, false);
asm.SetPart(4, 96, 0.625f, 1.375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 2:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.15625f, false);
asm.SetPart(3, 79, 0.5625f, 1f, false);
asm.SetPart(4, 96, 0.625f, 1.375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 3:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.15625f, false);
asm.SetPart(3, 79, 0.5625f, 0.9375f, false);
asm.SetPart(4, 96, 0.625f, 1.3125f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 4:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 5:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 6:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 7:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 8:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 134, -0.375f, 1.0625f, false);
break;
case 9:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 79, 0.5625f, 0.875f, false);
asm.SetPart(4, 96, 0.625f, 1.25f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 112, -0.875f, 0.875f, false);
break;
case 10:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.21875f, false);
asm.SetPart(3, 52, 0.1875f, 0.90625f, false);
asm.SetPart(4, 84, 0.4375f, 1.65625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 112, -0.8125f, 0.3125f, false);
break;
case 11:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 52, 0.1875f, 0.84375f, false);
asm.SetPart(4, 84, 0.4375f, 1.59375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 112, -0.8125f, -0.0625f, false);
break;
case 12:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 55, 0.125f, 0.84375f, false);
asm.SetPart(4, 84, 0.375f, 1.59375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 112, -0.6875f, -0.4375f, false);
break;
case 13:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 55, 0.125f, 0.78125f, false);
asm.SetPart(4, 85, 0.25f, 1.5f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 112, -0.5f, -0.75f, false);
break;
case 14:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.78125f, false);
asm.SetPart(4, 82, 0.1875f, 1.5f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.0625f, -1.1875f, false);
break;
case 15:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.71875f, false);
asm.SetPart(4, 82, 0.1875f, 1.4375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.3125f, -1.25f, false);
break;
case 16:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.71875f, false);
asm.SetPart(4, 82, 0.1875f, 1.4375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.5625f, -1.25f, false);
break;
case 17:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.40625f, false);
asm.SetPart(3, 60, 0.125f, 0.71875f, false);
asm.SetPart(4, 82, 0.1875f, 1.4375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.6875f, -1.1875f, false);
break;
case 18:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.40625f, false);
asm.SetPart(3, 60, 0.125f, 0.71875f, false);
asm.SetPart(4, 82, 0.1875f, 1.4375f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.75f, -1.1875f, false);
break;
case 19:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.40625f, false);
asm.SetPart(3, 60, 0.125f, 0.78125f, false);
asm.SetPart(4, 82, 0.1875f, 1.5f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.625f, -1.25f, false);
break;
case 20:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.78125f, false);
asm.SetPart(4, 82, 0.1875f, 1.5f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.5f, -1.375f, false);
break;
case 21:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.25f, -1.375f, false);
break;
case 22:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.34375f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.0625f, -1.375f, false);
break;
case 23:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.0625f, -1.3125f, false);
break;
case 24:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.25f, -1.25f, false);
break;
case 25:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.375f, -1.0625f, false);
break;
case 26:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.25f, -1.1875f, false);
break;
case 27:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, -0.0625f, -1.3125f, false);
break;
case 28:
asm.SetPart(0, 15, 0.1875f, 0.90625f, false);
asm.SetPart(1, 26, 0.5f, -1.25f, false);
asm.SetPart(2, 42, 0.4375f, -0.28125f, false);
asm.SetPart(3, 60, 0.125f, 0.84375f, false);
asm.SetPart(4, 82, 0.1875f, 1.5625f, false);
asm.SetPart(5, 101, -0.3125f, -1.34375f, false);
asm.SetPart(6, 122, 0.1875f, -1.3125f, false);
break;
}
}
}