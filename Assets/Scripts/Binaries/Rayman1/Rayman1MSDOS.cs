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
        Type animations = null;

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
            case DesignObjects.ContinueClock:
                animations = typeof(ContinueClockAnimations);
                break;
            case DesignObjects.HunterDreamForest:
                animations = typeof(HunterDreamForestAnimations);
                break;
            case DesignObjects.PoofEffects:
                animations = typeof(PoofAnimations);
                break;
            case DesignObjects.Electoons:
                animations = typeof(ElectoonAnimations);
                break;
            case DesignObjects.Livingstones:
                animations = typeof(LivingStoneAnimations);
                break;
            case DesignObjects.Bzzit:
                animations = typeof(BzzitAnimations);
                break;
            case DesignObjects.Moskito:
                animations = typeof(MoskitoAnimations);
                break;
            case DesignObjects.Plants:
                animations = typeof(PlantAnimations);
                break;
            case DesignObjects.Piranha:
                animations = typeof(PiranhaAnimations);
                break;
            case DesignObjects.WaterSplash:
                animations = typeof(WaterSplashAnimation);
                break;
            case DesignObjects.LipMonster:
                animations = typeof(LipMonsterAnimations);
                break;
            case DesignObjects.TarRayzan:
                animations = typeof(TarRayzanAnimations);
                break;
            case DesignObjects.BlueAntitoons:
                animations = typeof(BlueAntitoonAnimations);
                break;
            case DesignObjects.RedAntitoons:
                animations = typeof(RedAntitoonAnimations);
                break;
            case DesignObjects.RaymanOnBzzit:
                animations = typeof(RaymanOnBzzitAnimations);
                break;
            case DesignObjects.RisingWater:
                animations = typeof(RisingWaterAnimations);
                break;
            case DesignObjects.WingedRing:
                animations = typeof(WingedRingAnimations);
                break;
            case DesignObjects.BetillaJungle:
                animations = typeof(BetillaAnimations);
                break;
            case DesignObjects.CloudSplash:
                animations = typeof(CloudSplashAnimations);
                break;
            case DesignObjects.SpikyPlant:
                animations = typeof(SpikyPlantAnimations);
                break;
            case DesignObjects.CageUnlock:
                animations = typeof(CageUnlockAnimations);
                break;
            case DesignObjects.StrangeGate:
                animations = typeof(StrangeGateAnimations);
                break;
            case DesignObjects.Breakout:
                animations = typeof(BreakoutAnimations);
                break;
            case DesignObjects.HunterBandLand:
                animations = typeof(HunterBandLandAnimations);
                break;
            case DesignObjects.SomeObject3:
                animations = typeof(SomeAnimations3);
                break;
            case DesignObjects.SomeObject4:
                animations = typeof(SomeAnimations4);
                break;
            case DesignObjects.SomeObject5:
                animations = typeof(SomeAnimations5);
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

    //All missing indexes are either Design objects with no animations, or Design objects that are not animated.
    //I saw no point in adding them to the list if they don't.. animate
    public enum DesignObjects
    {
        Rayman,
        Items,
        MiniRayman,
        Items2,
        //Fonts at 4 and 5 don't have animations
        ContinueClock = 6,
        //7 and 8 are parallax
        HunterDreamForest = 9,
        PoofEffects,
        Electoons,
        Livingstones,
        Bzzit,
        Moskito,
        Plants,
        Piranha,
        WaterSplash,
        LipMonster,
        TarRayzan,
        BlueAntitoons,
        RedAntitoons,
        RaymanOnBzzit,
        RisingWater,
        WingedRing,
        BetillaJungle,
        CloudSplash,
        SpikyPlant,
        CageUnlock,
        StrangeGate,
        Breakout,
        //31 is parallax
        HunterBandLand = 32,
        SomeObject3,
        SomeObject4,
        SomeObject5,
    }

    private enum RayAnimations
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

    private enum ItemsAnimations
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

    private enum MiniRayAnimations
    {

    }

    private enum Items2Animations
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

    private enum ContinueClockAnimations
    {
        ClockWakeUpFast,
        ClockWakeUp,
        ClockRing,
        ClockRingDance,
        ClockSleeping,
    }

    private enum HunterDreamForestAnimations
    {
        HunterIdle,
        HunterHit,
        HunterTakeAim,
        HunterFire,
        BulletIdle,
        BulletHammerStart,
        BulletHammer,
    }

    private enum PoofAnimations
    {
        Explosion,
        DustCloud,
        DustExplosion,
    }

    private enum ElectoonAnimations
    {
        ElectoonFalling,
        ElectoonIdle,
        ElectoonSleepy,
        ElectoonFlying,
        ElectoonConfused,
        ElectoonJumping,
        ElectoonLove,
        ElectoonWalking,
        ElectoonLeaveStart,
        ElectoonFalling2,
        ElectoonLeave,
        MusicEndSign,
        EndSign,
        ReverseEndSign,
        MrStoneLaserShot,
        ElectoonCageIdle,
        ElectoonCageOpenBottom,
        ElectoonCageHit,
        ElectoonCageOpenTop,
        ElectoonCage,
        ElectoonCageBottom,
        ElectoonCageHitFast,
        ReturnSign,
    }

    private enum LivingStoneAnimations
    {
        BigLivingStoneRunningTowardsStart,
        BigLivingStoneIdle,
        BigLivingStoneRunningTowards,
        BigLivingStoneWalking,
        BigLivingStoneHit,
        Plum,
        BigLivingStoneWalkingPlum,
        BigLivingStonePlumLanding,
        BigLivingStonePlumHit,
        BigLivingStoneTurnAround,
        SmallLivingStoneIdleScare,
        SmallLivingStoneIdle,
        SmallLivingStoneWalkingAngry,
        SmallLivingStoneWalking,
        SmallLivingStoneHit,
        SmallLivingStoneTurnAround,
        SmallLivingStoneAttack,
        SmallLivingStonePlumBounce,
        SmallLivingStoneDuckStart,
        SmallLivingStoneDuck,
        SmallLivingStoneDuckEnd,
    }

    private enum BzzitAnimations
    {
        SpikyPlant,
        BzzitBlink,
        BzzitIdleSide,
        BzzitMediumSpikySting,
        BzzitFeetHandSting,
        BzzitSting,
        BzzitStrangeIdle,
        BzzitHit,
        BzzitSillyEnd,
        BzzitIdleFront,
        BzzitSpiky,
        BzzitSpikyDrop,
        BzzitDizzy,
        BzzitZoom,
        BzzitStingEnd,
        BzzitStingStart,
        BzzitStrange,
        BzzitSilly,
        MediumSpikyPlant,
        BzzitMediumSpiky,
        BzzitBigSpiky,
        BzzitDefeatedFall,
        BzzitDefeated,
        BzzitDefeatedIdle,
    }

    private enum MoskitoAnimations
    {
        SpikyPlant,
        MoskitoBlink,
        MoskitoIdleSide,
        MoskitoMediumSpikySting,
        MoskitoFeetHandSting,
        MoskitoSting,
        MoskitoStrangeIdle,
        MoskitoHit,
        MoskitoSillyEnd,
        MoskitoIdleFront,
        MoskitoSpiky,
        MoskitoSpikyDrop,
        MoskitoDizzy,
        MoskitoZoom,
        MoskitoStingEnd,
        MoskitoStingStart,
        MoskitoStrange,
        MoskitoSilly,
        MediumSpikyPlant,
        MoskitoMediumSpiky,
        MoskitoBigSpiky,
        MoskitoDefeatedFall,
        MoskitoDefeated,
        MoskitoDefeatedIdle,
    }

    private enum PlantAnimations
    {
        PlumSwingIdle,
        PlumSwingFull,
        PlumSwingRight,
        PlumSwingLeft,
        SpikyPlantSwingRight,
        SpikyPlantSwingLeft,
        PlumSwing360,
        FloatingPlant,
        BungiePlantIdle,
        BungiePlantBounce,
        FlowerDance,
        MushroomDance,
        MushroomStack,
        Plum,
        PlumWaterEnter,
        PlumWaterIdle,
        Butterfly1,
        Butterfly2,
        Butterfly3,
        Butterfly4,
        Butterfly5,
        Butterfly6,
        Butterfly7,
        Butterfly8,
        SeedPlantGrow,
        SeedPlantIdle,
        TinyFloatingPlant,
    }

    private enum PiranhaAnimations
    {
        PiranhaDead,
        Piranha,
        PiranhaHit,
    }

    private enum WaterSplashAnimation
    {
        WaterSplash,
    }

    private enum LipMonsterAnimations
    {
        LipMonsterWalking,
        LipMonsterHit,
        LipMonsterKO,
    }

    private enum TarRayzanAnimations
    {
        TarRayzanGetCloth,
        TarRayzanSeed,
        TarRayzanGiveSeed,
        TarRayzanGiveSeedEnd,
        TarRayzanLeave,
        TarRayzanCutscene,
        TarRayzanIdle,
    }

    private enum BlueAntitoonAnimations
    {
        AntitoonBite,
        AntitoonZoof,
        AntitoonEyesBlink,
        AntitoonEyesBounce,
        AntitoonJumpStart,
        AntitoonZoofStart,
        AntitoonJumpEnd,
        AntitoonJumpStart2,
        AntitoonHit,
        AntitoonWalking,
        AntitoonFlying,
        AntitoonIdle,
    }

    private enum RedAntitoonAnimations
    {
        AntitoonBite,
        AntitoonZoof,
        AntitoonEyesBlink,
        AntitoonEyesBounce,
        AntitoonJumpStart,
        AntitoonZoofStart,
        AntitoonJumpEnd,
        AntitoonJumpStart2,
        AntitoonHit,
        AntitoonWalking,
        AntitoonFlying,
        AntitoonIdle,
    }

    private enum RaymanOnBzzitAnimations
    {
        RayBzzitIdle,
        RayBzzitIdleFlying,
        RayBzzitSpeedflight,
        RayBzzitHit,
        RayBzzitFistChargeStart,
        RayBzzitFistCharge,
        RayBzzitFistThrow,
        RayBzzitSting,
        RayBzzitSpeedflightStart,
        RayBzzitSpeedflightEnd,
        RayBzzitLevelEndStart,
        RayBzzitLevelEndStart2,
        RayBzzitFriendshipCutscene,
        RayBzzitFriendshipCutsceneEnd,
        RayBzzitLevelEnd,
        RayBzzitLevelLoadCutscene,
        RayBzzitLevelLoadCutscene2,
    }

    private enum RisingWaterAnimations
    {
        RisingWater1,
        RisingWater2,
        RisingWater3,
        RisingWater4,
    }

    private enum WingedRingAnimations
    {
        DamagedWingedRing,
        PinkWingedRing,
    }

    private enum BetillaAnimations
    {
        BetillaIdle,
        BetillaTalking,
        BetillaTalking2,
        BetillaGivePowerStart,
        BetillaGivePower,
        BetillaGivePowerEnd,
        BetillaIdle2,
    }

    private enum CloudSplashAnimations
    {
        CloudSplashBack,
        CloudSplashFront,
    }

    private enum SpikyPlantAnimations
    {
        SpikyPlant,
    }

    private enum CageUnlockAnimations
    {
        CageUnlock1,
        CageUnlock2,
        CageUnlock3,
        CageUnlock4,
        CageUnlock5,
        CageUnlock6,
    }

    private enum StrangeGateAnimations
    {
        TopHalf,
        LowerHalf,
    }

    private enum BreakoutAnimations
    {
        Pinball,
        PlumBrick,
        LevelLayouts,
    }

    private enum HunterBandLandAnimations
    {
        HunterIdle,
        HunterHit,
        HunterTakeAim,
        HunterFire,
        BulletIdle,
        BulletHammerStart,
        BulletHammer,
    }

    private enum SomeAnimations3
    {
        Anim1,
        Anim2,
        Anim3,
        Anim4,
        Anim5,
        Anim6,
        Anim7,
        Anim8,
        Anim9,
        Anim10,
    }

    private enum SomeAnimations4
    {
        Anim1,
        Anim2,
        Anim3,
        Anim4,
        Anim5,
        Anim6,
        Anim7,
        Anim8,
        Anim9,
        Anim10,
    }

    private enum SomeAnimations5
    {
        Anim1,
        Anim2,
        Anim3,
        Anim4,
        Anim5,
        Anim6,
        Anim7,
        Anim8,
        Anim9,
        Anim10,
    }
}
