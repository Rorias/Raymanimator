using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Rayman1MSDOS
{
    public const string msdos = "Rayman 1 PC (MS-DOS)";

    private static Dictionary<DesignObjects, Type> ObjectAnimations = new Dictionary<DesignObjects, Type>()
    {
        { DesignObjects.Rayman, typeof(RayAnimations) },
        { DesignObjects.Items, typeof(ItemsAnimations) },
        { DesignObjects.Items2, typeof(Items2Animations) },
        { DesignObjects.ContinueClock, typeof(ContinueClockAnimations) },
        { DesignObjects.HunterDreamForest, typeof(HunterAnimations) },
        { DesignObjects.PoofEffectsDreamForest, typeof(PoofAnimations) },
        { DesignObjects.ElectoonsDreamForest, typeof(ElectoonAnimations) },
        { DesignObjects.Livingstones, typeof(LivingStoneAnimations) },
        { DesignObjects.Bzzit, typeof(BzzitAnimations) },
        { DesignObjects.Moskito, typeof(MoskitoAnimations) },
        { DesignObjects.Plants, typeof(PlantAnimations) },
        { DesignObjects.Piranha, typeof(PiranhaAnimations) },
        { DesignObjects.WaterSplashDreamForest, typeof(WaterSplashAnimation) },
        { DesignObjects.LipMonster, typeof(LipMonsterAnimations) },
        { DesignObjects.TarRayzan, typeof(TarRayzanAnimations) },
        { DesignObjects.BlueAntitoonsDreamForest, typeof(BlueAntitoonAnimations) },
        { DesignObjects.RedAntitoons, typeof(RedAntitoonAnimations) },
        { DesignObjects.RaymanOnBzzit, typeof(RaymanOnBzzitAnimations) },
        { DesignObjects.RisingWaterDreamForest, typeof(RisingWaterAnimations) },
        { DesignObjects.WingedRingDreamForest, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaDreamForest, typeof(BetillaAnimations) },
        { DesignObjects.CloudSplashDreamForest, typeof(CloudSplashAnimations) },
        { DesignObjects.SpikyPlant, typeof(SpikyPlantAnimations) },
        { DesignObjects.CageUnlockDreamForest, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateDreamForest, typeof(StrangeGateAnimations) },
        { DesignObjects.Breakout, typeof(BreakoutAnimations) },
        { DesignObjects.HunterBandLand, typeof(HunterAnimations) },
        { DesignObjects.ElectoonsBandLand, typeof(ElectoonAnimations) },
        { DesignObjects.Trumpet, typeof(TrumpetAnimations) },
        { DesignObjects.MrSax, typeof(MrSaxAnimations) },
        { DesignObjects.CloudsBandLand, typeof(CloudAnimations) },
        { DesignObjects.Cymbal, typeof(CymbalAnimations) },
        { DesignObjects.BandLandItems, typeof(BandLandAnimations) },
        { DesignObjects.Monk, typeof(MonkAnimations) },
        { DesignObjects.Notes, typeof(NoteAnimations) },
        { DesignObjects.Moth, typeof(MothAnimations) },
        { DesignObjects.DrumSlam, typeof(DrumSlamAnimations) },
        { DesignObjects.GKeyNote, typeof(GKeyAnimation) },
        { DesignObjects.RedDrummer, typeof(RedDrummerAnimations) },
        { DesignObjects.BlueAntitoonsBandLand, typeof(BlueAntitoonAnimations) },
        { DesignObjects.PoofEffectsBandLand, typeof(PoofAnimations) },
        { DesignObjects.BadEyes, typeof(BadEyesAnimations) },
        { DesignObjects.BlueSpiky, typeof(BlueSpikyBallAnimation) },
        { DesignObjects.DivingDrum, typeof(DivingDrumAnimations) },
        { DesignObjects.WaterSplashBandLand, typeof(WaterSplashAnimation) },
        { DesignObjects.WingedRingBandLand, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaBandLand, typeof(BetillaAnimations) },
        { DesignObjects.FryingPanBandLand, typeof(FryingPanAnimations) },
        { DesignObjects.CloudSplashBandLand, typeof(CloudSplashAnimations) },
        { DesignObjects.CageUnlockBandLand, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateBandLand, typeof(StrangeGateAnimations) },
        { DesignObjects.CloudSplashBlueMountains, typeof(CloudSplashAnimations) },
        { DesignObjects.HunterBlueMountains, typeof(HunterAnimations) },
        { DesignObjects.ElectoonsBlueMountains, typeof(ElectoonAnimations) },
        { DesignObjects.CloudsBlueMountains, typeof(CloudAnimations) },
        { DesignObjects.RockGolem, typeof(RockGolemAnimations) },
        { DesignObjects.StoneDog, typeof(StoneDogAnimations) },
        { DesignObjects.MrStone, typeof(MrStoneAnimations) },
        { DesignObjects.GiantLavaRock, typeof(GiantLavaRockAnimation) },
        { DesignObjects.BlueMountainsItems, typeof(BlueMountainsAnimations) },
        { DesignObjects.LavaRockPieces, typeof(LavaRockPieceAnimations) },
        { DesignObjects.LitBeacon, typeof(LitBeaconAnimation) },
        { DesignObjects.BlueMountainsItems2, typeof(BlueMountainsAnimations2) },
        { DesignObjects.CaveSpider, typeof(CaveSpiderAnimations) },
        { DesignObjects.BlueMountainsItems3, typeof(BlueMountainsAnimations3) },
        { DesignObjects.BlueAntitoonsBlueMountains, typeof(BlueAntitoonAnimations) },
        { DesignObjects.PoofEffectsBlueMountains, typeof(PoofAnimations) },
        { DesignObjects.WingedRingBlueMountains, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaBlueMountains, typeof(BetillaAnimations) },
        { DesignObjects.RisingWaterBlueMountains, typeof(RisingWaterAnimations) },
        { DesignObjects.CageUnlockBlueMountains, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateBlueMountains, typeof(StrangeGateAnimations) },
        { DesignObjects.SomeObject20, typeof(SomeAnimations20) },
    };

    public static List<string> FileOptions = new List<string>()
    {
        "Global", "Dream Forest", "Band Land", "Blue Mountains", "Picture City", "Caves of Skops", "Candy Chateau"
    };

    public static List<string> SetAnimationsForObject(int _object)
    {
        Type animations = ObjectAnimations[(DesignObjects)_object];

        List<string> anims = new List<string>();

        foreach (object obj in Enum.GetValues(animations))
        {
            anims.Add(obj.ToString());
        }

        return anims;
    }

    public static List<string> GetObjectsForFileIndex(int _index)
    {
        List<string> objects = new List<string>();

        int startIndex = 0;
        int endIndex = 6;

        switch (_index)
        {
            case 0:
                startIndex = 0;
                endIndex = 6;
                break;
            case 1:
                startIndex = 7;
                endIndex = 31;
                break;
            case 2:
                startIndex = 32;
                endIndex = 56;
                break;
            case 3:
                startIndex = 57;
                endIndex = 99;
                break;
            default:
                break;
        }

        foreach (object obj in Enum.GetValues(typeof(DesignObjects)))
        {
            if ((int)obj >= startIndex && (int)obj <= endIndex)
            {
                objects.Add(obj.ToString());
            }
        }

        return objects;
    }

    //All missing indexes are either Design objects with no animations, or Design objects that are not animated.
    //I saw no point in adding them to the list if they don't.. animate
    public enum DesignObjects
    {
        Rayman,
        Items,
        //MiniRayman, //doesn't have animations? I don't know how the game handles this
        Items2 = 3,
        //Fonts at 4 and 5 don't have animations
        ContinueClock = 6,
        //7 and 8 are parallax
        HunterDreamForest = 9,
        PoofEffectsDreamForest,
        ElectoonsDreamForest,
        Livingstones,
        Bzzit,
        Moskito,
        Plants,
        Piranha,
        WaterSplashDreamForest,
        LipMonster,
        TarRayzan,
        BlueAntitoonsDreamForest,
        RedAntitoons,
        RaymanOnBzzit,
        RisingWaterDreamForest,
        WingedRingDreamForest,
        BetillaDreamForest,
        CloudSplashDreamForest,
        SpikyPlant,
        CageUnlockDreamForest,
        StrangeGateDreamForest,
        Breakout,
        //31 is parallax
        HunterBandLand = 32,
        ElectoonsBandLand,
        Trumpet,
        MrSax,
        CloudsBandLand,
        Cymbal,
        BandLandItems,
        Monk,
        Notes,
        Moth,
        DrumSlam,
        GKeyNote,
        RedDrummer,
        BlueAntitoonsBandLand,
        PoofEffectsBandLand,
        BadEyes,
        BlueSpiky,
        DivingDrum,
        WaterSplashBandLand,
        WingedRingBandLand,
        BetillaBandLand,
        FryingPanBandLand,
        CloudSplashBandLand,
        CageUnlockBandLand,
        StrangeGateBandLand,
        //57 & 58 are parallax
        CloudSplashBlueMountains = 59,
        HunterBlueMountains,
        ElectoonsBlueMountains,
        CloudsBlueMountains,
        RockGolem,
        StoneDog,
        MrStone,
        GiantLavaRock,
        BlueMountainsItems,
        LavaRockPieces,
        LitBeacon,
        BlueMountainsItems2,
        CaveSpider,
        BlueMountainsItems3,
        BlueAntitoonsBlueMountains,
        PoofEffectsBlueMountains,
        WingedRingBlueMountains,
        BetillaBlueMountains,
        RisingWaterBlueMountains,
        CageUnlockBlueMountains,
        StrangeGateBlueMountains,
        SomeObject20,
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
        MiniRayFistChargeStart,
        MiniRayFistCharge,
        MiniRayFistThrowStart,
        MiniRayFistThrowEnd,
        MiniRayLeaningForward,
        MiniRayGrimace,
        MiniRayHandstandStart,
        MiniRayHandstandWalking,
        MiniRayHandstandEnd,
        MiniRayHandstandIdle,
        MiniRayHandstandEnd2,
        MiniRayHandstandAttack,
        MiniRayHandstandScared,
        MiniRayHandstandAttack2,
        MiniRayFlyingStart,
        MiniRayFlying,
        MiniRayFlyingEnd,
        MiniRayJumping,
        MiniRayJumpingToFalling,
        MiniRayFalling,
        MiniRayLanding,
        MiniRayTakingDamage,
        MiniRayIdleWaiting,
        MiniRayIdle,
        MiniRayStrangeIdle,
        MiniRayTakingDamageStart,
        MiniRayDuckingStart,
        MiniRayDuckingFastStart,
        MiniRayStunnedDamage,
        MiniRayWalking,
        MiniRayClimbingVineUp,
        MiniRayClimbingVineDown,
        MiniRayClimbingVineIdleStart,
        MiniRayClimbingVineIdle,
        MiniRayClimbingVineFistCharge,
        MiniRayClimbingVineFistThrow,
        MiniRayMagicianDepositStart,
        MiniRayMagicianDeposit,
        MiniRayMagicianDepositEnd,
        MiniRayHangingIdle,
        MiniRayHangingIdleWaitingStart,
        MiniRayHangingIdleWaiting,
        MiniRayHangingIdleWaitingEnd,
        MiniRayHangingIdleStart,
        MiniRayStrangeTakingDamageStart,
        MiniRayStrangeTakingDamage,
        MiniRayHangingToLanding,
        MiniRayPlantingSeedStart,
        MiniRayPlantingSeedEnd,
        MiniRayFallingFistThrow,
        MiniRayHandstandBounce,
        MiniRayHandstandDiveStart,
        MiniRayHandstandDive,
        MiniRayHandstandDiveEnd,
        MiniRayFlyingInfStart,
        MiniRayFlyingInf,
        MiniRayFlyingInfEnd,
        MiniRayRunning,
        MiniRaySlingFullCircle,
        MiniRayStrangeBalls,
        MiniRayTurnAroundStart,
        MiniRayTurnAroundEnd,
        MiniRayHangingFistChargeStart,
        MiniRayHangingFistCharge,
        MiniRayHangingFistThrow,
        MiniRayPowerReceiveStart,
        MiniRayPowerReceive,
        MiniRayPowerReceiveEnd,
        MiniRayBrakingStart,
        MiniRayBraking,
        MiniRayBrakingEnd,
        MiniRayWalkingEnd,
        MiniRayRunningToJumpingStart,
        MiniRayRunningToJumping,
        MiniRaySpinJump,
        MiniRaySpinJumpEnd,
        MiniRayCrawlStart,
        MiniRayCrawl,
        MiniRayCrawlBackwardsSlowStart,
        MiniRayGlidingForward,
        MiniRayGlidingBackwards,
        MiniRayGlidingForwardStart,
        MiniRayCrawlSlowStart,
        MiniRayCrawlBackwards,
        MiniRayCrawlBackwardsSlowEnd,
        MiniRayHandstandKick,
        MiniRayHandstandNoFeetIdle,
        MiniRayHandstandNoFeetWalking,
        MiniRayFeet,
        MiniRayDeathDrowning,
        MiniRayNoLivesStart,
        MiniRayNoLives,
        MiniRayNoLivesEnd,
        MiniRayContinueIdle,
        MiniRayNoLivesCancel,
        MiniRayContinueAcceptStart,
        MiniRayContinueAccept,
        MiniRayContinueAcceptEnd,
        MiniRayLevelFinish,
        MiniRayLeaningBackwards,
        MiniRayCrawlTurnAround,
        MiniRayGlidingForwardResetFeetStart,
        MiniRayGlidingForwardResetFeet,
        MiniRayGlidingForwardFastStart,
        MiniRayGlidingForwardEnd,
        MiniRayDeathFalling,
        MiniRayDeathSpikes,
        MiniRayDeathContinues,
        MiniEmpty1,
        MiniEmpty2,
        MiniRayWorried,
        MiniRayLevelFinishEnd,
        MiniRaySpawnParticles,
        MiniRaySpawnParticles2,
        MiniRaySpawnParticles3,
        MiniRayIdleWaiting2,
        MiniRayStrangeDance,
        MiniRayCrawlingDamage,
        MiniRayLevelSpawn,
        MiniRayHunterCutscene,
        MiniRayHunterCutscene2,
        MiniRayPondering1,
        MiniRayPondering2,
        MiniRayHairWave,
        MiniRayBody,
        MiniRayCarCutscenePart1,
        MiniRayCarCutscenePart2,
        MiniRayCarCutscenePart3,
        MiniRayDeathContinue,
        MiniRayStrangeWalk,
        MiniRayVictory,
        MiniRayIdleWaiting3,
        MiniRayIdleWaiting4,
        MiniRayIdleWaiting5,
        MiniRayIdleWaiting6,
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

    private enum HunterAnimations
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

    private enum TrumpetAnimations
    {
        TrumpetSuckingStart,
        TrumpetSucking,
        TrumpetSuckingEnd,
        TrumpetBlowingStart,
        TrumpetBlowing,
        TrumpetBlowingEnd,
        TrumpetHit,
        TrumpetTurnAround,
        TrumpetIdle,
        TrumpetWalking,
        TrumpetStrangeDancing,
    }

    private enum MrSaxAnimations
    {
        MrSaxWalking,
        MrSaxWalkingGroovy,
        MrSaxBlowNote,
        MrSaxIdle,
        MrSaxHit,
        MrSaxTurnAround,
        MrSaxWalkingStart,
        MrSaxDefeatDance,
        MrSaxDefeat,
        MrSaxDefeatIdle,
        MrSaxDefeatDanceStart,
        MrSaxStrangeFeet,
        MrSaxJumpStart,
        MrSaxJump,
        MrSaxJumpEnd,
    }

    private enum CloudAnimations
    {
        DefaultCloudIdle,
        DefaultCloudVanish,
        DefaultCloudVanishStart,
        Empty,
        DefaultCloudStatic,
        Empty2,
        Empty3,
        Empty4,
        Empty5,
        EyedCloudVanishStart,
        CloudVanishReappear,
        CloudReappear,
        CloudVanish,
        EyedCloudIdle,
        Empty6,
        EyedCloudVanishAndReappear,
        CloudReappear2,
    }

    private enum CymbalAnimations
    {
        CymbalClose,
        CymbalCloseShake,
        CymbalIdle,
        CymbalCloseTop,
        CymbalCloseShakeTop,
        CymbalIdleTop,
        CymbalCloseBottom,
        CymbalCloseShakeBottom,
        CymbalIdleBottom,
    }

    private enum BandLandAnimations
    {
        Empty,
        Empty2,
        Empty3,
        Empty4,
        MaracaIdle,
        MaracaPushed,
        GoodEyesSwirl,
        BigBongoRight,
        SmallBongo,
        BigBongoLeft,
        GhungrooBellExtend,
        MaracaFlying,
        MaracaStick,
        GhungrooBellRotate4,
        GhungrooBellRotate3,
    }

    private enum MonkAnimations
    {
        MonkBallShake,
        MonkBallBalanceStart,
        MonkBallBalanceEnd,
        MonkBallBalanceFull,
        MonkBallsSpread,
        MonkBigBongoSpread,
    }

    private enum NoteAnimations
    {
        BadNoteIdle,
        GoodNoteIdle,
        BadNoteOpenMouth,
        BadRestBombIdle,
        BadRestBombIdle2,
        Empty,
        BadNoteMini,
    }

    private enum MothAnimations
    {
       MothJumpStart,
       MothJump,
       MothBounce,
       MothHit,
       MothAngryFlyingStart,
       MothAngryStart,
       MothAttack,
       MothAvoid,
       MothTurnAround,
       MothAngryFlying,
       MothIdle,
       MothDuckStart,
       MothDuck,
       MothDuckAttackStart,
       MothFlyingAttack,
       MothDuckAttack,
       MothWalking,
    }

    private enum DrumSlamAnimations
    {
        DrumSlam,
        DrumSlamSlow,
        DrumSlamIdleFast,
        DrumSlamIdle,
        DrumSlamBall,
        DrumSlamStill,
        DrumSlamDown,
    }

    private enum GKeyAnimation
    {
        GKeyNote,
    }

    private enum RedDrummerAnimations
    {
        RedDrummerAttackFull,
        RedDrummerAttackLeftStart,
        RedDrummerAttackRightStart,
        RedDrummerAttackStart,
        RedDrummerIdle,
        RedDrummerWalkingLeft,
        RedDrummerWalkingRight,
        RedDrummerSlidingLeft,
        RedDrummerSlidingRight,
        RedDrummerAttackLeft,
        RedDrummerAttackRight,
        RedDrummerAttack,
        RedDrummerSpinStart,
        RedDrummerSpin,
        RedDrummerSpinFlying,
    }

    private enum BadEyesAnimations
    {
        BadEyesSwirl,
        BadEyesHalfClosedSwirl,
        BadEyesClosed,
        LightningShot,
    }

    private enum BlueSpikyBallAnimation
    {
        BlueSpikyBallIdle,
    }

    private enum DivingDrumAnimations
    {
        DivingDrumBounce,
        DivingDrumIdle,
        DivingDrumWalking,
        DivingDrumTurnAround,
    }

    private enum FryingPanAnimations
    {
        Shaking,
    }

    private enum RockGolemAnimations
    {
        RockGolemTurnAround,
        RockGolemIdleWater,
        RockGolemIdle,
        RockGolemRockAttack,
        RockGolemWalking,
        RockGolemHit,
        RockGolemLavaRock,
        RockGolemSlamAttack,
        RockGolemSlamAttackEnd,
        RockGolemKO,
        RockGolemRevive,
        RockGolemRockAttackFast,
        RockGolemRockAttackLow,
        RockGolemJumpStart,
        RockGolemJump,
        RockGolemJumpEnd,
        RockGolemIdle2,
    }

    private enum StoneDogAnimations
    {
        StoneDogWalking,
        StoneDogHit,
        StoneDogRolling,
        StoneDogRollingStart,
        StoneDogRollingEnd,
        StoneDogIdle,
    }

    private enum MrStoneAnimations
    {
        MrStoneWalking,
        MrStoneRunning,
        MrStoneRunningFast,
        MrStoneIdle,
        MrStoneAttackPrepare,
        MrStoneSlamAttack,
        MrStoneRockSummon,
        MrStoneSummon,
        MrStoneTurnAround,
        MrStoneStoneDogSummon,
        MrStoneFlashAttack,
        MrStoneHitWithRock,
        MrStoneHit,
        MrStoneRockThrowAttackIdle,
        MrStoneRockThrowAttack,
        MrStoneLanding,
        MrStoneJumping,
        MrStoneJumpingStart,
        MrStoneJumpingChaseStart,
        MrStoneJumpingChase,
        MrStoneJumpingChaseEnd,
        MrStoneLandingChase,
        MrStoneFeet,
        MrStoneSwatting,
        MrStoneStrangeIdle,
        MrStoneStrangeIdle2,
        MrStonePillar,
        MrStonePillarSlamRight,
        MrStonePillarSlamLeft,
        MrStonePillarSlamEndRight,
        MrStonePillarSlamEndLeft,
        MrStonePillarBall1,
        MrStonePillarBall2,
        MrStonePillarBall3,
        MrStonePillarStone1,
        MrStonePillarHead,
        MrStoneDeath,
        MrStonePillarEye,
        MrStoneRockSummonAndThrowAttack,
        MrStoneDanceStart,
        MrStoneDancing,
    }

    private enum GiantLavaRockAnimation
    {
        GiantLavaRock,
    }

    private enum BlueMountainsAnimations
    {
        SlidingPlateau,
        SlidingPlateauSliding,
        Empty1,
        BigSpikeRockFlyingUp,
        SmallSpikeRockFlying,
        SmallSpikeRockFlyingUp,
        SmallSpikeRockIdle,
        SmallSpikeRockFlyingStart,
        Empty2,
        WeightedRope1,
        WeightedRope2,
        WeightedRope3,
        WeightedRope4,
        WeightedRope5,
        WeightedRopeTop,
        WeightedRopeBottom,
        SmokePuff,
        SlidingPlateauKnotted,
        SlidingPlateauKnottedSliding,
    }

    private enum LavaRockPieceAnimations
    {
        LavaRockPiece1,
        LavaRockPiece2,
        LavaRockPiece3,
        LavaRockPiece4,
        LavaRockPiece5,
        LavaRockPiece6,
    }

    private enum LitBeaconAnimation
    {
        Empty1,
        Empty2,
        LitBeacon,
    }

    private enum BlueMountainsAnimations2
    {
        MotherAndSon,
        LoveBirds,
        MusicianSad,
        MusicianGuitarGet,
        MusicianPlayingGuitar,
        BreakableRockIdle,
        BreakableRockShake,
        BreakableRockPart1,
        BreakableRockPart2,
        BreakableRockPart3,
        BreakableRockPart4,
        BreakableRockPart5,
        MusicianCrying,
        MusicianIdle,
        CrushedGuitarRock,
        CrushedGuitarRockPhase3Shaking,
        CrushedGuitarRockPart1,
        CrushedGuitarRockPhase1,
        CrushedGuitarRockPhase1Shaking,
        CrushedGuitarRockPart2,
        CrushedGuitarRockPhase2,
        CrushedGuitarRockPhase2Shaking,
        CrushedGuitarRockPart3,
        CrushedGuitarRockPhase3,
        CrushedGuitarRockPhase4,
        CrushedGuitarRockBroken,
    }

    private enum CaveSpiderAnimations
    {
        CaveSpiderTurnAround,
        CaveSpiderIdleLook,
        CaveSpiderHeadButt,
        CaveSpiderHeadButt2,
        CaveSpiderIdleLaugh,
        CaveSpiderWalking,
        CaveSpiderNeedle,
        CaveSpiderHitStart,
        CaveSpiderHit,
        CaveSpiderHitEnd,
        CaveSpiderCeilingSting,
        CaveSpiderCeilingDropStart,
        CaveSpiderCeilingDrop,
        CaveSpiderCeilingDropEnd,
        CaveSpiderCeilingDropTheBeat,
        CaveSpiderStare,
        CaveSpiderStingToIdle,
        CaveSpiderIdleToSting,
        CaveSpiderCeilingJump,
        CaveSpiderCeilingJumpEnd,
        CaveSpiderCeilingIdle,
        CaveSpiderCeilingIdle2,
        CaveSpiderCeilingStrange,
        CaveSpiderDuckingStart,
        CaveSpiderDucking,
        CaveSpiderDuckingEnd,
        CaveSpiderHitDead,
        CaveSpiderStill,
    }

    private enum BlueMountainsAnimations3
    {
        BlueSpikyBallIdle,
        BluePillarBouncing,
        BluePillarIdle,
        BlueBigSpikyBallIdle,
        CavernPlatformDown,
        RedSpikyPlatform,
        CavernPlatformUp,
        BlueBigSpikySwing,
        BlueBigSpikySwingSwinging,
    }

    private enum SomeAnimations20
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
