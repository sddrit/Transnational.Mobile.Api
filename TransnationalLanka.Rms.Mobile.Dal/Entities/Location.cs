using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransnationalLanka.Rms.Mobile.Core.Enum;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("Location")]
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [MaxLength(20)]
        public string Code { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("name")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Column("isVehicle")]
        public bool IsVehicle { get; set; }

        [Column("isRcLocation")]
        public bool IsRcLocation { get; set; }

        [Column("isBay")]
        public bool IsBay { get; set; }

        [Column("isChamber")]
        public bool IsChamber { get; set; }


        [NotMapped]
        public string Type
        {
            get
            {
                if (IsBay)
                    return LocationType.Bay.ToString();
                if(IsChamber)
                    return LocationType.Chamber.ToString();
                if (IsVehicle)
                    return LocationType.Vehicle.ToString();
                if(IsRcLocation)
                    return LocationType.Rack.ToString();

                return LocationType.Undefined.ToString();

            }
            set { }
        }

    }
}
