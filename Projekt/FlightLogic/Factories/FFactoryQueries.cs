using FlightLogic.Queries;

namespace FlightLogic.Factories
{
    public class FFactoryQueries
    {
        public static FILogicQueries CreateInstance(FIDataRead data)
        {
            return new FLogicQueries(data);
        }
    }
}
