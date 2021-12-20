using GuardNet;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.MetaData
{
    public class MetaDataService : IMetaDataService
    {
        private readonly RmsDbContext _context;

        public MetaDataService(RmsDbContext context)
        {
            _context = context;
        }

        public Dal.Entities.MetaData GetMetaData()
        {
           var fieldDefinitions = _context.FieldDefinitions.ToList();

            if (fieldDefinitions == null)
            {
                throw new ServiceException(string.Empty, $"Unable to find field definitions");
            }

            return new Dal.Entities.MetaData()
            {
                FieldDefinitions = fieldDefinitions,
                LatestAppVersion = "0.1"

            };
        }
    }
}
