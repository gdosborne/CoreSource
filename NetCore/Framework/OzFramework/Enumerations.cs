/* File="Enumerations"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace Common {
    public enum ApplicationStatus {
        WatchDogIdle = -2,
        Starting,
        Started,
        Closed,
        NotUsed = 50,
        None = 99,
        Unknown
    }

    public enum LogTypes {
        Information,
        Warning,
        Error
    }

    public enum EmailTypes {
        InvalidNotificationError = -5,
        CouldNotGetTemplate,
        ApplicationError,
        SqlError,
        MissingBusinessInfo,
        Unspecified,
        Approved,
        NoManufacturer,
        OnHold,
        NotificationAlreadySent,
        NotificationRequest,
        InvalidNotification,
        ContractIdInvalid,
        UpdateToDefaultOptOut,
        MissingOrInvalidEmail,
        Success
    }

}
