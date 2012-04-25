namespace Common.Libraries.Utility.Shared
{
    public class TransferConstants
    {
        
    }

    public enum TransferMethod
    {
        Manual,
        QuickReceive
    }

    public enum TransferStatus
    {
        TI,
        TO,
        VO,
        FROM
    }

    public enum TransferTempStatus
    {
        NONE,
        REJCT,
        PENDG,
        ACCPT
    }

    public enum TransferTypes
    {
        STORETOSTORE,
        JOEXC,
        JORET,
        JORFB,
        JOSCR
    }

    public enum TransferSource
    {
        NONE,
        TOPSTICKET,
        CLXTICKET
    }
}
