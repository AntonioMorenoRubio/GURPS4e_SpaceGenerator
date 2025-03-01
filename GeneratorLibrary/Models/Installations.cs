using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
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
        public int? PR { get; set; } = null;

        public Facility(FacilityType facilityType)
        {
            Type = facilityType;
        }

        public Facility(FacilityType facilityType, int? populationRating)
        {
            Type = facilityType;
            PR = populationRating;
        }
    }

    public enum FacilityType
    {
        AlienEnclave,
        BlackMarket,
        ColonialOffice,
        CorporateHQ,
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
}
