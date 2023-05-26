using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.Enums
{
    public enum GraphicCard
    {
        NVIDIA=1,
        AMDRadeon,
        IntelUHD,
        IntelHD,
        [Display(Name = "Intel Iris XE")]
        IntelIrisXE,
        [Display(Name = "ATI Radeon")]
        ATIRadeon
    }
}
