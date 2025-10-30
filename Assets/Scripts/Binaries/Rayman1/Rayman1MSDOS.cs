using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Rayman1MSDOS
{
    public const string msdos = "Rayman 1 PC (MS-DOS)";

    public static List<string> SetAnimationsForObject(int _object)
    {
        Type animations;

        switch ((DesignObjects)_object)
        {
            case DesignObjects.Rayman:
                animations = typeof(RayAnimations);
                break;
            case DesignObjects.Items:
                animations = typeof(ItemsAnimations);
                break;
            case DesignObjects.MiniRayman:
                animations = typeof(MiniRayAnimations);
                break;
            case DesignObjects.Items2:
                animations = typeof(Items2Animations);
                break;
            case DesignObjects.Font:
                animations = typeof(FontAnimations);
                break;
            case DesignObjects.BigFont:
                animations = typeof(BigFontAnimations);
                break;
            case DesignObjects.ContinueClock:
                animations = typeof(ContinueClockAnimations);
                break;
            case DesignObjects.EmptyObject:
                animations = typeof(EmptyAnimations);
                break;
            case DesignObjects.AnotherObject:
                animations = typeof(AnotherAnimations);
                break;
            case DesignObjects.AnotherObject2:
                animations = typeof(AnotherAnimations2);
                break;
            default:
                DebugHelper.Log(((DesignObjects)_object).ToString() + " Could not be found.", DebugHelper.Severity.error);
                Debug.LogError(((DesignObjects)_object).ToString() + " Could not be found.");
                return null;
        }

        List<string> anims = new List<string>();

        foreach (object obj in Enum.GetValues(animations))
        {
            anims.Add(obj.ToString());
        }

        return anims;
    }

    public enum DesignObjects
    {
        Rayman,
        Items,
        MiniRayman,
        Items2,
        Font,
        BigFont,
        ContinueClock,
        EmptyObject,
        EmptyObject2,
        AnotherObject,
        AnotherObject2,
    }

    public enum RayAnimations
    {
        RayFistChargeStart,
        RayFistCharge,
        RayFistThrowStart,
        RayFistThrowEnd,
        RayLeaningForward,
        RayGrimace,
        RayHandstandStart,
        RayHandstandWalking,
        RayHandstandEnd,
        RayHandstandIdle,
        RayHandstandEnd2,
        RayHandstandAttack,
        RayHandstandScared,
        RayHandstandAttack2,
        RayFlyingStart,
        RayFlying,
        RayFlyingEnd,
        RayJumping,
        RayJumpingToFalling,
        RayFalling,
        RayLanding,
        RayTakingDamage,
        RayIdleWaiting,
        RayIdle,
        RayStrangeIdle,
        RayTakingDamageStart,
        RayDuckingStart,
        RayDuckingFastStart,
        RayStunnedDamage,
        RayWalking,
        RayClimbingVineUp,
        RayClimbingVineDown,
        RayClimbingVineIdleStart,
        RayClimbingVineIdle,
        RayClimbingVineFistCharge,
        RayClimbingVineFistThrow,
        RayMagicianDepositStart,
        RayMagicianDeposit,
        RayMagicianDepositEnd,
        RayHangingIdle,
        RayHangingIdleWaitingStart,
        RayHangingIdleWaiting,
        RayHangingIdleWaitingEnd,
        RayHangingIdleStart,
        RayStrangeTakingDamageStart,
        RayStrangeTakingDamage,
        RayHangingToLanding,
        RayPlantingSeedStart,
        RayPlantingSeedEnd,
        RayFallingFistThrow,
        RayHandstandBounce,
        RayHandstandDiveStart,
        RayHandstandDive,
        RayHandstandDiveEnd,
        RayFlyingInfStart,
        RayFlyingInf,
        RayFlyingInfEnd,
        RayRunning,
        RaySlingFullCircle,
        RayStrangeBalls,
        RayTurnAroundStart,
        RayTurnAroundEnd,
        RayHangingFistChargeStart,
        RayHangingFistCharge,
        RayHangingFistThrow,
        RayPowerReceiveStart,
        RayPowerReceive,
        RayPowerReceiveEnd,
        RayBrakingStart,
        RayBraking,
        RayBrakingEnd,
        RayWalkingEnd,
        RayRunningToJumpingStart,
        RayRunningToJumping,
        RaySpinJump,
        RaySpinJumpEnd,
        RayCrawlStart,
        RayCrawl,
        RayCrawlBackwardsSlowStart,
        RayGlidingForward,
        RayGlidingBackwards,
        RayGlidingForwardStart,
        RayCrawlSlowStart,
        RayCrawlBackwards,
        RayCrawlBackwardsSlowEnd,
        RayHandstandKick,
        RayHandstandNoFeetIdle,
        RayHandstandNoFeetWalking,
        RayFeet,
        RayDeathDrowning,
        RayNoLivesStart,
        RayNoLives,
        RayNoLivesEnd,
        RayContinueIdle,
        RayNoLivesCancel,
        RayContinueAcceptStart,
        RayContinueAccept,
        RayContinueAcceptEnd,
        RayLevelFinish,
        RayLeaningBackwards,
        RayCrawlTurnAround,
        RayGlidingForwardResetFeetStart,
        RayGlidingForwardResetFeet,
        RayGlidingForwardFastStart,
        RayGlidingForwardEnd,
        RayDeathFalling,
        RayDeathSpikes,
        RayDeathContinues,
        Empty1,
        Empty2,
        RayWorried,
        RayLevelFinishEnd,
        RaySpawnParticles,
        RaySpawnParticles2,
        RaySpawnParticles3,
        RayIdleWaiting2,
        RayStrangeDance,
        RayCrawlingDamage,
        RayLevelSpawn,
        RayHunterCutscene,
        RayHunterCutscene2,
        RayPondering1,
        RayPondering2,
        RayHairWave,
        RayBody,
        RayCarCutscenePart1,
        RayCarCutscenePart2,
        RayCarCutscenePart3,
        RayDeathContinue,
        RayStrangeWalk,
        RayVictory,
        RayIdleWaiting3,
        RayIdleWaiting4,
        RayIdleWaiting5,
        RayIdleWaiting6,
    }

    public enum ItemsAnimations
    {
        ItemFistPunch,
        ItemSmallP1Dot,
        ItemSmallP2Dot,
        itemPhotographerIdle,
        itemPhotographerPicture,
        ItemMagicianAppear,
        ItemMagicianIdle,
        ItemMagicianDisappear,
        Empty1,
        ItemMagicianIdleWait,
        ItemMagicianShowBonus,
        ItemMagicianTakeTings,
        ItemMagicianIdleWait2,
        ItemStrangeSmallP2Dot,
        ItemStrangeSmallP2Dot2,
        ItemStrangeSmallP2Dot3,
        ItemStrangeSmallP2Dot4,
        ItemStrangeSmallP2Dot5,
        ItemStrangeSmallP2Dot6,
        ItemStrangeSmallP2Dot7,
        ItemStrangeSmallP2Dot8,
        ItemStrangeSmallP2Dot9,
        ItemStrangeSmallP2Dot10,
        ItemStrangeSmallP2Dot11,
        ItemStrangeSmallP2Dot12,
        ItemMagicianShowTingRequirement,
    }

    public enum MiniRayAnimations
    {

    }

    public enum Items2Animations
    {
        Item2Power,
        Item2Fist,
        Item2FistTurnAround,
        Item2FistCharged1,
        Item2FistCharged1TurnAround,
        Item2FistCharged2,
        Item2FistCharged2TurnAround,
        Item2GoldFistPower,
        Item2GoldFist,
        Item2GoldFistTurnAround,
        Item2GoldFistCharged1,
        Item2GoldFistCharged1TurnAround,
        Item2GoldFistCharged2,
        Item2GoldFistCharged2TurnAround,
        Item2FistChargePower,
        Item2GoldFistSparklw,
        Item2Strange,
        Item2Sparkles,
        Item2Sparkles2,
        Item2InfFlyingPower,
        Item2ExtraLifePower,
        Item2AppearFlash,
        Item2InfFlyingPowerAppear,
        Item2SmallSparkle,
        Item2Ting,
        Item2Ting2,
        Item2Ting3,
        Item2Ting4,
        Item2Ting5,
        Item2Ting6,
        Item2Ting7,
        Item2Ting8,
        Item2MrDarkBadge,
        Item2GoldFistMrDark,
        Item2TeleportStationStart,
        Item2TeleportStation,
        Item2TeleportStationEnd,
        Item2Clock,
        Item2LevelBadgeEmpty,
        Item2LevelBadge1Cage,
        Item2LevelBadge2Cage,
        Item2LevelBadge3Cage,
        Item2LevelBadge4Cage,
        Item2LevelBadge5Cage,
        Item2LevelBadge6Cage,
        Item2LevelBadgeAppear,
        Item2LevelBadge1CageAppear,
        Item2LevelBadge2CageAppear,
        Item2LevelBadge3CageAppear,
        Item2LevelBadge4CageAppear,
        Item2LevelBadge5CageAppear,
        Item2LevelBadge6CageAppear,
        Item2Lever,
        Item2LeverSwitchedFaulty,
        Item2LeverSwitchedLeft,
        Item2LeverSwitchedRight,
        Item2MiniFairy,
        Item2SaveBadge,
        Item2SmallSparkle2,
        Item2SmallSparkle3,
        Item2SmallSparkles,
        Item2SmallSparkles2,
    }

    public enum FontAnimations
    {
        SomeAnimation1,
    }

    public enum BigFontAnimations
    {
        SomeAnimation1,
    }

    public enum ContinueClockAnimations
    {
        ClockWakeUpFast,
        ClockWakeUp,
        ClockRing,
        ClockRingDance,
        ClockSleeping,
    }

    public enum EmptyAnimations
    {
        Empty1,
    }

    public enum AnotherAnimations
    {
        SomeAnimation1,
        SomeAnimation2,
        SomeAnimation3,
    }

    public enum AnotherAnimations2
    {
        SomeAnimation1,
        SomeAnimation2,
        SomeAnimation3,
    }
}
