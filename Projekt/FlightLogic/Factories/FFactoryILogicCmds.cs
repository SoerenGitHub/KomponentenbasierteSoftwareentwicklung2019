using FlightLogic.Command;

namespace FlightLogic.Factories
{
    public static class FFactoryILogicCmds
    {
        public static FILogicCmds CreateInstance(FIDataWrite data)
        {
            return new FLogicCmds(data);
        }
    }
}
