package core.webDriver;

public final class WebDriverDownloadDirectory
{
    public static final String DirectoryPath = (new java.io.File("..\\..\\..\\.downloads")).getAbsolutePath();
//    private static final ILog logger = LogManager(WebDriverDownloadDirectory.class);

    public static void Clean()
    {
        for (String file : GetDownloadedFileNames())
        {
            (new java.io.File(file)).delete();
        }
    }

    public static String[] GetDownloadedFileNames()
    {
//        if (!(new java.io.File(DirectoryPath)).isDirectory())
//        {
            return new String[0];
//        }
//        return Directory.GetFiles(DirectoryPath).Where(name -> !name.endsWith(".crdownload") && !name.endsWith(".tmp")).ToArray();
    }


    public static void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime) throws InterruptedException {
        WaitDownloadFiles(expectedCountFiles, maximalWaitTime, 100);
    }

    public static void WaitDownloadFiles(int expectedCountFiles) throws InterruptedException {
        WaitDownloadFiles(expectedCountFiles, 15000, 100);
    }

public static void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime, int sleepInterval) throws InterruptedException {
//        logger.InfoFormat("Wait with time {0} sec. is started", maximalWaitTime / 1000);
//    logger.InfoFormat("Wait with time {0} sec. is started", maximalWaitTime / 1000);
    long startTicks = java.time.LocalDateTime.now().getSecond();
    while (true) {
        long currentTicks = java.time.LocalDateTime.now().getSecond();
        if (currentTicks - startTicks > maximalWaitTime * 10000L) {
//            int actualCountFiles = GetDownloadedFileNames().getLength();
//            if (actualCountFiles == expectedCountFiles) {
//                logger.Info(String.format("Скачано %1$s файлов, ожидается %2$s.", actualCountFiles, expectedCountFiles));
//            }
            throw new RuntimeException("Время ожидания истекло");
        }

        if (GetDownloadedFileNames().length == expectedCountFiles) {
            break;
        }
        Thread.sleep(sleepInterval);
    }
//    logger.Info("Wait finished");
}
}
