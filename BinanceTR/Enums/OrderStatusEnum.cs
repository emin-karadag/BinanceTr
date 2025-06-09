namespace BinanceTR.Enums;

public enum OrderStatusEnum
{
    SYSTEM_PROCESSING = -2,
    NEW = 0,
    PARTIALLY_FILLED = 1,
    FILLED = 2,
    CANCELED = 3,
    PENDING_CANCEL = 4,
    REJECTED = 5,
    EXPIRED = 6,
}
