﻿namespace GeneratorLibrary.Models.Basic
{
    public record Installations
    {
        public List<SpaceportClass> Spaceports { get; set; } = new();
        public List<Facility> Facilities { get; set; } = new();
    }

    public enum SpaceportClass
    {
        Ø,
        I,
        II,
        III,
        IV,
        V
    }

    public record Facility
    {
        public FacilityType Type { get; set; }
        public int? PR { get; set; }
        public bool IsSecret { get; set; }

        public Facility()
        {

        }
    }

    public enum FacilityType
    {
        AlienEnclave,
        BlackMarket,
        ColonialOffice,
        CorporateHeadquarters,
        CriminalBase,
        EspionageFacility,
        GovernmentResearchStation,
        MercenaryBase,
        NaturePreserve,
        NavalBase,
        PatrolBase,
        PirateBase,
        Prison,
        PrivateResearchCenter,
        RebelOrTerroristBase,
        RefugeeCamp,
        ReligiousCenter,
        SpecialJusticeGroupOffice,
        SurveyBase,
        University
    }

    public record EspionageFacility : Facility
    {
        public EspionageFacilityType SubType { get; set; }

        public EspionageFacility()
        {

        }
    }

    public enum EspionageFacilityType
    {
        Civilian,
        FriendlyMilitary,
        EnemyMilitary
    }

}
