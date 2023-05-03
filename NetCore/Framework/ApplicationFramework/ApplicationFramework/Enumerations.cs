namespace Common.AppFramework {
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
}
