using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Rayman1MSDOS
{
    public const string msdos = "Rayman 1 PC (MS-DOS)";
    public const int allfixEndIndex = 6;
    public const int jungleEndIndex = 30;
    public const int musicEndIndex = 56;
    public const int mountainEndIndex = 79;
    public const int imageEndIndex = 105;
    public const int caveEndIndex = 137;
    public const int candyEndIndex = 161;

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
        { DesignObjects.BadEyesBandLand, typeof(BadEyesAnimations) },
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
        { DesignObjects.RockGolemBlueMountains, typeof(RockGolemAnimations) },
        { DesignObjects.StoneDogBlueMountains, typeof(StoneDogAnimations) },
        { DesignObjects.MrStone, typeof(MrStoneAnimations) },
        { DesignObjects.GiantLavaRock, typeof(GiantLavaRockAnimation) },
        { DesignObjects.BlueMountainsItems, typeof(BlueMountainsAnimations) },
        { DesignObjects.LavaRockPiecesBlueMountains, typeof(LavaRockPieceAnimations) },
        { DesignObjects.LitBeacon, typeof(LitBeaconAnimation) },
        { DesignObjects.BlueMountainsItems2, typeof(BlueMountainsAnimations2) },
        { DesignObjects.CaveSpiderBlueMountains, typeof(CaveSpiderAnimations) },
        { DesignObjects.BlueMountainsItems3, typeof(BlueMountainsAnimations3) },
        { DesignObjects.BlueAntitoonsBlueMountains, typeof(BlueAntitoonAnimations) },
        { DesignObjects.PoofEffectsBlueMountains, typeof(PoofAnimations) },
        { DesignObjects.WingedRingBlueMountains, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaBlueMountains, typeof(BetillaAnimations) },
        { DesignObjects.RisingWaterBlueMountains, typeof(RisingWaterAnimations) },
        { DesignObjects.CageUnlockBlueMountains, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateBlueMountains, typeof(StrangeGateAnimations) },
        { DesignObjects.HunterPictureCity, typeof(HunterAnimations) },
        { DesignObjects.CloudSplashPictureCity, typeof(CloudSplashAnimations) },
        { DesignObjects.PictureCityItems, typeof(PictureCityAnimations) },
        { DesignObjects.ElectoonsPictureCity, typeof(ElectoonAnimations) },
        { DesignObjects.RisingOil, typeof(RisingOilAnimations) },
        { DesignObjects.CloudsPictureCity, typeof(CloudAnimations) },
        { DesignObjects.GroundPirate, typeof(GroundPirateAnimations) },
        { DesignObjects.MastPirate, typeof(MastPirateAnimations) },
        { DesignObjects.SpaceMama, typeof(SpaceMamaAnimations) },
        { DesignObjects.PoofEffectsPictureCity, typeof(PoofAnimations) },
        { DesignObjects.BlueAntitoonsPictureCity, typeof(BlueAntitoonAnimations) },
        { DesignObjects.WingedRingPictureCity, typeof(WingedRingAnimations) },
        { DesignObjects.PirateMama, typeof(PirateMamaAnimations) },
        { DesignObjects.FryingPanMonkey, typeof(FryingPanMonkeyAnimations) },
        { DesignObjects.BetillaPictureCity, typeof(BetillaAnimations) },
        { DesignObjects.CookingPot, typeof(CookingPotAnimations) },
        { DesignObjects.BadEyesPictureCity, typeof(BadEyesAnimations) },
        { DesignObjects.UnusedPin, typeof(UnusedPinAnimation) },
        { DesignObjects.Eraser, typeof(EraserAnimations) },
        { DesignObjects.Curtains, typeof(CurtainAnimations) },
        { DesignObjects.CageUnlockPictureCity, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGatePictureCity, typeof(StrangeGateAnimations) },
        { DesignObjects.BounceSparkles, typeof(BounceSparklesAnimation) },
        { DesignObjects.LavaRockPiecesCavesOfSkops, typeof(LavaRockPieceAnimations) },
        { DesignObjects.HunterCavesOfSkops, typeof(HunterAnimations) },
        { DesignObjects.JoeBall, typeof(JoeBallAnimations) },
        { DesignObjects.PoofEffectsCavesOfSkops, typeof(PoofAnimations) },
        { DesignObjects.ElectoonsCavesOfSkops, typeof(ElectoonAnimations) },
        { DesignObjects.CloudsCavesOfSkops, typeof(CloudAnimations) },
        { DesignObjects.RockGolemCavesOfSkops, typeof(RockGolemAnimations) },
        { DesignObjects.BadEyesCavesOfSkops, typeof(BadEyesAnimations) },
        { DesignObjects.StoneDogCavesOfSkops, typeof(StoneDogAnimations) },
        { DesignObjects.SeaUrchin, typeof(SeaUrchinAnimation) },
        { DesignObjects.CavesOfSkopsItems, typeof(BlueMountainsAnimations2) },
        { DesignObjects.CaveSpiderCavesOfSkops, typeof(CaveSpiderAnimations) },
        { DesignObjects.Fish, typeof(FishAnimations) },
        { DesignObjects.BlueAntitoonsCavesOfSkops, typeof(BlueAntitoonAnimations) },
        { DesignObjects.CavesOfSkopsItems2, typeof(BlueMountainsAnimations3) },
        { DesignObjects.MrSkops, typeof(MrSkopsAnimations) },
        { DesignObjects.WingedRingCavesOfSkops, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaCavesOfSkops, typeof(BetillaAnimations) },
        { DesignObjects.RisingWaterCavesOfSkops, typeof(RisingWaterAnimations) },
        { DesignObjects.JoeUFOControls, typeof(JoeUFOControlAnimations) },
        { DesignObjects.Joe, typeof(JoeAnimations) },
        { DesignObjects.EatAtJoesSign, typeof(EatAtJoesSignAnimations) },
        { DesignObjects.WaterSplashCavesOfSkops, typeof(WaterSplashAnimation) },
        { DesignObjects.ElectricPlug, typeof(PlugAnimations) },
        { DesignObjects.RisingLava, typeof(RisingLavaAnimation) },
        { DesignObjects.SomethingStrange, typeof(SomethingAnimation) },
        { DesignObjects.CageUnlockCaveOfSkops, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateCaveOfSkops, typeof(StrangeGateAnimations) },
        { DesignObjects.CavesOfSkopsItems3, typeof(CavesOfSkopsAnimations) },
        { DesignObjects.BreakableRock, typeof(BreakableRockAnimations) },
        { DesignObjects.MiniLavaRock, typeof(MiniLavaRockAnimation) },
        { DesignObjects.CloudSplashCandyChateau, typeof(CloudSplashAnimations) },
        { DesignObjects.MrDarkBosses, typeof(MrDarkBossAnimations) },
        { DesignObjects.HunterCandyChateau, typeof(HunterAnimations) },
        { DesignObjects.MrDarkFire, typeof(MrDarkFireAnimations) },
        { DesignObjects.ElectoonsCandyChateau, typeof(ElectoonAnimations) },
        { DesignObjects.BigClown, typeof(BigClownAnimations) },
        { DesignObjects.SmallClown, typeof(SmallClownAnimations) },
        { DesignObjects.CloudsCandyChateau, typeof(CloudAnimations) },
        { DesignObjects.FlyingClown, typeof(FlyingClownAnimations) },
        { DesignObjects.PoofEffectsCandyChateau, typeof(PoofAnimations) },
        { DesignObjects.BlueAntitoonsCandyChateau, typeof(BlueAntitoonAnimations) },
        { DesignObjects.WingedRingCandyChateau, typeof(WingedRingAnimations) },
        { DesignObjects.BetillaCandyChateau, typeof(BetillaAnimations) },
        { DesignObjects.FryingPanCandyChateau, typeof(FryingPanAnimations) },
        { DesignObjects.MrDark, typeof(MrDarkAnimations) },
        { DesignObjects.WaterSplashCandyChateau, typeof(WaterSplashAnimation) },
        { DesignObjects.SomeObject18, typeof(LeftOverObjectAnimations) },
        { DesignObjects.CageUnlockCandyChateau, typeof(CageUnlockAnimations) },
        { DesignObjects.StrangeGateCandyChateau, typeof(StrangeGateAnimations) },
        { DesignObjects.CandyChateauItems, typeof(CandyChateauAnimations) },
        { DesignObjects.SmallClownWater, typeof(SmallClownWaterAnimations) },
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
        int endIndex = allfixEndIndex;

        switch (_index)
        {
            case 0:
                startIndex = 0;
                endIndex = allfixEndIndex;
                break;
            case 1:
                startIndex = allfixEndIndex + 1;
                endIndex = jungleEndIndex;
                break;
            case 2:
                startIndex = jungleEndIndex + 1;
                endIndex = musicEndIndex;
                break;
            case 3:
                startIndex = musicEndIndex + 1;
                endIndex = mountainEndIndex;
                break;
            case 4:
                startIndex = mountainEndIndex + 1;
                endIndex = imageEndIndex;
                break;
            case 5:
                startIndex = imageEndIndex + 1;
                endIndex = caveEndIndex;
                break;
            case 6:
                startIndex = caveEndIndex + 1;
                endIndex = candyEndIndex;
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
        BadEyesBandLand,
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
        RockGolemBlueMountains,
        StoneDogBlueMountains,
        MrStone,
        GiantLavaRock,
        BlueMountainsItems,
        LavaRockPiecesBlueMountains,
        LitBeacon,
        BlueMountainsItems2,
        CaveSpiderBlueMountains,
        BlueMountainsItems3,
        BlueAntitoonsBlueMountains,
        PoofEffectsBlueMountains,
        WingedRingBlueMountains,
        BetillaBlueMountains,
        RisingWaterBlueMountains,
        CageUnlockBlueMountains,
        StrangeGateBlueMountains,
        //80, 81 & 82 are parallax
        HunterPictureCity = 83,
        CloudSplashPictureCity,
        PictureCityItems,
        ElectoonsPictureCity,
        RisingOil,
        CloudsPictureCity,
        GroundPirate,
        MastPirate,
        SpaceMama,
        PoofEffectsPictureCity,
        BlueAntitoonsPictureCity,
        WingedRingPictureCity,
        PirateMama,
        FryingPanMonkey,
        BetillaPictureCity,
        CookingPot,
        BadEyesPictureCity,
        UnusedPin,
        Eraser,
        Curtains,
        CageUnlockPictureCity,
        StrangeGatePictureCity,
        BounceSparkles,
        //106 is parallax
        LavaRockPiecesCavesOfSkops = 107,
        HunterCavesOfSkops,
        JoeBall,
        PoofEffectsCavesOfSkops,
        ElectoonsCavesOfSkops,
        CloudsCavesOfSkops,
        RockGolemCavesOfSkops,
        BadEyesCavesOfSkops,
        StoneDogCavesOfSkops,
        SeaUrchin,
        CavesOfSkopsItems,
        CaveSpiderCavesOfSkops,
        Fish,
        BlueAntitoonsCavesOfSkops,
        CavesOfSkopsItems2,
        MrSkops,
        WingedRingCavesOfSkops,
        BetillaCavesOfSkops,
        RisingWaterCavesOfSkops,
        JoeUFOControls,
        Joe,
        EatAtJoesSign,
        WaterSplashCavesOfSkops,
        ElectricPlug,
        RisingLava,
        SomethingStrange,
        CageUnlockCaveOfSkops,
        StrangeGateCaveOfSkops,
        CavesOfSkopsItems3,
        BreakableRock,
        MiniLavaRock,
        //138 & 139 are parallax
        CloudSplashCandyChateau = 140,
        MrDarkBosses,
        HunterCandyChateau,
        MrDarkFire,
        ElectoonsCandyChateau,
        BigClown,
        SmallClown,
        CloudsCandyChateau,
        FlyingClown,
        PoofEffectsCandyChateau,
        BlueAntitoonsCandyChateau,
        WingedRingCandyChateau,
        BetillaCandyChateau,
        FryingPanCandyChateau,
        MrDark,
        WaterSplashCandyChateau,
        //156 has no animation data for some reason
        SomeObject18 = 157,
        CageUnlockCandyChateau,
        StrangeGateCandyChateau,
        CandyChateauItems,
        SmallClownWater,
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
        RockGolemMiniLavaRock,
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
        MrStoneDead,
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

    private enum PictureCityAnimations
    {
        PencilDown,
        PencilPointUp,
        PencilUp,
        PencilPointDown,
        PencilStump,
        PencilWithoutPoint,
        Pen,
        TackDown,
        TackUp,
        TackLeft,
        PencilSharpener,
        YinYangBall,
        YinYangBallPointy,
        YinYangBallCrazy,
        TackUpDown,
        PartialTackDown,
        PenClicking,
    }

    private enum RisingOilAnimations
    {
        RisingOil1,
        RisingOil2,
        RisingOil3,
        RisingOil4,
    }

    private enum GroundPirateAnimations
    {
        Earring,
        EarringThrow,
        Appearing,
        BoatSwingAndLand,
        Falling,
        BoatSwing,
        BoatSwingFast,
        Falling2,
        Landing,
        Hit,
        TurnAround,
        Idle,
        Walking,
        DuckingStart,
        Ducking,
        DuckingEnd,
    }

    private enum MastPirateAnimations
    {
        Bomb,
        BombThrow,
        JumpOut,
        Falling,
        Landing,
        NinjaKickStart,
        NinjaKick,
        NinjaKickEnd,
        BombThrowGrounded,
        Hit,
        IdleBomb,
        IdleBombGrab,
        Idle,
        IdleMast,
        MastDuckAway,
        MastAppear,
        MastHide,
        DuckingStart,
        Ducking,
        DuckingEnd,
        DuckingBombStart,
        DuckingBomb,
        DuckingBombEnd,
        MastHit,
    }

    private enum SpaceMamaAnimations
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
        Anim11,
        Anim12,
        Anim13,
        Anim14,
        Anim15,
        Anim16,
        Anim17,
        Anim18,
        Anim19,
        Anim20,
        Anim21,
        Anim22,
        Anim23,
        Anim24,
        Anim25,
        Anim26,
        Anim27,
        Anim28,
        Anim29,
        Anim30,
        Anim31,
        Anim32,
        Anim33,
        Anim34,
        Anim35,
        Anim36,
        Anim37,
        Anim38,
        Anim39,
        Anim40,
        Anim41,
        Anim42,
        Anim43,
        Anim44,
        Anim45,
        Anim46,
        Anim47,
        Anim48,
        Anim49,
    }

    private enum PirateMamaAnimations
    {
        PirateBoatIdle,
        SeaWaves,
        PirateMamaIdle,
        PirateMamaIdle2,
        PirateMamaFiringKnifesStart,
        PirateMamaFiringKnifes,
        PirateMamaFiringKnifesFull,
        PirateMamaHit,
        SeaWaves2,
        PirateMamaWalking,
        PirateMamaFiringKnifesEnd,
        PirateMamaBounce,
        PirateMamaDancing,
        PirateMamaJumping,
        PirateMamaLanding,
        PirateMamaHitInAir,
        PirateMamaKnifeBounce,
        PirateMamaKnife,
        PirateMamaKnife2,
        PirateMamaKnife3,
        PirateMamaKnifeFalling,
        PirateMamaKnifeInGround,
        PirateMamaSpawnStart,
        PirateMamaSpawn,
        PirateMamaSpawnEnd,
    }

    private enum FryingPanMonkeyAnimations
    {
        FryingPanMonkey1Flying1,
        FryingPanMonkey1Flying2,
        FryingPanMonkey1Flying3,
        FryingPanMonkey1Flying4,
        FryingPanMonkey1Breaking,
        FryingPanMonkey1Hit,
        FryingPanMonkey1FlyingStart,
        FryingPanMonkey1Flying,
        FryingPanMonkey1FlyingStop,
        FryingPanMonkey1Idle,
        FryingPanMonkey1Idle2,
        FryingPanMonkey1Dead,
        FryingPanMonkey1DeadFlyingOff,
        FryingPanMonkey2Flying1,
        FryingPanMonkey2Flying2,
        FryingPanMonkey2Flying3,
        FryingPanMonkey2Flying4,
        FryingPanMonkey2Breaking,
        FryingPanMonkey2Hit,
        FryingPanMonkey2FlyingStart,
        FryingPanMonkey2Flying,
        FryingPanMonkey2FlyingStop,
        FryingPanMonkey2Idle,
        FryingPanMonkey2Idle2,
        FryingPanMonkey2Dead,
        FryingPanMonkey2DeadFlyingOff,
        FryingPan,
    }

    private enum CookingPotAnimations
    {
        CookingPotDown,
        CookingPotDownExplosion,
        CookingPotSouthEast,
        CookingPotSouthEastExplosion,
        CookingPotEast,
        CookingPotEastExplosion,
        CookingPotLidDown,
        CookingPotPotDown,
        CookingPotLidSouthEast,
        CookingPotPotSouthEast,
        CookingPotLidEast,
        CookingPotPotEast,
    }

    private enum UnusedPinAnimation
    {
        UnusedPinAngles,
    }

    private enum EraserAnimations
    {
        EraserIdle,
        EraserBouncing,
    }

    private enum CurtainAnimations
    {
        EdgeCurtains,
        MiddleCurtain,
    }

    private enum BounceSparklesAnimation
    {
        BounceSparkles,
    }

    private enum JoeBallAnimations
    {
        JoeBall,
        JoeBallBounce,
    }

    private enum SeaUrchinAnimation
    {
        SeaUrchin,
    }

    private enum FishAnimations
    {
        GirlFishSwimming,
        GirlFishScared,
        GirlFishTurnAround,
        GuyFishIdle,
        GuyFishPush,
        GuyFishTurnAround,
        MeanFishBiteStart,
        MeanFishBite,
        MeanFishBiteEnd,
        MeanFishSwimming,
        MeanFishBiteStartLeft,
        MeanFishBiteStartRight,
        MeanFishTurnAround,
        MeanFishHit,
    }

    private enum MrSkopsAnimations
    {
        Sleeping,
        WakingUp,
        Idle,
        IdleFast,
        GroundSlam,
        ZapStart,
        ZapEnd,
        Zap,
        ClawStart,
        Clawing,
        ClawEnd,
        Hit,
        Dead,
        Walking,
        Running,
        WalkingBackwards,
        ZapBeam,
        ClawingClaw,
        JumpStart,
        Jumping,
        JumpEnd,
        Landing,
        ZapBeamCopy,
    }

    private enum JoeUFOControlAnimations
    {
        SelectorBlinkingRight,
        SelectorBlinkingDown,
        SelectorBlinkingLeft,
        SelectorBlinkingUp,
        SelectorUpRight,
        SelectorRightDown,
        SelectorLeftUp,
        SelectorDownLeft,
        SelectorCenter,
        SelectorLeft1Right2,
        SelectorLeft2Right1,
        SelectorUp1Down2,
        SelectorUp2Down1,
        JoeUFO1,
        JoeUFO2,
    }

    private enum JoeAnimations
    {
        DownIdle,
        SadIdle,
        HappyIdle,
        SadTalkingStart,
        SadIdle2,
        SadTalking,
        HappyTalking,
        HappyTalkingFlipped,
        HappyCelebrate,
        HappyCelebrateFlipped,
        SadTalking2,
    }

    private enum EatAtJoesSignAnimations
    {
        SignOff,
        SignOneAtATime,
        SignOneLEDString,
    }

    private enum PlugAnimations
    {
        PlugOut,
        PlugSlammedIn,
        PlugIn,
    }

    private enum RisingLavaAnimation
    {
        RisingLava,
    }

    private enum SomethingAnimation
    {
        SomethingStrange,
    }

    private enum CavesOfSkopsAnimations
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
        Empty3,
        Empty4,
        Empty5,
        Empty6,
        Empty7,
        Empty8,
        Empty9,
        SmokePuff,
        SlidingPlateau2,
        SlidingPlateau2Sliding,
    }

    private enum BreakableRockAnimations
    {
        Empty1,
        Empty2,
        Empty3,
        Empty4,
        Empty5,
        BreakableRockIdle,
        BreakableRockShake,
        BreakableRockPart1,
        BreakableRockPart2,
        BreakableRockPart3,
        BreakableRockPart4,
        BreakableRockPart5,
    }

    private enum MiniLavaRockAnimation
    {
        Empty1,
        Empty2,
        Empty3,
        Empty4,
        Empty5,
        Empty6,
        MiniLavaRock,
    }

    private enum MrDarkBossAnimations
    {
        Boss2Idle,
        Boss2Hit,
        Boss2Attack,
        Boss2AimDown,
        Boss2AimUp,
        Boss2Aim,
        Boss3Hit,
        Boss3JumpStart,
        Boss3JumpMiddle,
        Boss3Jump,
        Moss3LandingStart,
        Moss3Landing,
        Boss3Idle,
        Boss1AttackStart,
        Boss1Attack,
        Boss1AttackEnd,
        Boss1AttackHit,
        Boss1AttackClaw,
        Boss1BlockingStart,
        Boss1Blocking,
        Boss1BlockingEnd,
        Boss1AttackBlockingStart,
        Boss1AttackBlocking,
        Boss1AttackBlockingEnd,
        Boss1Idle,
        Boss2Beam,
        Boss1Hit,
        Boss1Walking,
    }

    private enum MrDarkFireAnimations
    {
        FireVanish,
        FireThin,
        Fire,
        FireStart,
        FireEnd,
    }

    private enum BigClownAnimations
    {
        HammerAttack,
        HammerAttackEnd,
        Dead,
        Hit,
        TurnAround,
        Idle,
        Walking,
    }

    private enum SmallClownAnimations
    {
        Attack,
        AttackEnd,
        WaterSquirt,
        Dead,
        Hit,
        TurnAround,
        Idle,
        Walking,
    }

    private enum FlyingClownAnimations
    {
        Idle,
        Idle2,
        Bomb,
        BombDrop,
        PresentShake,
        PresentAttack,
        PresentAttackFull,
    }

    private enum MrDarkAnimations
    {
        FlyingAway,
        FlyingAwayStart,
        Cursing,
        Idle,
        Jumping,
        MagicBoltMiddle,
        MagicBoltHead,
        MagicBoltTail,
        Rope,
        MrDarkRope,
        MrDarkPullRopeStart,
        MrDarkPullRope,
        MrDarkPullRopeEnd,
        MrDarkRemoveRope,
    }

    private enum LeftOverObjectAnimations
    {
        Center,
        Up,
        Right,
        Down,
        Left,
    }

    private enum CandyChateauAnimations
    {
        CandyWrapper,
        LongCandyWrapper,
        LongCandyWrapperExtend,
        SwissArmyKnife,
        Fork,
        Nougat,
        SwissArmyKnifeMiniRight,
        SwissArmyKnifeMiniLeft,
        Corkscrew,
    }

    private enum SmallClownWaterAnimations
    {
        Falling,
        FallingDrop,
        FallingDropThin,
        Landing,
    }
}
